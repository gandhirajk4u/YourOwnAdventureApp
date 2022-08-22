var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddTransient<IAdventureService, AdventureService>();
builder.Services.AddTransient<IAdventureRepository, AdventureRepository>();
builder.Services.AddTransient<IAdventureUserService, AdventureUserService>();
builder.Services.AddTransient<IAdventureUserRepository, AdventureUserRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

// Database Context
var connectionString = configuration.GetConnectionString(Constants.DbConnectionName);
builder.Services.AddDbContextPool<AdventureDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

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
