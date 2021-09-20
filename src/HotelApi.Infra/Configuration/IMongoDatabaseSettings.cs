namespace HotelApi.Infra.Configuration
{
    public interface IMongoDatabaseSettings
    {
        string BookingsCollectionName { get; }
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}