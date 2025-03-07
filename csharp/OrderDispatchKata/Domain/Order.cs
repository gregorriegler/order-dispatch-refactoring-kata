using System.Collections.Generic;
using Deveel.Math;

namespace OrderDispatchKata.Domain;

public class Order
{
    private string currency;
    private int id;
    private List<OrderItem> items;
    private OrderStatus status;
    private BigDecimal tax;
    private BigDecimal total;

    public BigDecimal getTotal()
    {
        return total;
    }

    public void setTotal(BigDecimal total)
    {
        this.total = total;
    }

    public string getCurrency()
    {
        return currency;
    }

    public void setCurrency(string currency)
    {
        this.currency = currency;
    }

    public List<OrderItem> getItems()
    {
        return items;
    }

    public void setItems(List<OrderItem> items)
    {
        this.items = items;
    }

    public BigDecimal getTax()
    {
        return tax;
    }

    public void setTax(BigDecimal tax)
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