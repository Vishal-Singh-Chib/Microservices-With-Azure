using Microsoft.EntityFrameworkCore;

namespace Wpm.Clinic.DataAccess
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) 
        { 
          
        }
    }

    public record Consulation(Guid Id, int PatientId, string PatientName, int PatientAge,DateTime StartTime);
    public static class ManagementDbContextExtext
    {
        public static void EnsureDbISCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ClinicDbContext>();
            context!.Database.EnsureCreated();
        }
    }
}
