using PizzaIllico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using PizzaIllico.Config;
using PizzaIllico.Services;
using PizzaV2.Models;
using PizzaV2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPizzaPage : ContentPage
    {
        public ItemPizza pizza;
        public DetailsPizzaPage(ItemPizza pizza)
        {
            this.pizza = pizza;
            InitializeComponent();

            name.Text = pizza.name;
            description.Text = pizza.description;
            price.Text = pizza.price + " €";
            image.Source = pizza.imageUrl;

  
        }
        
           private async  void Button_OnClicked_Commander(object sender, EventArgs e)
           {
               
               Barrel.ApplicationId = Constant.KEY_LOGIN;
               Login login = App.PizzaManager.GetLoginInProperties();
               if (login!=null)
               {
                   int qteValue = Convert.ToInt32(Qte.Text);
                   if (qteValue>0)
                   {

                       App.PizzaManager.SavePizzaInPanier(new ItemPanier(pizza,qteValue));
                       App.PizzaManager.SaveOrder();
                       await DisplayAlert("Commande", "Commande éffectuée", "OK");

                       await Navigation.PopAsync();
                   }
                   else
                   {
                       await DisplayAlert("Erreur", "Veuillez saisir une qauntité positif", "OK");
                   }
                
               }
               else
               {
                   await DisplayAlert("Alert", "Veuillez vous connecter ou vous inscrire", "OK");
                   await Navigation.PopAsync();
               }
              
           }
        
                private async void Button_OnClicked_AddPanier(object sender, EventArgs e)
                {
                    int qteValue = Convert.ToInt32(Qte.Text);
                    if (qteValue<=0)
                        await DisplayAlert("Erreur", "Veuillez saisir une qauntité positif", "OK");
                    else
                    {
                        App.PizzaManager.SavePizzaInPanier(new ItemPanier(pizza,qteValue));
                       await Navigation.PushAsync(new PanierPage());

                    }   
                 
                }
    }
}