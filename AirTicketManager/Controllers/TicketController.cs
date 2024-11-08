using AirTicketBLL;
using AirTicketEntittes;
using AirTicketServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace AirTicketManager.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepositoryService _ticketRepositoryService;

        public TicketController(ITicketRepositoryService ticketRepositoryService)
        {
            _ticketRepositoryService = ticketRepositoryService;
        }
        // Action to view all tickets
        public ActionResult Index()
        {
            var tickets = _ticketRepositoryService.GetAllTickets();
            return View(tickets);
        }

        // Action to view ticket details by ID
        public ActionResult Details(int id)
        {
            var ticket = _ticketRepositoryService.GetTicketById(id);
            return PartialView("_TicketDetails", ticket);
        }

        // Action to create a new ticket
        [HttpGet]
        public ActionResult Create()
        {
           
            var model = new TicketViewModel();
            model.AvailablePriorites = _ticketRepositoryService.GetAllPriorities();
            model.AvailableLebel = _ticketRepositoryService.PrepareLebels();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel viewModel)
        {
            int isSuccess = _ticketRepositoryService.AddTicket(viewModel.PassengerName, viewModel.DepartureDate, viewModel.ArrivalDate, viewModel.TicketPrice,viewModel.PriorityId);
            if (isSuccess == 1)
                return RedirectToAction("Index");
            else
                return View();
        }
        // GET: Ticket/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ticket = _ticketRepositoryService.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            var model = new TicketViewModel
            {
                TicketID = ticket.TicketID,
                PassengerName = ticket.PassengerName,
                DepartureDate = ticket.DepartureDate,
                ArrivalDate = ticket.ArrivalDate,
                TicketPrice = ticket.TicketPrice,
                PriorityId = ticket.PriorityId,
                AvailablePriorites = _ticketRepositoryService.GetAllPriorities(),
                AvailableLebel = _ticketRepositoryService.PrepareLebels()
            };

            return View(model);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(TicketViewModel viewModel)
        {
               var isSuccess = _ticketRepositoryService.UpdateTicket(viewModel.TicketID, viewModel.PassengerName, viewModel.DepartureDate, viewModel.ArrivalDate, viewModel.TicketPrice, viewModel.PriorityId);

            //if (ModelState.IsValid)
            //{
            //    var isSuccess = _ticketRepositoryService.UpdateTicket(viewModel.TicketID, viewModel.PassengerName, viewModel.DepartureDate, viewModel.ArrivalDate, viewModel.TicketPrice, viewModel.PriorityId);

            //    if (isSuccess == 1)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View(viewModel);
            //    }
            //}

            // Reload the available priorities and labels in case of validation failure
            //viewModel.AvailablePriorites = _ticketRepositoryService.GetAllPriorities();
            //viewModel.AvailableLebel = _ticketRepositoryService.PrepareLebels();


            //return View(viewModel);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            // Get the ticket by ID
            var ticket = _ticketRepositoryService.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound(); // If the ticket doesn't exist, return 404
            }

            // Return the ticket to the view for confirmation
            return View(ticket);
        }
        // POST: Ticket/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Call the repository method to delete the ticket
            int isSuccess = _ticketRepositoryService.DeleteTicket(id);

            if (isSuccess == 1)
            {
                return Json(new { success = true }); // Return a JSON response indicating success
            }
            else
            {
                return Json(new { success = false }); // Return a JSON response indicating failure
            }
        }


    }
}
