using NUnit.Framework;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Tests.Doubles;
using OrderDispatchKata.UseCase;

namespace OrderDispatchKata.Tests.UseCase;

[TestFixture]
public class OrderShipmentUseCaseTest
{
    [SetUp]
    public void SetUp()
    {
        orderRepository = new TestOrderRepository();
        shipmentService = new TestShipmentService();
        useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
    }

    private TestOrderRepository orderRepository;
    private TestShipmentService shipmentService;
    private OrderShipmentUseCase useCase;

    [Test]
    public void shipApprovedOrder()
    {
        var initialOrder = new Order();
        initialOrder.setId(1);
        initialOrder.setStatus(OrderStatus.APPROVED);
        orderRepository.addOrder(initialOrder);

        var request = new OrderShipmentRequest();
        request.setOrderId(1);

        useCase.run(request);

        Assert.That(orderRepository.getSavedOrder().getStatus(), Is.EqualTo(OrderStatus.SHIPPED));
        Assert.That(shipmentService.getShippedOrder(), Is.EqualTo(initialOrder));
    }

    [Test]
    public void createdOrdersCannotBeShipped()
    {
        var initialOrder = new Order();
        initialOrder.setId(1);
        initialOrder.setStatus(OrderStatus.CREATED);
        orderRepository.addOrder(initialOrder);

        var request = new OrderShipmentRequest();
        request.setOrderId(1);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<OrderCannotBeShippedException>());


        Assert.That(orderRepository.getSavedOrder(), Is.Null);
        Assert.That(shipmentService.getShippedOrder(), Is.Null);
    }

    [Test]
    public void rejectedOrdersCannotBeShipped()
    {
        var initialOrder = new Order();
        initialOrder.setId(1);
        initialOrder.setStatus(OrderStatus.REJECTED);
        orderRepository.addOrder(initialOrder);

        var request = new OrderShipmentRequest();
        request.setOrderId(1);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<OrderCannotBeShippedException>());
    }

    [Test]
    public void shippedOrdersCannotBeShippedAgain()
    {
        var initialOrder = new Order();
        initialOrder.setId(1);
        initialOrder.setStatus(OrderStatus.SHIPPED);
        orderRepository.addOrder(initialOrder);

        var request = new OrderShipmentRequest();
        request.setOrderId(1);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<OrderCannotBeShippedTwiceException>());
    }
}