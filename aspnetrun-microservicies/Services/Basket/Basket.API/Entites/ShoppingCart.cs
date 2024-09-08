namespace Basket.API.Entites
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {

        }
        public ShoppingCart(string UserName)
        {

            this.UserName = UserName;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totoalPrice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    totoalPrice = item.Price * item.Quantity;
                }
                return totoalPrice;
            }

        }
    }
}
