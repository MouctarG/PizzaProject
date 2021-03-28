using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Models;
using PizzaIllico.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuPage : ContentPage
    {
        private PizzaManager pizzaManager;
        public MainMenuPage()
        {
            InitializeComponent();
            pizzaManager = new PizzaManager (new PizzaService());
            
            listView.RefreshCommand = new Command((obj) =>
            {
           
                pizzaManager.GetAllPizzaria((pizzarias) =>
                {
                    pizzaManager.SortPizzarias(pizzarias);
                    listView.ItemsSource = pizzarias;
                    listView.IsRefreshing = false;
                });
            });

            listView.IsVisible = false;
            waitLayout.IsVisible = true;
            pizzaManager.GetAllPizzaria(pizzarias =>
            {
                pizzaManager.SortPizzarias(pizzarias);
                listView.ItemsSource = pizzarias;

                listView.IsVisible = true;
                waitLayout.IsVisible = false;
                listView.ItemSelected += (sender, e) =>
                {
                    if (listView.SelectedItem != null)
                    {
                        ItemPizzaria itemPizza = listView.SelectedItem as ItemPizzaria;
                  
                      Navigation.PushAsync(new PizzasPage((itemPizza.id)));
                    
                    }
                };

            });

        }

        private void MenuItem_OnClicked_Panier(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PanierPage());
        }
    }
}