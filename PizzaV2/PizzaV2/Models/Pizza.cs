using System.Collections.Generic;

namespace PizzaIllico.Models
{
    public class Pizza
    {
        public List<ItemPizza> data { get; set; }
        private bool is_success { get; set; }
        private string error_code { get; set; }
        private string error_message { get; set; }
   
    }

    public class ItemPizza
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public bool out_of_stock  { get; set; }
        public string imageUrl { get; set; }
        public string priceStr => $"{price}"+" €";
    }
}