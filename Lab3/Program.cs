using Lab3.Repositories;
using Lab3.Repositories.SQL;
using Lab3.Repositories.File;
using Lab3.Services;

var builder = WebApplication.CreateBuilder(args);

var repositoryType = builder.Configuration["RepositoryType"];
IStoreRepository storeRepository;
IProductRepository productRepository;
if (repositoryType == "Sql")
{
    var dbContext = new ApplicationContext();
    storeRepository = new SQLStoreRepository(dbContext);
    productRepository = new SQLProductRepository(dbContext);

}
else
{
    storeRepository = new FileStoreRepository(builder.Configuration["FilePath:Store"]);
    productRepository = new FileProductRepository(builder.Configuration["FilePath:Product"]);
}

// Add services to the container.
var storeService = new StoreService(storeRepository);
var productService = new ProductService(productRepository);
var storeProductService = new StoreProductService(storeRepository, productRepository);

builder.Services.AddControllers();
builder.Services.AddSingleton(storeService);
builder.Services.AddSingleton(productService);
builder.Services.AddSingleton(storeProductService);
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
