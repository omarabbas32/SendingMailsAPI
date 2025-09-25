using SendingMailsAPI.Services;

namespace SendingMailsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // ✅ Configure SmtpSettings from appsettings.json or Environment Variables
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

            // ✅ Register EmailService
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            // ✅ Railway uses PORT env variable
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            app.Urls.Add($"http://*:{port}");

            app.Run();
        }
    }
}
