using System;

namespace PizzaIllico.Models
{
    public class DataOrders
    {
        private Shop shop;
        private DateTime date;
        private double amount;

        public Shop Shop => shop;

        public DateTime Date => date;

        public double Amount => amount;
    }
}