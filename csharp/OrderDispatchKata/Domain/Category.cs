namespace OrderDispatchKata.Domain;

public class Category
{
    private string? name;
    private decimal taxPercentage;

    public string? getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public decimal getTaxPercentage()
    {
        return taxPercentage;
    }

    public void setTaxPercentage(decimal taxPercentage)
    {
        this.taxPercentage = taxPercentage;
    }
}