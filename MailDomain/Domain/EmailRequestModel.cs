namespace EmailApplication.Domain
{
    public class EmailRequestModel
    {
        public string? SenderMailId { get; set; }
        public string? RecipientMailId { get; set; }
        public string? Body { get; set; }
    }
}