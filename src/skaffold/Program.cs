using skaffold.Data;
using skaffold.Models;

DemoContext context = new DemoContext();

foreach (Customer c in context.Customers)
{
    Console.WriteLine($"Name: {c.CustomerInformation}");
}
