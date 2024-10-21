namespace Catalog.Products.Models;

public class Product : Aggregate<Guid>
{
    public string Name { get; private set; }

    public List<string> Category { get; private set; }

    public string Description { get; private set; }

    public string Image { get; private set; }

    public decimal Price { get; private set; }

    private Product()
    {
    }

    private Product(
        Guid id,
        string name,
        List<string> category,
        string description,
        string image,
        decimal price)
    {
        Id = id;
        Name = name;
        Category = category;
        Description = description;
        Image = image;
        Price = price;
    }

    public static Product Create(
        Guid id,
        string name,
        List<string> category,
        string description,
        string image,
        decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var product = new Product(id, name, category, description, image, price);
        product.AddDomainEvent(new ProductCreatedDomainEvent(product));
        return product;
    }

    public void Update(
        string name,
        List<string> category,
        string description,
        string image,
        decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        Name = name;
        Category = category;
        Description = description;
        Image = image;
        Price = price;

        if (Price == price) return;
        Price = price;
        AddDomainEvent(new ProductPriceChangedDomainEvent(this));
    }   
}