namespace PizzaIllico.Models
{
    public class Shop
    {
        private long id;
        private string name;
        private string address;
        private double latitude;
        private double longitude;
        private double minutes_per_kilometer;

        public long Id => id;

        public string Name => name;

        public string Address => address;

        public double Latitude => latitude;

        public double Longitude => longitude;

        public double MinutesPerKilometer => minutes_per_kilometer;
    }
}