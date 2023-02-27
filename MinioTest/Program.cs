using MinioTest.Config;
using MinioTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Hard coded MinIO configuration, could be read from appsettings.json file
var minioConfig = new MinioConfiguration
{
    Host = "localhost",
    Port = 9000,
    AccessKey = "R2mRPbn4czooU0vJ",
    SecureKey = "uuLXgzSBQFSAkZlWCR6NhwRDWS6bvHNe",
    BucketName = "test-bucket"
};

builder.Services.AddSingleton(minioConfig);

builder.Services.AddScoped<IMinioService, MinioService>();

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