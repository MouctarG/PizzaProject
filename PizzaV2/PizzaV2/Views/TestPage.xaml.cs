using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using PizzaIllico.Config;
using PizzaIllico.Models;
using PizzaIllico.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        private PizzaManager _pizzaManager;

        public TestPage()
        {
            InitializeComponent();
            _pizzaManager = new PizzaManager(new PizzaService());
        }

        private async void Button_OnClicked_Login(object sender, EventArgs e)
        {
            Barrel.ApplicationId = Constant.KEY_LOGIN;
            Login login = new Login("paul@123.frdv", "123456");
           

            GetLoginData loginData = await _pizzaManager.GetTokenLogin(login);

            if (loginData!=null)
            {
                if (Barrel.Current.Exists(login.login))
                {
                    await DisplayAlert("Alert", "Existe", "OK");
                   // await Navigation.PushAsync(new MainMenuPage());
                   // await Navigation.PushAsync(new MainMenuPage());
               
                 
                }
                else
                { 
                    await DisplayAlert("Alert", "Nouveau creer", "OK");
                   await _pizzaManager.GetAuthentificationToken(login);
                   
                }
                
               

             
            }
            else
            {
                await DisplayAlert("Alert", "Login ou Mot de passe incorrecte", "OK");
            }
        
        }
    }


}