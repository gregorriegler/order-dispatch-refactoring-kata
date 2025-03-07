using Deveel.Math;

namespace OrderDispatchKata.Domain;

public class Product
{
    private Category category;
    private string name;
    private BigDecimal price;

    public string getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public BigDecimal getPrice()
    {
        return price;
    }

    public void setPrice(BigDecimal price)
    {
        this.price = price;
    }

    public Category getCategory()
    {
        return category;
    }

    public void setCategory(Category category)
    {
        this.category = category;
    }
}