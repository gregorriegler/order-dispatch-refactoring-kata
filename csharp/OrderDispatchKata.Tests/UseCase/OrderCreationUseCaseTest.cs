using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;
using OrderDispatchKata.Tests.Doubles;
using OrderDispatchKata.UseCase;

namespace OrderDispatchKata.Tests.UseCase;

[TestFixture]
public class OrderCreationUseCaseTest
{
    [SetUp]
    public void SetUp()
    {
        orderRepository = new TestOrderRepository();
        food = new Category();
        food.setName("food");
        food.setTaxPercentage(10m);

        var salad = new Product();
        salad.setName("salad");
        salad.setPrice(3.56m);
        salad.setCategory(food);

        var tomato = new Product();
        tomato.setName("tomato");
        tomato.setPrice(4.65m);
        tomato.setCategory(food);

        productCatalog = new InMemoryProductCatalog(
            new List<Product>
            {
                salad, tomato
            });

        useCase = new OrderCreationUseCase(orderRepository, productCatalog);
    }

    private TestOrderRepository orderRepository;
    private Category food;
    private ProductCatalog productCatalog;
    private OrderCreationUseCase useCase;

    [Test]
    public void sellMultipleItems()
    {
        var saladRequest = new SellItemRequest();
        saladRequest.setProductName("salad");
        saladRequest.setQuantity(2);

        var tomatoRequest = new SellItemRequest();
        tomatoRequest.setProductName("tomato");
        tomatoRequest.setQuantity(3);

        var request = new SellItemsRequest();
        request.setRequests(new List<SellItemRequest>());
        request.getRequests().Add(saladRequest);
        request.getRequests().Add(tomatoRequest);

        useCase.run(request);

        var insertedOrder = orderRepository.getSavedOrder()!;
        Assert.That(insertedOrder.getStatus(), Is.EqualTo(OrderStatus.CREATED));
        Assert.That(insertedOrder.getTotal(), Is.EqualTo(23.20m));
        Assert.That(insertedOrder.getTax(), Is.EqualTo(2.13m));
        Assert.That(insertedOrder.getCurrency(), Is.EqualTo("EUR"));
        Assert.That(insertedOrder.getItems()!.Count(), Is.EqualTo(2));
        Assert.That(insertedOrder.getItems()![0].getProduct()!.getName(), Is.EqualTo("salad"));
        Assert.That(insertedOrder.getItems()![0].getProduct()!.getPrice(), Is.EqualTo(3.56m));
        Assert.That(insertedOrder.getItems()![0].getQuantity(), Is.EqualTo(2));
        Assert.That(insertedOrder.getItems()![0].getTaxedAmount(), Is.EqualTo(7.84m));
        Assert.That(insertedOrder.getItems()![0].getTax(), Is.EqualTo(0.72m));
        Assert.That(insertedOrder.getItems()![1].getProduct()!.getName(), Is.EqualTo("tomato"));
        Assert.That(insertedOrder.getItems()![1].getProduct()!.getPrice(), Is.EqualTo(4.65m));
        Assert.That(insertedOrder.getItems()![1].getQuantity(), Is.EqualTo(3));
        Assert.That(insertedOrder.getItems()![1].getTaxedAmount(), Is.EqualTo(15.36m));
        Assert.That(insertedOrder.getItems()![1].getTax(), Is.EqualTo(1.41m));
    }

    [Test]
    public void unknownProduct()
    {
        var request = new SellItemsRequest();
        request.setRequests(new List<SellItemRequest>());
        var unknownProductRequest = new SellItemRequest();
        unknownProductRequest.setProductName("unknown product");
        request.getRequests().Add(unknownProductRequest);

        Assert.That(() => useCase.run(request),
            Throws.TypeOf<UnknownProductException>());
    }
}