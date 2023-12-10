using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(mt =>
{
    mt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
    //cfg.AutoStart = true;

    //cfg.Publish<IServerNotificationMessage>(e => e.ExchangeType = RabbitMQ.Client.ExchangeType.Direct);
});

//builder.Services.Configure<MassTransitHostOptions>(options =>
//{
//    options.WaitUntilStarted = true;
//    options.StartTimeout = TimeSpan.FromSeconds(30);
//    options.StopTimeout = TimeSpan.FromMinutes(1);
//});
builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllerRoute(name: "default",
               pattern: "{controller=Ticket}/{action=GetTicket}/{id?}");

app.Run();
