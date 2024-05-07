using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.IISIntegration;



#if DEBUG
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
#endif
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options => options.DefaultScheme = IISDefaults.AuthenticationScheme);
builder.Services.Configure<IISOptions>(options => { options.AutomaticAuthentication = true; });
builder.Services.AddAuthorization();




#if !DEBUG
string connection = builder.Configuration.GetConnectionString("DebugConnection");
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connection));
#endif

#if DEBUG
string connection = builder.Configuration.GetConnectionString("DebugConnection");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("h*").AllowCredentials().SetIsOriginAllowed((host) => true)
                                                .AllowAnyHeader().WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                                .AllowAnyMethod();
                      });
});
#endif

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

//Регистрация в DI контейнере


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection(); //
//app.UseRouting();          //
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#if DEBUG
app.UseCors(MyAllowSpecificOrigins);
#endif
app.MapControllers();

app.Run();
