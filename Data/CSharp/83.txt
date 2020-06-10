using System;
using System.Collections.Generic;
using System.Linq;

namespace Company {
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
  public static class Db {

    private static List<Product> products = new List<Product>(){
        new Product(){ Id = 1, Name= "Avengers End Game" },
        new Product(){ Id = 2, Name= "Wonder Woman" }
    };

    public static IEnumerable<Product> GetProducts() 
    {
      return products.AsEnumerable();
    }

    public static Product GetProductById(int id)
    {
      return products.Find(p => p.Id == id);
    }

    public static Product CreateProduct(string name)
    {
      var newProduct = new Product(){ Id = products.Count + 1, Name = name}; 
      products.Add(newProduct);
      return newProduct;
    }
  } 
}