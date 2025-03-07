using OrderDispatchKata.Domain;

namespace OrderDispatchKata.Repository;

public interface OrderRepository
{
    void save(Order order);

    Order getById(int orderId);
}