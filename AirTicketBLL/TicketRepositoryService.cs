using AirTicketDAL;
using AirTicketEntittes;
using AirTicketServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTicketBLL
{
    public class TicketRepositoryService : ITicketRepositoryService
    {
        private DataAccess dataAccess = new DataAccess();

        //Insert a new ticket into the database
        // Get all available priorities
        public List<SelectListItem> GetAllPriorities()
        {
            var priorities = new List<SelectListItem>();
            var dataTable = dataAccess.ExecuteQuery("GetAllPriorities", new SqlParameter[] { });

            foreach (DataRow row in dataTable.Rows)
            {
                priorities.Add(new SelectListItem
                {
                    Value = row["PriorityId"].ToString(),
                    Text = row["PriorityName"].ToString()
                });
            }

            return priorities;
        }
        public List<SelectListItem> PrepareLebels()
        {
            var lebels = new List<SelectListItem>
            { new SelectListItem {Value="0", Text=""},
             new SelectListItem{Value="1", Text="lebel1"},
             new SelectListItem{Value="2", Text="lebel2"},
             new SelectListItem{Value="3", Text="lebel3"},
            };

            return lebels;
        }

        public int AddTicket(string passengerName, DateTime departureDate, DateTime arrivalDate, decimal ticketPrice, int priorityId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {

            new SqlParameter("@PassengerName", passengerName),
            new SqlParameter("@DepartureDate", departureDate),
            new SqlParameter("@ArrivalDate", arrivalDate),
            new SqlParameter("@TicketPrice", ticketPrice),
            new SqlParameter("@PriorityId", priorityId)
            };
            return dataAccess.ExecuteNonQuery("InsertTicket", parameters);
        }
        public int UpdateTicket(int ticketID, string passengerName, DateTime departureDate, DateTime arrivalDate, decimal ticketPrice, int priorityId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketID", ticketID),
                new SqlParameter("@PassengerName", passengerName),
                new SqlParameter("@DepartureDate", departureDate),
                new SqlParameter("@ArrivalDate", arrivalDate),
                new SqlParameter("@TicketPrice", ticketPrice),
                new SqlParameter("@PriorityId", priorityId)
            };

            return dataAccess.ExecuteNonQuery("UpdateTicket", parameters);
        }
        public int DeleteTicket(int ticketID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TicketID", ticketID)
            };

            return dataAccess.ExecuteNonQuery("DeleteTicket", parameters);
        }


        // Get all tickets
        public List<TicketViewModel> GetAllTickets()
        {
            // Execute the query to get all tickets
            var dataTable = dataAccess.ExecuteQuery("GetAllTickets", new SqlParameter[] { });

            // Create a list of TicketViewModel objects to return
            var tickets = new List<TicketViewModel>();

            // Loop through each row in the DataTable and map to TicketViewModel
            foreach (DataRow row in dataTable.Rows)
            {
                var ticket = PrepareTicketViewModel(row);
                // Add the ticket to the list
                tickets.Add(ticket);
            }

            return tickets;
        }

        private TicketViewModel PrepareTicketViewModel(DataRow row)
        {
            var ticket = new TicketViewModel
            {
                TicketID = Convert.ToInt32(row["TicketID"]),
                PassengerName = row["PassengerName"].ToString(),
                DepartureDate = Convert.ToDateTime(row["DepartureDate"]),
                ArrivalDate = Convert.ToDateTime(row["ArrivalDate"]),
                TicketPrice = Convert.ToDecimal(row["TicketPrice"]),
                PriorityId = row["PriorityId"] != DBNull.Value && row["PriorityId"] != null
                        ? Convert.ToInt32(row["PriorityId"])
                        : 0,
                PriorityName = row["PriorityName"] != DBNull.Value && row["PriorityName"] != null
                        ? row["PriorityName"].ToString()
                        : "no"
            };

            return ticket;
        }

        // Get ticket by ID
        public TicketViewModel GetTicketById(int ticketId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@TicketID", ticketId)
            };

            var dataTable = dataAccess.ExecuteQuery("GetTicketById", parameters);
            // Check if we received any results
            if (dataTable.Rows.Count == 0)
            {
                return null; // No ticket found
            }

            // Get the first row (since it's a single ticket)

            return PrepareTicketViewModel(dataTable.Rows[0]);
        }
    }

}
