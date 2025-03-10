﻿using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;

namespace OrderDispatchKata.UseCase;

public class OrderApprovalUseCase
{
    private readonly OrderRepository orderRepository;

    public OrderApprovalUseCase(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public void run(OrderApprovalRequest request)
    {
        var order = orderRepository.getById(request.getOrderId());

        if (order.getStatus().Equals(OrderStatus.SHIPPED)) throw new ShippedOrdersCannotBeChangedException();

        if (request.isApproved() && order.getStatus().Equals(OrderStatus.REJECTED))
            throw new RejectedOrderCannotBeApprovedException();

        if (!request.isApproved() && order.getStatus().Equals(OrderStatus.APPROVED))
            throw new ApprovedOrderCannotBeRejectedException();

        order.setStatus(request.isApproved() ? OrderStatus.APPROVED : OrderStatus.REJECTED);
        orderRepository.save(order);
    }
}