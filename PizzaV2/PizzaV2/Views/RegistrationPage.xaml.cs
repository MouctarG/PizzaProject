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
    public partial class RegistrationPage : ContentPage
    {
        private bool _isEdit;
        public RegistrationPage(bool isEdit)
        {
            InitializeComponent();
            this._isEdit = isEdit;
            if (isEdit)
            {
                init();
            }
        }
        private async void  Button_OnClicked(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                if (string.IsNullOrWhiteSpace(nom.Text) || string.IsNullOrEmpty(nom.Text)
                                                        || string.IsNullOrWhiteSpace(prenom.Text) || string.IsNullOrEmpty(prenom.Text)
                                                        || string.IsNullOrWhiteSpace(email.Text) || string.IsNullOrEmpty(email.Text))
                {
                    await DisplayAlert ("Erreur", "Veuillez saisir les champs obligatoire", "OK");
                }
                else
                {
                    UserUpdate user = new UserUpdate(email.Text, prenom.Text, nom.Text, phone.Text);

                    await Update(user);
                }
            }

            else
            {


                if (string.IsNullOrWhiteSpace(nom.Text) || string.IsNullOrEmpty(nom.Text)
                                                        || string.IsNullOrWhiteSpace(prenom.Text) ||
                                                        string.IsNullOrEmpty(prenom.Text)
                                                        || string.IsNullOrWhiteSpace(mdp.Text) ||
                                                        string.IsNullOrEmpty(mdp.Text)
                                                        || string.IsNullOrWhiteSpace(email.Text) ||
                                                        string.IsNullOrEmpty(email.Text))
                {
                    await DisplayAlert("Erreur", "Veuillez saisir les champs obligatoire", "OK");
                }
                else
                {
                    UserRegister user = new UserRegister(email.Text, prenom.Text, nom.Text, phone.Text,
                        mdp.Text);
                    await Inscription(user);


                }
            }


        }
        private async Task  Inscription(UserRegister user)
        {
            waitLayout.IsVisible = true;
            bool b = await App.PizzaManager.Insription(user);
            if (b)
            {
                waitLayout.IsVisible = false;
                await DisplayAlert ("Inscrit", "Votre compte est créer", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                waitLayout.IsVisible = false;
                await DisplayAlert ("Erreur", "Impossible de creer le compte " +
                                              "login déja existant", "OK");
            }
        }

        private async Task  Update( UserUpdate user)
        {
            Barrel.ApplicationId = Constant.KEY_LOGIN;
            Login login = App.PizzaManager.GetLoginInProperties();
            if (login != null)
            {
           

                        string token_2 = await App.PizzaManager.GetAuthentificationToken(login);
                        bool b = await App.PizzaManager.UpdateProfil(user,token_2);
                        if (b)
                        {
                         
                            await DisplayAlert ("Profil mis à jour", "Votre profil est bien mis " +
                                                                     "à jour", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Erreur", "Impossible de creer le compte " +
                                                         "login déja existant", "OK");
                        }
                
                }


        }

        private async void init()
        {
            Barrel.ApplicationId = Constant.KEY_LOGIN;
            Login login = App.PizzaManager.GetLoginInProperties();
            if (login != null)
            {
                string token = await App.PizzaManager.GetAuthentificationToken(login);
                GetUser getUser = await App.PizzaManager.GetUser(token);
                if (getUser.is_success)
                {
                    phone.Text = getUser.data.phone_number;
                    nom.Text = getUser.data.last_name;
                    prenom.Text = getUser.data.first_name;
                    email.Text = getUser.data.email;
                    mdp.IsEnabled = false;
                }
            }
        }
    }
}