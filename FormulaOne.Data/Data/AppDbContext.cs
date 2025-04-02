using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Data.Data;

public class AppDbContext:DbContext
{
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Achievement>(e =>
        {
            e.HasOne(x => x.Driver).WithMany(x => x.Achievements).HasForeignKey(x => x.DriverId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction).HasForeignKey("FK-Achievements_Driver");
        });
    }
}