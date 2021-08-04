namespace api.DataLayer
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string KadNumber { get; set; }
        public string Content { get; set; }

        public ApplicationUser User { get; set; }
    }
}