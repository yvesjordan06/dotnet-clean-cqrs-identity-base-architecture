using ProxyGas.Domain.Exceptions;
using ProxyGas.Domain.Helpers;
using ProxyGas.Domain.Validators.Products;

namespace ProxyGas.Domain.Aggregates.Products;

public class Product
{
    
    private Product()
    {
        
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
   
   
   /// <summary>
   ///  This method is used to create a new product
   /// </summary>
   /// <param name="name"></param>
   /// <param name="description"></param>
   /// <param name="image"></param>
   /// <param name="price"></param>
   /// <returns>
   /// The newly created product
   /// </returns>
   ///
   /// <exception cref="DomainValidationException">
   /// Thrown if the product is invalid
   /// </exception>
    public static Product Create(string name, string description, string image, decimal price)
    {
        var validator = new ProductValidator();
        
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        return DomainValidator.ValidateAndThrow(product, validator.Validate(product));
    }
}