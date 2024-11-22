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
    builder.Services.AddDbContext<ApplicationContext>();
    builder.Services.AddScoped<IProductRepository, SQLProductRepository>();
    builder.Services.AddScoped<IStoreRepository, SQLStoreRepository>();
}
else
{
    builder.Services.AddScoped<IProductRepository>(provider => new FileProductRepository(builder.Configuration["FilePath:Product"]));
    builder.Services.AddScoped<IStoreRepository>(provider => new FileStoreRepository(builder.Configuration["FilePath:Store"]));
}

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddScoped<StoreService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<StoreProductService>();
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
