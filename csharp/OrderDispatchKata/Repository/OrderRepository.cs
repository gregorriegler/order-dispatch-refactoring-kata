using System.Collections.Generic;
using OrderDispatchKata.Domain;

namespace OrderDispatchKata.Repository;

public class OrderRepository
{
    public static OrderRepository Instance { get; set; } = new OrderRepository();

    private Dictionary<int, Order> orders = new();
    
    public void save(Order order)
    {
        orders[order.getId()] = order;
    }

    public Order getById(int orderId)
    {
        if (!orders.ContainsKey(orderId))
        {
            return null;
        }
        return orders[orderId];
    }
}