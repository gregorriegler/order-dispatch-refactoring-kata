namespace OrderDispatchKata.UseCase;

public class SellItemRequest
{
    private string productName;
    private int quantity;

    public void setQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    public void setProductName(string productName)
    {
        this.productName = productName;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public string getProductName()
    {
        return productName;
    }
}