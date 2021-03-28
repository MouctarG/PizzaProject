namespace PizzaIllico.Models
{
    public class UserRegister
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }

        public UserRegister(string email, string firstName, string lastName, string phoneNumber, string password)
        {
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            phone_number = phoneNumber;
            this.password = password;
            this.client_id = "MOBILE";
            this.client_secret = "UNIV";
        }
    }
}