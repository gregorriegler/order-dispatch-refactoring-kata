namespace OrderDispatchKata.Domain;

public class Product
{
    private Category? category;
    private string? name;
    private decimal price;

    public string? getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public decimal getPrice()
    {
        return price;
    }

    public void setPrice(decimal price)
    {
        this.price = price;
    }

    public Category? getCategory()
    {
        return category;
    }

    public void setCategory(Category category)
    {
        this.category = category;
    }
}