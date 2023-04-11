using FluentValidation;
using ProxyGas.Domain.Aggregates.Products;

namespace ProxyGas.Domain.Validators.Products;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The Name is required");
        
        RuleFor(x => x.Description).NotEmpty().WithMessage("The Description is required");
        
        RuleFor(x => x.Price).NotEmpty().WithMessage("The Price is required");
        
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("The Price must be a positive number");
        
        //The image should be a valid URL
        RuleFor(x => x.Image).NotEmpty().WithMessage("The Image is required");
        RuleFor(x => x.Image).Must(x => Uri.IsWellFormedUriString(x, UriKind.Absolute))
            .WithMessage("The Image should be a valid URL in the format http://www.example.com/image.jpg");
        
        RuleFor(x => x.Price).Must(x => x % 25 == 0).WithMessage("The Price should be a multiple of 25");
        
        RuleFor(x => x.CreatedAt).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The CreatedAt Should never be in the future");
        RuleFor(x => x.UpdatedAt).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The UpdatedAt Should never be in the future");
        RuleFor(x => x.UpdatedAt).GreaterThanOrEqualTo(x => x.CreatedAt)
            .WithMessage("The UpdatedAt Should never be older than the CreatedAt");
    }
}