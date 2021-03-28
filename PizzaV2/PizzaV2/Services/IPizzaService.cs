using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PizzaIllico.Models;
using PizzaV2.Models;
using PizzaV2.ViewModels;

namespace PizzaIllico.Services
{
    public interface IPizzaService
    {
    
        /**
         * Prend en paramètre un utilisation et le sauvegarde dans la base
         * de données
         */
        Task <bool> Inscription(UserRegister user);
        
        /**
         * Cette méthode permets de récuperer le token et le refresh token 
         */
        Task<GetLoginData> GetTokenLogin(Login login);
       /**
        * retourne la liste de tous les pizzerias disponible depuis l'Api
        */
        void  GetAllPizzaria(Action<List<ItemPizzaria>> action);
       /**
        * retourne la liste des pizzas par pizzarias
        */
       void GetAllPizzaByShop(Action<List<ItemPizza>> action, int id);
        
       /**
        * Pour la mis à jour du mot de passe utilisateur
        */
        Task <bool> UpdatePassword(UpdatePassword updatePassword,string token);

       /**
        * Retourne les pizzas dans le panier par utilisateur
        */
        List<ItemPanier> GetPizzaInPanier();
      /**
       * Sauvegarde en local une pizza dans le panier 
       */
        void SavePizzaInPanier(ItemPanier itemPanier);
      /**
       * retourne la les commandes de l'utilisateur 
       */
         List<ItemOrder> GetOrders();
      /**
       * sauvegarde en local une commande
       */
        void SaveOrder();

      /**
       * Permet de faire le refresh token 
       */
        Task<GetLoginData> RefreshToken(string token);
      /**
       * retourne le token ou un nouveau token si celui ci est expiré
       * le token et sa durée de validité est stoké en local
       */
         Task<string> GetAuthentificationToken(Login login);
        /**
         * retoune le login et le mot de passe de l'utilisateur
         */

         Login GetLoginInProperties();
        /**
         * stocke le login en local
         */
         void SaveLoginInProperties(Login login);
        /**
         * effectue le trie de la liste des pizzarias en fonction de la distance
         */

          void SortPizzarias(List<ItemPizzaria> pizzerias);
        /**
         * Pour le mis à jour du profil de l'utilisateur
         * return true si la modification est effectuer
         * sinon faux
         */
          
          Task <bool> UpdateProfil(UserUpdate userUpdate,string token);
        /**
         * retourne le profile d'un utilisateur
         */

          Task<GetUser> GetUser(string token);

    }
}