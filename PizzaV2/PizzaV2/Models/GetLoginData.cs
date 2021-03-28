namespace PizzaIllico.Models
{
    public class GetLoginData
    {
        public DataToken data { get; set; }
        public bool is_success { get; set; }
        private string error_code { get; set; }
        private string error_message { get; set; }

       
    }
}