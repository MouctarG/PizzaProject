namespace PizzaIllico.Models
{
    public class UpdatePassword
    {
        public string old_password { get; set; }
        public string new_password { get; set; }

        public UpdatePassword(string oldPassword, string newPassword)
        {
            old_password = oldPassword;
            new_password = newPassword;
        }
    }
}