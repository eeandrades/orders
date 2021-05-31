using Orders.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Repository.Redis
{
    public class ProductRepository : IProductRepository
    {

        private static Product[] SProducts = new Product[]
        {
            new()
            {
                Id =Guid.Parse("1b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Cerveja",
                Price = 7.8m
            },
            new()
            {
                Id =Guid.Parse("2b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Vodka",
                Price = 35.6m
            },
            new()
            {
                Id =Guid.Parse("3b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Picaanha",
                Price = 98
            },
            new()
            {
                Id =Guid.Parse("4b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Maminha",
                Price = 47
            },
            new()
            {
                Id =Guid.Parse("5b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Fraudinha",
                Price = 47
            },
            new()
            {
                Id =Guid.Parse("6b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Contra Filé",
                Price = 69.6m
            },
            new()
            {
                Id =Guid.Parse("7b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Linguiça",
                Price = 12.8m
            },
            new()
            {
                Id =Guid.Parse("8b02f96e-39a0-428c-992f-1e495db615e9"),
                Name = "Linguiça",
                Price = 12.8m
            },
        };

        Task<bool> IProductRepository.Exists(Guid productId)
        {
            return Task.FromResult(SProducts
                .Any(p => p.Id == productId));
        }

        Task<Product> IProductRepository.FindById(Guid productId)
        {
            return Task.FromResult( SProducts
                .Where(p => p.Id == productId)
                .DefaultIfEmpty(Product.Empty)
                .FirstOrDefault());
        }
    }
}
