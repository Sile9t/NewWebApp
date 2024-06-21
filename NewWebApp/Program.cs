using Microsoft.EntityFrameworkCore;
using NewWebApp.Data;
using NewWebApp.Graph.Mutation;
using NewWebApp.Graph.Query;
using NewWebApp.Mapper;
using NewWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<StorageContext>(option => option.UseSqlServer(builder
//    .Configuration.GetConnectionString("db")).LogTo(Console.WriteLine));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.MapGraphQL();

app.Run();
