$(document).ready(function () {
    // Initialize DataTable
    $('#ticketsTable').DataTable({
        "paging": true, // Enable pagination
        "searching": true, // Enable search box
        "ordering": true, // Enable column sorting
        "lengthChange": true, // Allow changing number of rows per page
        "pageLength": 10, // Default number of rows per page
        "responsive": true // Make table responsive on mobile
    });

    // Handle the View button click event
    $('.view-btn').click(function () {
        var ticketId = $(this).data('ticket-id');

        // Perform AJAX call to load ticket details
        $.ajax({
            url: '/Ticket/Details/' + ticketId, // Corrected URL for AJAX call
            method: 'GET',
            success: function (response) {
                // Inject the ticket details into the modal
                $('#ticketDetailsContent').html(response);
                // Show the modal
                $('#ticketDetailsModal').show();
            },
            error: function () {
                alert('Error loading ticket details.');
            }
        });
    });

    // Handle the Cancel button click event
    $('#cancelBtn').click(function () {
        $('#ticketDetailsModal').hide(); // Close the modal
        $('#ticketDetailsContent').empty(); // Clear modal content
    });

    // Close the modal when clicking the close button (X)
    $('.close').click(function () {
        $('#ticketDetailsModal').hide();
        $('#ticketDetailsContent').empty(); // Clear modal content
    });

    // Variable to hold the ticket ID to be deleted
    var ticketIdToDelete = null;

    // Handle the Delete button click event
    $('.delete-btn').click(function () {
        // Get the ticket ID from the button's data attribute
        ticketIdToDelete = $(this).data('ticket-id');

        // Show the confirmation modal
        $('#deleteConfirmationModal').show();
    });

    // Confirm delete button event
    $('#confirmDeleteBtn').click(function () {
        if (ticketIdToDelete) {
            // Perform AJAX delete request
            $.ajax({
                url: '/Ticket/DeleteConfirmed/' + ticketIdToDelete, // AJAX URL for deleting
                method: 'POST', // Use POST for deletion
                success: function (response) {
                    if (response.success) {
                        // On success, remove the row from the table
                        $('tr[data-ticket-id="' + ticketIdToDelete + '"]').remove();
                        $('#deleteConfirmationModal').hide(); // Hide the modal
                    } else {
                        alert('Error deleting ticket.');
                    }
                },
                error: function () {
                    alert('Error deleting ticket.');
                }
            });
        } else {
            alert('Ticket ID not found.');
        }
    });

    // Cancel button event
    $('#cancelDeleteBtn').click(function () {
        $('#deleteConfirmationModal').hide(); // Close the modal without doing anything
    });

    // Close the modal when clicking the close button (X)
    $('.close').click(function () {
        $('#deleteConfirmationModal').hide();
    });
});
