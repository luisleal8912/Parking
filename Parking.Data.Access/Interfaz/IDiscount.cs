namespace Parking.Data.Access.Implementation
{
    public interface IDiscount
    {
        int Add(Objects.Discount discount);
        Objects.Discount GetByInvoice(string invoice);
    }
}