using Dapper;
using Discount.API.Entites;
using Npgsql;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                 (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                ("INSERT INTO COUPON (ProductName , Descrption , Amount) Values (@ProductName , @Description ,@Amount)",
                new Coupon { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
                );
            if(affected == 0)
            {
                return false; ;
            }
            return true;    
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon where ProductName = @ProductName", new { ProductName = productName });

            if (coupon == null) 
                return new Coupon
                {
                    ProductName = "No Discount", Amount =0, Description="No Discount DESC"
                };
             
            
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
             (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                ("UPDATE  COUPON Set ProductName= @ProductName , Descrption = @Description  , Amount =@Amount where ID = @Id)",
                new Coupon { Id= coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount  }
                );
            if (affected == 0)
            {
                return false; ;
            }
            return true;
        }
    }
}
