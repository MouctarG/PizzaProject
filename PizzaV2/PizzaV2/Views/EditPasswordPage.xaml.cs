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
    public partial class EditPasswordPage : ContentPage
    {
        public EditPasswordPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            UpdatePassword();
        }

        private async void UpdatePassword()
        {
            if (string.IsNullOrWhiteSpace(old_password.Text) || string.IsNullOrEmpty(old_password.Text)
                                                             || string.IsNullOrWhiteSpace(new_password.Text) ||
                                                             string.IsNullOrEmpty(new_password.Text))
            {
                await DisplayAlert("Erreur", "Les deux champs sont obligatiore", "OK");
            }
            else
            {
                Barrel.ApplicationId = Constant.KEY_LOGIN;
                Login login = App.PizzaManager.GetLoginInProperties();
                if (login!=null)
                {
                    UpdatePassword updatePassword = new UpdatePassword(old_password.Text, new_password.Text);
                    string token = await  App.PizzaManager.GetAuthentificationToken(login) ;
                    waitLayout.IsVisible = true;
                    if (string.IsNullOrEmpty(token))
                    {
                        await DisplayAlert("Alert", "Veuillez vous connecter à votre", "OK");
                        await Navigation.PopAsync();
                    }
                    bool b = await App.PizzaManager.UpdatePassword(updatePassword, token);

                    if (b)
                    {
                        new_password.Text = "";
                        old_password.Text = "";
                        waitLayout.IsVisible = false;
                        await DisplayAlert("Operation effectuée", "Votre mot de passe est bien modifier", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Veuillez réessayer / attention à la longueur" +
                                                     " du nouveau mot de passe", "OK");
                        waitLayout.IsVisible = false;
                        new_password.Text = "";
                        old_password.Text = "";
                    }
                }
                else
                {
                    
                        await DisplayAlert("Erreur", "Veuillez vous connecter à votre compte", "OK");
                   
                }
              
            }
        }
    }
}