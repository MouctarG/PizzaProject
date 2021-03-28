using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaIllico.Models;
using PizzaV2.Models;
using PizzaV2.ViewModels;

namespace PizzaIllico.Services
{
    public class PizzaManager
    {
        private IPizzaService _service;

        public PizzaManager(IPizzaService pizzaService)
        {
            this._service = pizzaService;
        }

        public Task<GetLoginData> GetTokenLogin(Login login)
        {
            return _service.GetTokenLogin(login);
        }

        public void GetAllPizzaria(Action<List<ItemPizzaria>> action)
        {
            _service.GetAllPizzaria(action);
        }

        public void GetAllPizzaByShop(Action<List<ItemPizza>> action, int id)
        {
            _service.GetAllPizzaByShop(action, id);
        }

        public Task<bool> Insription(UserRegister user)
        {
            return _service.Inscription(user);
        }

        public Task<bool> UpdatePassword(UpdatePassword updatePassword, string token)
        {
            return _service.UpdatePassword(updatePassword, token);
        }

        public List<ItemPanier> GetPizzaInPanier()
        {
            return _service.GetPizzaInPanier();
        }

        public void SavePizzaInPanier(ItemPanier itemPanier)
        {
            _service.SavePizzaInPanier(itemPanier);
        }

        public List<ItemOrder> GetOrders()
        {
            return _service.GetOrders();
        }

        public void SaveOrder()
        {
            _service.SaveOrder();
        }

        public Task<GetLoginData> RefreshToken(string token)
        {
            return _service.RefreshToken(token);
        }

        public Task<string> GetAuthentificationToken(Login login)
        {
            return _service.GetAuthentificationToken(login);
        }


        public Login GetLoginInProperties()
        {

            return _service.GetLoginInProperties();
        }

        public void SaveLoginInProperties(Login login)
        {
            _service.SaveLoginInProperties(login);
        }

        public void SortPizzarias(List<ItemPizzaria> pizzerias)
        {
            _service.SortPizzarias(pizzerias);
        }

        public Task<GetUser> GetUser(string token)
        {
            return _service.GetUser(token);
        }

        public Task<bool> UpdateProfil(UserUpdate userUpdate, string token)
        {
            return _service.UpdateProfil(userUpdate, token);


        }
    }
}