using System.Collections.Generic;
using System.Collections.ObjectModel;
using PizzaIllico.Models;
using Storm.Mvvm;
using Xamarin.Forms;


namespace PizzaV2.Models
{
    public class ItemPanierViewModel : ViewModelBase
    {
        public ObservableCollection<ItemPanier> listItemPaniers { get; set; }
        private string _total;
        public string totalStr => $"{_total}"+" €";

     

        public ItemPanierViewModel()
        {
            listItemPaniers = new ObservableCollection<ItemPanier>(App.PizzaManager.GetPizzaInPanier());
            initTotal();
          
        }
        public string Total
        {
            get => _total;
            set => _total = value;
        }
        private void initTotal()
        {
            double res = 0;
            foreach (var it  in listItemPaniers)
                if (it!=null)
                {
                    res += it.Pizza.price;
                }
              
            _total = "Montant : " + res;
        }

    }

    public class ItemPanier
    {
        private ItemPizza _pizza;
        private int _quantite;

        public ItemPanier(ItemPizza pizza, int quantite)
        {
            _pizza = pizza;
            this._quantite = quantite;
        }

     
        public ItemPizza Pizza
        {
            get => _pizza;
            set => _pizza = value;
        }

        public int Quantite
        {
            get => _quantite;
            set => _quantite = value;
        }

       
    }
}