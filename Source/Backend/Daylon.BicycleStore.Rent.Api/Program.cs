using Daylon.BicycleStore.Rent.Application;
using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new Daylon.BicycleStore.Rent.Api.ModelBinders.FlexibleDateTimeModelBinderProvider());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

//==|=====>
var app = builder.Build();

// Support Methods
using (var scope = app.Services.CreateScope())
{
    var orderService = scope.ServiceProvider.GetRequiredService<IRentalOrderService>();
    await orderService.ModifyOrderStatusToOverdueAsync();
}

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

//     ╱|、
//    (-ˎ- >  
//    |、˜〵          
//    じしˍ,)ノ D A Y L O N