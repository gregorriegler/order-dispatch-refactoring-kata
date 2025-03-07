using OrderDispatchKata.Domain;

namespace OrderDispatchKata.Service;

public interface ShipmentService
{
    void ship(Order order);
}