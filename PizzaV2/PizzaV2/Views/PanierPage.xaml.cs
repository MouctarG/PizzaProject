using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaV2.Models;
using PizzaV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PanierPage : ContentPage
    {
        public PanierPage()
        {
            InitializeComponent();
            BindingContext = new ItemPanierViewModel();
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (App.PizzaManager.GetPizzaInPanier().Any())
            {
                App.PizzaManager.SaveOrder();
                await DisplayAlert("Commande", "Commande éffectuée", "OK");
               await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Le panier est vide", "OK");
                await Navigation.PopAsync();
            }
    
        }
    }
}