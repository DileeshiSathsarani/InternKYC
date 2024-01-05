using internKYC;
using internKYC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IinternKYCService, InternKYCService>();
builder.Services.AddScoped<IOtpService>(provider =>
{
    var dbContext = provider.GetRequiredService<ApplicationDbContext>();
    var configuration = provider.GetRequiredService<IConfiguration>();

    var accountSid = configuration["Twilio:ACa32b104c47f3e44aca1cc060c3f060f6"]; 
    var authToken = configuration["Twilio:450a90064dbf15f56a565ae8c58577d9"]; 
    var contact_number = configuration["Twilio:XXXXX1830"]; 
    return new OtpService(dbContext, accountSid, authToken, contact_number);
});


builder.Services.AddControllers();
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
