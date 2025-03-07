using System.Collections.Generic;
using System.Linq;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;

namespace OrderDispatchKata.Tests.Doubles;

public class TestOrderRepository : OrderRepository
{
    private Order insertedOrder;
    private readonly List<Order> orders = new();

    public void save(Order order)
    {
        insertedOrder = order;
    }

    public Order getById(int orderId)
    {
        return orders.FirstOrDefault(o => o.getId() == orderId);
    }

    public Order getSavedOrder()
    {
        return insertedOrder;
    }

    public void addOrder(Order order)
    {
        orders.Add(order);
    }
}