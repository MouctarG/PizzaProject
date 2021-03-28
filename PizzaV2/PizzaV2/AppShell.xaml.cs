using PizzaV2.ViewModels;
using PizzaV2.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PizzaV2
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void GoToLogOut(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new LoginPage());
        }

        private async void GoToMapPage(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new MapPage());
        }

        private async void GoToEditPassword(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new EditPasswordPage());
        
        }

        private async void GoToRegistration(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new RegistrationPage(false));
        }

        private async void GoToOrders(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new OrdersPage());
        }

        private async void GoToConnect(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new LoginPage());
        }

        private  async void GoToEditProfile(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new RegistrationPage(true));
        }
    }
}
