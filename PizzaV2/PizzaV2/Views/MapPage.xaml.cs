using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaIllico.Models;
using PizzaIllico.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PizzaV2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private PizzaManager pizzaManager;

        private List<ItemPizzaria> itemPizzarias;
        public MapPage()
        {
            InitializeComponent();
            pizzaManager = new PizzaManager(new PizzaService());
            itemPizzarias = new List<ItemPizzaria>();
            pizzaManager.GetAllPizzaria(pizzarias =>
            {
                
                foreach(var item in pizzarias)
                {
                    itemPizzarias.Add(new ItemPizzaria
                    {
                        name = item.name,
                        address = item.address,
                        // Location = place.geometry.location,
                        Position = new Position(Convert.ToDouble(item.latitude), 
                            Convert.ToDouble( item.longitude))
                        //Icon = place.icon,
                        //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                        //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    });

                }
                MyMap.ItemsSource = itemPizzarias;
               
                
            
                
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.856614,2.3522219), Distance.FromKilometers(100)));
               
    
            });
        }
    }
}