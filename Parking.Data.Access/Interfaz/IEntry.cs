namespace Parking.Data.Access.Implementation
{
    public interface IEntry
    {
        int Add(Objects.Entry entry);
        Objects.Entry GetDepartureNull(int vehicleId);
        int Update(Objects.Entry entry);
    }
}