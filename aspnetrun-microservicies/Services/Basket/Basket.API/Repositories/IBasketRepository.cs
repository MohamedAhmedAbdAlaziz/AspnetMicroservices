﻿using Basket.API.Entites;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);

        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        
        Task DeleteBasket(string userName);
    }
}
