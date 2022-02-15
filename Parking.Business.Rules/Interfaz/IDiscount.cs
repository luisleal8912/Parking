namespace Parking.Business.Rules.Implementation
{
    public interface IDiscount
    {
        int Add(Data.Objects.Discount discount);
        Data.Objects.Discount GetByInvoice(string invoice);
    }
}