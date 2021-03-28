
using PizzaV2.Views;
using System;
using PizzaIllico.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PizzaV2
{
    public partial class App : Application
    {
        public static PizzaManager PizzaManager { get; private set; }

        public App()
        {
            InitializeComponent();

      
           MainPage = new AppShell();
      
            PizzaManager = new PizzaManager (new PizzaService());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
