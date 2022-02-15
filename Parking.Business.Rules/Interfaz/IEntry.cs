namespace Parking.Business.Rules.Implementation
{
    public interface IEntry
    {
        int Add(Data.Objects.Entry entry);
        Data.Objects.Entry GetDepartureNull(int vehicleId);
        int Update(Data.Objects.Entry entry);
    }
}