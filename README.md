# Hotel API
The booking API for the very last hotel in Cancun.

## How is it structured

The solution is divided in 5 projects:

 - HotelApi.App - The main project
 - HotelApi.Service
 - HotelApi.Infra
 - HotelApi.Domain
 - HotelApi.Tests

## Database

By default, the project is configured to store data only in memory. If the project is stopped and restarted all data is lost. But it is possible to run it with MongoDB.
To do that, follow these steps:

 1. In the file ConfigurationModule.cs at the HotelApi.Infra project, switch the commented line for the uncommented line:
	 From:
	```cs
	services.AddSingleton<IBookingRepository, BookingRepository>();
	//services.AddScoped<IBookingRepository, MongoBookingRepository>();
	```

	To:
	```cs
	//services.AddSingleton<IBookingRepository, BookingRepository>();
	services.AddScoped<IBookingRepository, MongoBookingRepository>();
	```

 2. Check the appsettings.json file to make sure database config is ok. Current parameters are configured to run at a local database:

	```javascript
	"MongoDatabaseSettings": {
		"ConnectionString":  "mongodb://admin:admin@localhost:27017/?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false",
		"DatabaseName":  "hotel_db",
		"BookingsCollectionName":  "Bookings"
	}
	```
		
## Testing

The HotelApi.Tests project is responsible for unit testing of the service layer.
It is also possible to run tests with coverage collection by running
```dotnet test /p:CollectCoverage=true```
