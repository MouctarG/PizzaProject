namespace PizzaV2.Models
{
    public class RefreshToken
    {
        public string refresh_token { get; set; }
        public string client_id { get; set; }
        public string client_secret  { get; set; }

        public RefreshToken(string refreshToken)
        {
            refresh_token = refreshToken;
            client_id = "MOBILE";
            client_secret = "UNIV";
        }
    }
}