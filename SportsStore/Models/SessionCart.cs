using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
 using Newtonsoft.Json;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
    public class SessionCart:Cart   // Subclass of Cart
    {

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService < IHttpContextAccessor > ()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;    // Stores the updated state
            return cart;

        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
             base.AddItem(product, quantity);
             Session.SetJson("Cart", this);  // Stores the updated state
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);    // Stores the updated state
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
