
using Lab3.Reposiories;
using Lab3.Repositories;
using Lab3.Services;

var builder = WebApplication.CreateBuilder(args);

var repositoryType = builder.Configuration["RepositoryType"];
IStoreRepository storeRepository;
if (repositoryType == "Sql")
{
    var dbContext = new ApplicationContext();
    storeRepository = new SQLStoreRepository(dbContext);
}
else
{
    storeRepository = new FileStoreRepository(builder.Configuration["FilePath:Store"]);
}

// Add services to the container.
var storeService = new StoreService(storeRepository);

builder.Services.AddControllers();
builder.Services.AddSingleton(storeService);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
