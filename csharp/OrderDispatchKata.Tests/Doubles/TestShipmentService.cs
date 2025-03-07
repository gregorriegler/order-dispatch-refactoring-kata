using OrderDispatchKata.Domain;
using OrderDispatchKata.Service;

namespace OrderDispatchKata.Tests.Doubles;

public class TestShipmentService : ShipmentService
{
    private Order shippedOrder;

    public void ship(Order order)
    {
        shippedOrder = order;
    }

    public Order getShippedOrder()
    {
        return shippedOrder;
    }
}