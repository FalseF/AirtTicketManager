using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirTicketEntittes
{
    public class TicketViewModel
    {
        public TicketViewModel()
        {
            AvailablePriorites = new List<SelectListItem>();
            AvailableLebel = new List<SelectListItem>();
        }
        public int TicketID { get; set; }
        public string PassengerName { get; set; }    // Name of the passenger
        public DateTime DepartureDate { get; set; }   // Departure Date and Time
        public DateTime ArrivalDate { get; set; }     // Arrival Date and Time
        public decimal TicketPrice { get; set; }    
        public int PriorityId { get; set; }
        public int LebelId { get; set; }
        public string PriorityName { get; set; }
        public string LebelName { get; set; }
        public List<SelectListItem> AvailablePriorites { get; set; }
        public List<SelectListItem> AvailableLebel { get; set; }

    }
}
