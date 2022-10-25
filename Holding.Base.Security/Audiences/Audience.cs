namespace Holding.Base.Security.Audiences
{
    public class Audience
    {
        public string ClientId { get; set; }
        public string Base64Secret { get; set; }
        public string Name { get; set; }
    }
}