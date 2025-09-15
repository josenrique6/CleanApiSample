using CleanApiSample.Application.Features.Invoices.Commands;
using CleanApiSample.Application.Features.Invoices.Queries;
using CleanApiSample.Application.Features.Products;
using CleanApiSample.Application.Features.Products.Commands;
using CleanApiSample.Application.Features.Products.Queries;
using CleanApiSample.Infraestructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
     {
         policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
     });
});

builder.Services.AddInfraestructure(builder.Configuration);

#region [QC services]
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProductCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductsByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProductCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProductoCommand).Assembly));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddInvoiceCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetInvoicesQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetInvoiceByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateInvoiceCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteInvoiceCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetInvoicesByFiltersQuery).Assembly));
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanApiSample.Api", Version = "v1" }));

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanApiSample.Api v1"));

app.MapControllers();

app.Run();
