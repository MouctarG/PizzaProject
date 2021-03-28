namespace PizzaIllico.Models
{
    public class Login
    {
        public string login { get; set; }
        public string password { get; set; } 
        
       public  string client_id  { get; } 
       public  string client_secret  { get; } 
        
        
        public Login(string login, string password)
        {
            this.login = login;
            this.password = password;
            client_id = "MOBILE";
            client_secret = "UNIV";
        }
    }
}