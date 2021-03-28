using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace PizzaIllico.Models
{
    public class Pizzaria
    {
        public List<ItemPizzaria>data { get; set; }
    }

    public class ItemPizzaria
    {
        public int id { get; set; }
        public string name { get; set; }
        public Position Position { get; set; }
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public double minutes_per_kilometer { get; set; }
    }
}