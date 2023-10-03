using MongoDB.Driver;
using System.Text.Json.Serialization;
using System.Text.Json;
using ArtClub.AdminAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection(nameof(MongoDbSettings)).Bind(mongoDbSettings);
var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoDbSettings.DatabaseName));
builder.Services.AddScoped<ILearningPathProvider, MongoDbLearningPathProvider>(s => new MongoDbLearningPathProvider(s.GetService<IMongoDatabase>(), mongoDbSettings.LearningPathesCollectionName));


var app = builder.Build();

app.MapGet("/api/learningpath", (ILearningPathProvider learningPathProvider) => learningPathProvider.GetLearningPaths());
app.MapGet("/api/learningpath/{id}", (string id, ILearningPathProvider learningPathProvider) => learningPathProvider.GetLearningPathTranslations(id));

app.Run();
