using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using Newtonsoft.Json;
using PizzaIllico.Config;
using PizzaIllico.Models;
using PizzaV2;
using PizzaV2.Models;
using PizzaV2.ViewModels;
using Xamarin.Forms;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PizzaIllico.Services
{
    public class PizzaService : IPizzaService
    {

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public PizzaService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public List<Pizza> pizzas { get; private set; }

        public Task<List<Pizza>> GetPizzasAsync(string res)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Inscription(UserRegister user)
        {
            Uri uri = new Uri(Constant.URL_USER_INSCRIPTION);

            try
            {

                string json = JsonSerializer.Serialize<UserRegister>(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                string contentResponse = await response.Content.ReadAsStringAsync();

    
                GetLoginData code =  JsonConvert.DeserializeObject<GetLoginData>(contentResponse);

                return code.is_success;


               
            }
            catch (Exception ex)
            {


                return false;
            }
            return false;
       
        }

        public async Task<GetLoginData> GetTokenLogin(Login login)
        {
            Uri uri = new Uri(Constant.URL_LOGIN);
            GetLoginData loginData = null;

            try
            {

                string json = JsonSerializer.Serialize<Login>(login, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                string contentResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
               
                    loginData=  JsonConvert.DeserializeObject<GetLoginData>(contentResponse);

             
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
                else return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                
                Debug.WriteLine("IMPOSSIBLE Connect", ex.Message);
            }

            return loginData;
        }

        public void GetAllPizzaria(Action<List<ItemPizzaria>> action)
        {
         List<ItemPizzaria> itemPizzarias = new List<ItemPizzaria>();

            using (var webclient = new WebClient())
            {

                Console.WriteLine("ETAPE 2");

                webclient.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) =>
                {
              
                    Console.WriteLine("ETAPE 5");

                    try
                    {
                        string pizzasJson = e.Result;

                        Pizzaria pizzaria = JsonConvert.DeserializeObject<Pizzaria>(pizzasJson);

                    

                        Device.BeginInvokeOnMainThread(() => { action.Invoke(pizzaria.data); });

                    }
                    catch (Exception ex)
                    {
                       
                        Device.BeginInvokeOnMainThread(() =>
                        {
                         
                            action.Invoke(null);
                        });
                    }

                };

             
                webclient.DownloadStringAsync(new Uri(Constant.URL_SHOPS));
            }

        }

        public void GetAllPizzaByShop(Action<List<ItemPizza>> action, int id)
        {

            using (var webclient = new WebClient())
            {
      
                webclient.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) =>
                {
                  
                 

                  
                    try
                    {
                        string pizzasJson = e.Result;

                        Pizza pizza = JsonConvert.DeserializeObject<Pizza>(pizzasJson);
                        

                        Device.BeginInvokeOnMainThread(() => { action.Invoke(pizza.data); });

                    }
                    catch (Exception ex)
                    {
                     
                        Device.BeginInvokeOnMainThread(() =>
                        {
                          
                            action.Invoke(null);
                        });
                    }

                };
                webclient.DownloadStringAsync(new Uri(Constant.URL_SHOPS+"/"+id+"/pizzas"));
            }
        }

        public async Task<bool>  UpdatePassword(UpdatePassword updatePassword,string token)
        {
            Uri uri = new Uri(Constant.URL_UPDATE_PASSWORD);

            try
            {
                
                string json = JsonSerializer.Serialize<UpdatePassword>(updatePassword);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               
               var method = new HttpMethod("PATCH");

               var request = new HttpRequestMessage(method, uri) {
                   Content = new StringContent(
                       JsonConvert.SerializeObject(updatePassword),
                       Encoding.UTF8, "application/json")
               };
                response = await client.SendAsync(request);
          
                string contentResponse = await response.Content.ReadAsStringAsync();
                GetLoginData code =  JsonConvert.DeserializeObject<GetLoginData>(contentResponse);

                return code.is_success;


            }
            catch (Exception ex)
            {
           

                return false;
            }
      

        }

        public List<ItemPanier> GetPizzaInPanier()
        {
            Login login = GetLoginInProperties();
            if (login!=null)
            {
                if (Application.Current.Properties.ContainsKey(login.login))
                {
                    string value = Application.Current.Properties[login.login].ToString();

                    List<ItemPanier> itemPaniers = JsonConvert.DeserializeObject<List<ItemPanier>>(value);
                    List<ItemPanier> nwList = new List<ItemPanier>();
                    foreach (var var in itemPaniers)
                    {
                        if (var.Pizza!=null)
                        {
                            nwList.Add(var);   
                        }
                    
                    }
                    return nwList;

                }

            }
          
            return new List<ItemPanier>();
        }

        public void SavePizzaInPanier(ItemPanier itemPanier)
        {
            Login login = GetLoginInProperties();
            if (login!=null)
            {
                List<ItemPanier> list = GetPizzaInPanier();
                List<ItemPanier> newList = new List<ItemPanier>();
                bool b = false;
                if (list.Any())
                {
                    foreach (var pz in list)
                    {
                        if (pz.Pizza.name.Equals(itemPanier.Pizza.name))
                        {
                            pz.Quantite = pz.Quantite + itemPanier.Quantite;
                            pz.Pizza.price += itemPanier.Pizza.price * itemPanier.Quantite;
                            b = true;
                        }
                 

                      
                       
                        newList.Add(pz);
                    }
                }

                if (!b)
                {
                    itemPanier.Pizza.price = itemPanier.Pizza.price * itemPanier.Quantite;
                    newList.Add(itemPanier);

                }
            

        
                string key = login.login;
                string valueJson = JsonConvert.SerializeObject(newList);
            
                Application.Current.Properties[key] = valueJson;
                Application.Current.SavePropertiesAsync();
            }
          
        }

        public List<ItemOrder> GetOrders()
        {
            Login login = GetLoginInProperties();
            if (login!=null)
            {
                if (Application.Current.Properties.ContainsKey(login.login))
                {
                    string value = Application.Current.Properties[login.login].ToString();

                    List<ItemOrder> itemOrders = JsonConvert.DeserializeObject<List<ItemOrder>>(value);
                    return itemOrders;

                }

            }
          
        

            return new List<ItemOrder>();
        }

        public void SaveOrder()
        {
            List<ItemOrder> list = GetOrders();
            Login login = GetLoginInProperties();
            if (login!=null)
            {
              
                    List<ItemPanier> listItemPanier = GetPizzaInPanier();
                    ItemOrder it = new ItemOrder();
                    it.Pizzas = "";
                    it.Price = 0;
                    it.OrderDate = DateTime.Now;
                    foreach (var itPanier in listItemPanier)
                    {
                        it.Pizzas = it.Pizzas + " - " + itPanier.Pizza.name;
                        it.Price += itPanier.Pizza.price;
                    }
                    list.Add(it);
                    string valueJsonPanier = JsonConvert.SerializeObject(new List<ItemPanier>());
                   
                    Application.Current.Properties[login.login] = valueJsonPanier;

            }
            
                string key = login.login;
                string valueJson = JsonConvert.SerializeObject(list);
            
                Application.Current.Properties[key] = valueJson;
                Application.Current.SavePropertiesAsync();
            
           
        }

    
        public async Task<string> GetAuthentificationToken(Login login)
        {

            try
            {
                var key = login.login;
                //Dev handles checking if cache is expired
                if(!Barrel.Current.IsExpired(key))
                {
                    var   d =Barrel.Current.Get<GetLoginData>(key: key);
                    return d.data.access_token;
                }
                else if (Barrel.Current.Exists(key) && Barrel.Current.IsExpired(key: key))
                {
                    GetLoginData loginDataRes = Barrel.Current.Get<GetLoginData>(key: key);
                    loginDataRes = await RefreshToken(loginDataRes.data.refresh_token);
                    if (loginDataRes != null)
                    {
                        Barrel.Current.Add(key: key, data: loginDataRes, expireIn: TimeSpan.FromSeconds(loginDataRes.data.expires_in));
                        return loginDataRes.data.access_token;
                    }
             
                }


                //Saves the cache and pass it a timespan for expiration
         
    
                GetLoginData dataUser=  await GetTokenLogin(login);
           
                Barrel.Current.Add(key: key, data: dataUser, expireIn: TimeSpan.FromSeconds(5));
                return dataUser.data.access_token;
            }
            catch (Exception e)
            {
                return null;
            }
           
        
          
         

        }
        
        
        
        
        public async Task<GetLoginData> RefreshToken(string token)
        {
            RefreshToken refreshToken = new RefreshToken(token);
            Uri uri = new Uri(Constant.URL_REFRESH_TOKEN);
            GetLoginData loginData = null;

            try
            {

                string json = JsonSerializer.Serialize<RefreshToken>(refreshToken);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                string contentResponse = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                   
                    loginData=  JsonConvert.DeserializeObject<GetLoginData>(contentResponse);

                
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
                else return null;

            }
            catch (Exception ex)
            {
              
            }

            return loginData;
        }

       public Login GetLoginInProperties()
       {
           string key = Constant.KEY_LOGIN;
            if (Application.Current.Properties.ContainsKey(key))
            {
                string value = Application.Current.Properties[key].ToString();

                Login login = JsonConvert.DeserializeObject<Login>(value);
                return login;
            }

            return null;

        }

       public void SaveLoginInProperties(Login login)
       {
           string key = Constant.KEY_LOGIN;
           string valueJson = JsonConvert.SerializeObject(login);
            
           Application.Current.Properties[key] = valueJson;
           Application.Current.SavePropertiesAsync();
       }
       public void SortPizzarias(List<ItemPizzaria> pizzerias)
       {
           if (pizzerias == null ) return;
           pizzerias.Sort((p1, p2) =>
           {
               if (p1 == null) return (p2 == null) ? 1 : 0;      
               else if (p2 == null) return 1;                    
               else
               {
                   double res = p2.minutes_per_kilometer - p1.minutes_per_kilometer;
                   if (res == 0) return 0;
                   else return res < 0 ? 1 : -1;

               };
           });


}

       public async Task<GetUser> GetUser(string token)
       {
           Uri uri = new Uri(Constant.URL_GET_USER);
           GetUser loginData = null;

           try
           {
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               
               HttpResponseMessage response = null;
               response = await client.GetAsync(uri);

               string contentResponse = await response.Content.ReadAsStringAsync();


               if (response.IsSuccessStatusCode)
               {
             
                   loginData=  JsonConvert.DeserializeObject<GetUser>(contentResponse);
                   return loginData;

                
               }
               else return null;

           }
           catch (Exception ex)
           {
              
           }

           return loginData;
       }

       public  async Task<bool> UpdateProfil(UserUpdate userUpdate,string token)
       {
           Uri uri = new Uri(Constant.URL_UPDATE_PROFIL);

           try
           {
                
               string json = JsonSerializer.Serialize<UserUpdate>(userUpdate);
               StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
               HttpResponseMessage response = null;
               client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
               
               var method = new HttpMethod("PATCH");

               var request = new HttpRequestMessage(method, uri) {
                   Content = new StringContent(
                       JsonConvert.SerializeObject(userUpdate),
                       Encoding.UTF8, "application/json")
               };
               response = await client.SendAsync(request);
          
               string contentResponse = await response.Content.ReadAsStringAsync();
               GetUser code =  JsonConvert.DeserializeObject<GetUser>(contentResponse);

               return code.is_success;


           }
           catch (Exception ex)
           {
           

               return false;
           }
       }


    }
    
 
   
    
}

