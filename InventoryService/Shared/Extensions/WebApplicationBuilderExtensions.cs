using InventoryService.Features.Product;
using InventoryService.Features.Product.Abstractions;
using InventoryService.Features.Reservation;
using InventoryService.Features.Reservation.Abstractions;
using InventoryService.Shared.Infrastructure;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Shared.Abstractions;

namespace InventoryService.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void SetupMongoDb(this WebApplicationBuilder builder)
    {
        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();

            cm.MapIdProperty(p => p.Id)
                .SetIdGenerator(GuidGenerator.Instance)
                .SetSerializer(new GuidSerializer(BsonType.Binary))
                .SetElementName("product_id");

            cm.MapProperty(p => p.Name).SetElementName("product_name");
        });
        BsonClassMap.RegisterClassMap<Reservation>(cm =>
        {
            cm.AutoMap();

            cm.MapIdProperty(p => p.Id)
                .SetIdGenerator(GuidGenerator.Instance)
                .SetSerializer(new GuidSerializer(BsonType.Binary))
                .SetElementName("reservation_id");

            cm.MapProperty(p => p.OrderId).SetElementName("order_id");
            cm.MapProperty(p => p.Items).SetElementName("order_id"); //TODO

            cm.MapProperty(p => p.Status)
                .SetSerializer(new EnumSerializer<ReservationStatus>(BsonType.String))
                .SetElementName("reservation_status");
        });

        var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
        var databaseName = builder.Configuration["MongoDb:DatabaseName"];

        var mongoClient = new MongoClient(mongoConnectionString);
        builder.Services.AddSingleton<IMongoClient>(mongoClient);
        builder.Services.AddScoped<IMongoDatabase>(_ => mongoClient.GetDatabase(databaseName));

        builder.Services.AddScoped<DatabaseContext>();
        builder.Services.AddScoped<IUnitOfWorkProvider>(sp => sp.GetRequiredService<DatabaseContext>());
        
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
    }
}