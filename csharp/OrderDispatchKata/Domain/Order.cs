using System.Collections.Generic;

namespace OrderDispatchKata.Domain;

public class Order
{
    private string? currency;
    private int id;
    private List<OrderItem>? items;
    private OrderStatus status;
    private decimal tax;
    private decimal total;

    public decimal getTotal()
    {
        return total;
    }

    public void setTotal(decimal total)
    {
        this.total = total;
    }

    public string? getCurrency()
    {
        return currency;
    }

    public void setCurrency(string currency)
    {
        this.currency = currency;
    }

    public List<OrderItem>? getItems()
    {
        return items;
    }

    public void setItems(List<OrderItem> items)
    {
        this.items = items;
    }

    public decimal getTax()
    {
        return tax;
    }

    public void setTax(decimal tax)
    {
        this.tax = tax;
    }

    public OrderStatus getStatus()
    {
        return status;
    }

    public void setStatus(OrderStatus status)
    {
        this.status = status;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }
}