namespace OrderDispatchKata.UseCase;

public class OrderApprovalRequest
{
    private bool approved;
    private int orderId;

    public void setOrderId(int orderId)
    {
        this.orderId = orderId;
    }

    public int getOrderId()
    {
        return orderId;
    }

    public void setApproved(bool approved)
    {
        this.approved = approved;
    }

    public bool isApproved()
    {
        return approved;
    }
}