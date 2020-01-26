using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DesignPatterns.FunctionalBuilder
{
    //Functional Builders using extension methods

    public class Product
    {
        public string Name { get; set; }
        public Decimal Price { get; set; }

        public List<string> Recipes = new List<string>();

        public override string ToString()
        {
            return $"{nameof(Name)}:{Name}, {nameof(Price)}: {Price} \n{nameof(Recipes)}: {string.Join(',', Recipes)}";
        }
    }

    public class ProductBuilder
    {
        public List<Action<Product>> Actions = new List<Action<Product>>();

        public ProductBuilder Create(string name, int price)
        {
            Actions.Add(p =>
            {
                p.Name = name;
                p.Price = price;
            });
            return this;
        }
        public Product Build()
        {
            var product = new Product();
            //Execute actions
            Actions.ForEach(a => a(product));
            return product;
        }
    }

    public static class ProductBuilderExtentions
    {
        public static ProductBuilder AddRecipe(this ProductBuilder productBuilder, string recipe)
        {
            productBuilder.Actions.Add(a => a.Recipes.Add(recipe));
            return productBuilder;
        }
    }
    //internal class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var product = new ProductBuilder()
    //            .Create("Quinawa", 223)
    //            .AddRecipe("Large")
    //            .AddRecipe("Small")
    //            .Build();
    //        Console.WriteLine(product.ToString());
    //        Console.ReadLine();
    //    }
    //}
}