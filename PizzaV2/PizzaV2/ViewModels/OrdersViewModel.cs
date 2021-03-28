using System;
using System.Collections.ObjectModel;

namespace PizzaV2.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        public ObservableCollection<ItemOrder> listItemOrders { get; set; }

        public OrdersViewModel()
        {
            listItemOrders = new ObservableCollection<ItemOrder>(App.PizzaManager.GetOrders());
        }
    }

    public class ItemOrder
    {
        private string pizzas;
        private double price;
        private DateTime _orderDate;

        public string Pizzas
        {
            get => pizzas;
            set => pizzas = value;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => _orderDate = value;
        }
    }
}