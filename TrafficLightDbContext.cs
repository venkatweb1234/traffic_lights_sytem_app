using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using traffic_lights_sytem_app.Modal;

public class TrafficLightConfiguration : IEntityTypeConfiguration<TrafficLight>
{
    public void Configure(EntityTypeBuilder<TrafficLight> builder)
    {
        builder.Property(t => t.Direction)
               .HasConversion<string>();
        builder.Property(t => t.Color)
              .HasConversion<string>();
    }
}

public class TrafficLightDbContext : DbContext
{
    public DbSet<TrafficLight> TrafficLights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrafficLightConfiguration());
    }
    public TrafficLightDbContext(DbContextOptions<TrafficLightDbContext> options)
           : base(options)
    {
    }
}
