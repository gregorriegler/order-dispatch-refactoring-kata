﻿using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;
using OrderDispatchKata.Service;

namespace OrderDispatchKata.UseCase;

public class OrderShipmentUseCase
{
    private readonly OrderRepository orderRepository;
    private readonly ShipmentService shipmentService;

    public OrderShipmentUseCase(OrderRepository orderRepository, ShipmentService shipmentService)
    {
        this.orderRepository = orderRepository;
        this.shipmentService = shipmentService;
    }

    public void run(OrderShipmentRequest request)
    {
        var order = orderRepository.getById(request.getOrderId());

        if (order.getStatus().Equals(OrderStatus.CREATED) || order.getStatus().Equals(OrderStatus.REJECTED))
            throw new OrderCannotBeShippedException();

        if (order.getStatus().Equals(OrderStatus.SHIPPED)) throw new OrderCannotBeShippedTwiceException();

        shipmentService.ship(order);

        order.setStatus(OrderStatus.SHIPPED);
        orderRepository.save(order);
    }
}