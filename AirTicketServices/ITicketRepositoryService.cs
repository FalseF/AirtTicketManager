using AirTicketEntittes;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AirTicketServices
{
    public interface ITicketRepositoryService
    {
        int AddTicket(string passengerName, DateTime departureDate, DateTime arrivalDate, decimal ticketPrice, int priorityId);
        List<TicketViewModel> GetAllTickets();
        TicketViewModel GetTicketById(int ticketId);
        List<SelectListItem> GetAllPriorities();
        List<SelectListItem> PrepareLebels();
        int DeleteTicket(int ticketID);
        int UpdateTicket(int ticketID, string passengerName, DateTime departureDate, DateTime arrivalDate, decimal ticketPrice, int priorityId);
    }
}
