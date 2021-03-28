using System.Collections.Generic;

namespace PizzaIllico.Models
{
    public class Orders
    {
        private List<DataOrders> data;
        private bool is_success;
        private string error_code;
        private string error_message;

        public List<DataOrders> Data => data;

        public bool IsSuccess => is_success;

        public string ErrorCode => error_code;

        public string ErrorMessage => error_message;
    }
}