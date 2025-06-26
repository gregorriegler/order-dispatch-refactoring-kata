using System;
using System.Collections.Generic;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;

namespace OrderDispatchKata.UseCase;

public class OrderCreationUseCase
{
    private readonly OrderRepository orderRepository = OrderRepository.Instance;
    private readonly ProductCatalog productCatalog;

    public OrderCreationUseCase(ProductCatalog productCatalog)
    {
        this.productCatalog = productCatalog;
    }

    public OrderCreationUseCase(OrderRepository orderRepository, ProductCatalog productCatalog)
    {
        this.orderRepository = orderRepository;
        this.productCatalog = productCatalog;
    }

    public void run(SellItemsRequest request)
    {
        var order = new Order();
        order.setStatus(OrderStatus.CREATED);
        order.setItems(new List<OrderItem>());
        order.setCurrency("EUR");
        order.setTotal(0.0m);
        order.setTax(0.0m);

        foreach (var itemRequest in request.getRequests())
        {
            var product = productCatalog.getByName(itemRequest.getProductName());

            if (product == null)
            {
                throw new UnknownProductException();
            }

            var unitaryTax = Math.Round((product.getPrice() / 100m) * product.getCategory()!.getTaxPercentage(), 2, MidpointRounding.AwayFromZero);
            var unitaryTaxedAmount = Math.Round(product.getPrice() + unitaryTax, 2, MidpointRounding.AwayFromZero);
            var taxedAmount = Math.Round(unitaryTaxedAmount * itemRequest.getQuantity(), 2, MidpointRounding.AwayFromZero);
            var taxAmount = unitaryTax * itemRequest.getQuantity();

            var orderItem = new OrderItem();
            orderItem.setProduct(product);
            orderItem.setQuantity(itemRequest.getQuantity());
            orderItem.setTax(taxAmount);
            orderItem.setTaxedAmount(taxedAmount);
            order.getItems()!.Add(orderItem);

            order.setTotal(order.getTotal() + taxedAmount);
            order.setTax(order.getTax() + taxAmount);
        }

        orderRepository.save(order);
    }
}