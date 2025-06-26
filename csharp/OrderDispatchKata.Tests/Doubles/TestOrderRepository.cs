using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;

namespace OrderDispatchKata.Tests.Doubles
{
    public class TestOrderRepository : OrderRepository
    {
        private Order? savedOrder;
    
        public override void save(Order order)
        {
            savedOrder = order;
            base.save(order);
        }
    
        public Order? getSavedOrder()
        {
            return savedOrder;
        }
    
        public void addOrder(Order order)
        {
            // Store the order but don't mark it as "saved" for test purposes
            base.save(order);
        }
        
        public void clearSavedOrder()
        {
            savedOrder = null;
        }
    }
}