import OrderRepository from "../repository/OrderRepository";
import ShipmentService from "../service/ShipmentService";
import OrderShipmentRequest from "./OrderShipmentRequest";
import OrderCannotBeShippedError from "./OrderCannotBeShippedError";
import OrderCannotBeShippedTwiceError from "./OrderCannotBeShippedTwiceError";
import OrderStatus from "../domain/OrderStatus";

export default class OrderShipmentUseCase {
    private readonly _orderRepository: OrderRepository;
    private readonly _shipmentService: ShipmentService;

    constructor(orderRepository: OrderRepository, shipmentService: ShipmentService) {
        this._orderRepository = orderRepository;
        this._shipmentService = shipmentService;
    }

    public run(request: OrderShipmentRequest): void {
        const order = this._orderRepository.getById(request.orderId);

        if (order.status === OrderStatus.CREATED || order.status === OrderStatus.REJECTED) {
            throw new OrderCannotBeShippedError();
        }

        if (order.status == OrderStatus.SHIPPED) {
            throw new OrderCannotBeShippedTwiceError();
        }

        this._shipmentService.ship(order);

        order.status = OrderStatus.SHIPPED;
        this._orderRepository.save(order);
    }

}