namespace PizzaIllico.Models
{
    public class GetUser
    {
        public DataUser data { get; set; }
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }


      
    }
}