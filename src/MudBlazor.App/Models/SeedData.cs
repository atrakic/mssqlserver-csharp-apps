using Microsoft.EntityFrameworkCore;

using My.Data;

namespace My.Models;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context is null)
        {
            throw new System.ArgumentNullException(nameof(context));
        }

        if (context.Customers.Any())
        {
            return; // DB has been seeded
        }

        Console.WriteLine("Seeding the database.");

        context.Customers.AddRange(
            new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "foo@bar.com"
            }
        );

        context.Products.AddRange(
            new Product
            {
                Name = "Product 1",
                Price = 10.00m
            },
            new Product
            {
                Name = "Product 2",
                Price = 20.00m
            },
            new Product
            {
                Name = "Product 3",
                Price = 30.00m
            }
        );

        context.Orders.AddRange(
            new Order
            {
                CustomerId = 1,
                OrderPlaced = System.DateTime.Now,
            }
        );

        // TODO: ensure FOREIGN KEY constraint https://docs.microsoft.com/en-us/ef/core/modeling/relationships#foreign-key-constraints
        /*
        context.OrderDetails.AddRange(
            new OrderDetail
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            },
            new OrderDetail
            {
                OrderId = 1,
                ProductId = 2,
                Quantity = 2
            },
            new OrderDetail
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 3
            }
        );
        */

        context.SaveChanges();
    }
}
