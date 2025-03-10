﻿using System.Collections.Generic;
using System.Linq;
using Deveel.Math;
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
        food.setTaxPercentage(new BigDecimal(10));

        var salad = new Product();
        salad.setName("salad");
        salad.setPrice(new BigDecimal(3.56));
        salad.setCategory(food);

        var tomato = new Product();
        tomato.setName("tomato");
        tomato.setPrice(new BigDecimal(4.65));
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

        var insertedOrder = orderRepository.getSavedOrder();
        Assert.That(insertedOrder.getStatus(), Is.EqualTo(OrderStatus.CREATED));
        Assert.That(insertedOrder.getTotal(), Is.EqualTo(new BigDecimal(23.20).SetScale(2, RoundingMode.HalfUp)));
        Assert.That(insertedOrder.getTax(), Is.EqualTo(new BigDecimal(2.13).SetScale(2, RoundingMode.HalfUp)));
        Assert.That(insertedOrder.getCurrency(), Is.EqualTo("EUR"));
        Assert.That(insertedOrder.getItems().Count(), Is.EqualTo(2));
        Assert.That(insertedOrder.getItems()[0].getProduct().getName(), Is.EqualTo("salad"));
        Assert.That(insertedOrder.getItems()[0].getProduct().getPrice(), Is.EqualTo(new BigDecimal(3.56)));
        Assert.That(insertedOrder.getItems()[0].getQuantity(), Is.EqualTo(2));
        Assert.That(insertedOrder.getItems()[0].getTaxedAmount(),
            Is.EqualTo(new BigDecimal(7.84).SetScale(2, RoundingMode.HalfUp)));
        Assert.That(insertedOrder.getItems()[0].getTax(),
            Is.EqualTo(new BigDecimal(0.72).SetScale(2, RoundingMode.HalfUp)));
        Assert.That(insertedOrder.getItems()[1].getProduct().getName(), Is.EqualTo("tomato"));
        Assert.That(insertedOrder.getItems()[1].getProduct().getPrice(), Is.EqualTo(new BigDecimal(4.65)));
        Assert.That(insertedOrder.getItems()[1].getQuantity(), Is.EqualTo(3));
        Assert.That(insertedOrder.getItems()[1].getTaxedAmount(),
            Is.EqualTo(new BigDecimal(15.36).SetScale(2, RoundingMode.HalfUp)));
        Assert.That(insertedOrder.getItems()[1].getTax(),
            Is.EqualTo(new BigDecimal(1.41).SetScale(2, RoundingMode.HalfUp)));
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