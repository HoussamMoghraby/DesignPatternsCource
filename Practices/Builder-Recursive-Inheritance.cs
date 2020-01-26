using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DesignPatterns.RecursiveInheritanceBuilder
{

    public abstract class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Price)}: {Price}";
        }

        public class Builder : NutritionFactBuilder<Builder>
        {

        }

        public static Builder New { get { return new Builder(); } }
    }

    public class Product : Item
    {
        public List<string> Recipes { get; set; } = new List<string>();
        public List<string> NutritionFacts { get; set; } = new List<string>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.Append($"{nameof(Recipes)}:")
                .AppendJoin(',', Recipes)
                .AppendLine("");
            sb.Append($"{nameof(NutritionFacts)}:")
                .AppendJoin(',', NutritionFacts)
                .AppendLine("");
            return sb.ToString();
        }
    }

    //public class Ingredient : Item
    //{
    //    public int PortionSize { get; set; }
    //}


    public abstract class ItemBuilder
    {
        protected Product product = new Product();

        public Product Build()
        {
            return product;
        }
    }

    public class ProductBuilder<T> : ItemBuilder where T : ProductBuilder<T>
    {
        public T CreateProduct(string name, int price)
        {
            product.Name = name;
            product.Price = price;
            return (T)this;
        }
    }
    public class RecipeBuilder<T> : ProductBuilder<RecipeBuilder<T>> where T : RecipeBuilder<T>
    {
        public T AddRecipe(string recipe)
        {
            product.Recipes.Add(recipe);
            return (T)this;
        }
    }

    public class NutritionFactBuilder<T> : RecipeBuilder<NutritionFactBuilder<T>> where T : NutritionFactBuilder<T>
    {
        public T AddNutriionFact(string fact)
        {
            product.NutritionFacts.Add(fact);
            return (T)this;
        }
    }


    //internal class Program
    //{

    //    public static void Main(string[] args)
    //    {
    //        var product = Item.New
    //            .CreateProduct("Shawarma", 1244)
    //            .AddRecipe("large")
    //            .AddNutriionFact("calories")
    //            .AddRecipe("small")
    //            .Build();
    //        // Create an item and set it's price and type(Product or Ingredient) and set the Recipe in case it's a product
    //        Console.WriteLine(product.ToString());
    //        Console.ReadLine();
    //    }
    //}
}