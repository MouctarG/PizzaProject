namespace PizzaIllico.Models
{
    public class User
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }

        public User()
        {
        }

        public User(string email, string firstName, string lastName, string phoneNumber, string password)
        {
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            phone_number = phoneNumber;
            this.password = password;
        }
        
        
    }

    public class UserUpdate
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        
        public UserUpdate(string email, string firstName, string lastName, string phoneNumber)
        {
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            phone_number = phoneNumber;
        }
    }
}