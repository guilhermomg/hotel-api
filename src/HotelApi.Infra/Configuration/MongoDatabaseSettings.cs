namespace HotelApi.Infra.Configuration
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string BookingsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}