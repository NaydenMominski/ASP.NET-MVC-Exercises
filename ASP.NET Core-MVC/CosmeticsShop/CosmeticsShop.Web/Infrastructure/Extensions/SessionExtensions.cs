namespace CosmeticsShop.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public static class SessionExtensions
    {
        private const string ShopingCartId = "Shopping_Cart_Id";

        public static string GetShoppinCartId(this ISession session)
        {
            var shoppingCartId = session.GetString(ShopingCartId);

            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();
                session.SetString(ShopingCartId, shoppingCartId);
            }

            return shoppingCartId;
        }
    }
}
