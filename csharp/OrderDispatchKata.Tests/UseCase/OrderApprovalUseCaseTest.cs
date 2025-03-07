using NUnit.Framework;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Tests.Doubles;
using OrderDispatchKata.UseCase;

namespace OrderDispatchKata.Tests.UseCase;

[TestFixture]
public class OrderApprovalUseCaseTest
{
    [SetUp]
    public void setUp()
    {
        orderRepository = new TestOrderRepository();
        useCase = new OrderApprovalUseCase(orderRepository);
    }

    private TestOrderRepository orderRepository;
    private OrderApprovalUseCase useCase;

    [Test]
    public void approvedExistingOrder()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.CREATED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(true);

        useCase.run(request);

        var savedOrder = orderRepository.getSavedOrder();
        Assert.That(savedOrder.getStatus(), Is.EqualTo(OrderStatus.APPROVED));
    }

    [Test]
    public void rejectedExistingOrder()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.CREATED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(false);

        useCase.run(request);

        var savedOrder = orderRepository.getSavedOrder();
        Assert.That(savedOrder.getStatus(), Is.EqualTo(OrderStatus.REJECTED));
    }

    [Test]
    public void cannotApproveRejectedOrder()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.REJECTED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(true);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<RejectedOrderCannotBeApprovedException>());
    }

    [Test]
    public void cannotRejectApprovedOrder()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.APPROVED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(false);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<ApprovedOrderCannotBeRejectedException>());
    }

    [Test]
    public void shippedOrdersCannotBeApproved()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.SHIPPED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(true);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<ShippedOrdersCannotBeChangedException>());
    }

    [Test]
    public void shippedOrdersCannotBeRejected()
    {
        var initialOrder = new Order();
        initialOrder.setStatus(OrderStatus.SHIPPED);
        initialOrder.setId(1);
        orderRepository.addOrder(initialOrder);

        var request = new OrderApprovalRequest();
        request.setOrderId(1);
        request.setApproved(false);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<ShippedOrdersCannotBeChangedException>());
    }
}