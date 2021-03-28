using PizzaV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using PizzaIllico.Config;
using PizzaIllico.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //   this.BindingContext = new LoginViewModel();
            Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));
            Routing.RegisterRoute("../AppShell", typeof(AppShell));
        }

        private async void Button_OnClicked_Login(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.login.Text) || 
                string.IsNullOrWhiteSpace(this.login.Text)
                || string.IsNullOrEmpty(this.mdp.Text) || 
                string.IsNullOrWhiteSpace(this.mdp.Text) )
            {
                await DisplayAlert("Alert", "Login ou Mot de passe incorrecte", "OK");
            }
            else
            {
                
                Barrel.ApplicationId = Constant.KEY_LOGIN;
                Login login = new Login(this.login.Text,mdp.Text);


                GetLoginData loginData = await App.PizzaManager.GetTokenLogin(login);

                if (loginData != null)
                {
                    if (Barrel.Current.Exists(login.login))
                    {
                        App.PizzaManager.SaveLoginInProperties(login);
                        await DisplayAlert("Alert", "Existe", "OK");
                        // await Navigation.PushAsync(new MainMenuPage());
                        // await Navigation.PushAsync(new MainMenuPage());

                        await Navigation.PopAsync();
           
                        
                    }
                    else
                    {
                        App.PizzaManager.SaveLoginInProperties(login);
                        await DisplayAlert("Alert", "Nouveau creer", "OK");
                        await App.PizzaManager.GetAuthentificationToken(login);
                       // await Navigation.PopAsync();
                       await Navigation.PopAsync();
                     

                    }




                }
                else
                {
                    await DisplayAlert("Alert", "Login ou Mot de passe incorrecte", "OK");
                }


                
            }

        }

    private void Button_OnClicked_Inscription(object sender, EventArgs e)
    {
    Navigation.PushAsync(new RegistrationPage(false));
    }


}

}