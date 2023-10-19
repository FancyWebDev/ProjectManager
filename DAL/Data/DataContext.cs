using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Task = DAL.Models.Task;

namespace DAL.Data;

public class DataContext : IdentityDbContext<Collaborator>
{
    public DataContext()
    {
            
    }
        
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
            
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder
            .Entity<Project>()
            .HasMany(project => project.Collaborators)
            .WithMany(collaborator => collaborator.Projects);

        modelBuilder.Entity<Task>(builder =>
        {
            builder
                .HasOne(task => task.Author)
                .WithMany(collaborator => collaborator.CreatedTasks)
                .HasForeignKey(task => task.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(task => task.Performer)
                .WithMany(collaborator => collaborator.Tasks)
                .HasForeignKey(task => task.PerformerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
            
        modelBuilder.Entity<Project>()
            .Property(x => x.FilesUrl)
            .HasConversion(new ValueConverter<ICollection<string>?, string>(
                collection => JsonConvert.SerializeObject(collection),
                value => JsonConvert.DeserializeObject<ICollection<string>>(value)));

        modelBuilder
            .Entity<Project>()
            .Property(e => e.FilesUrl)
            .Metadata
            .SetValueComparer(new ValueComparer<ICollection<string>>(
                (collection1, collection2) => collection1.SequenceEqual(collection2),
                collection => collection.Aggregate(0, (value1, value2) => HashCode.Combine(value1, value2.GetHashCode())),
                collection => collection.ToHashSet()));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) 
            optionsBuilder.UseSqlServer("Data Source=LITTLE-MONSTER;Initial Catalog=Projects;Integrated Security=True;Encrypt=False");
    }
        
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter, DateOnlyComparer>()
            .HaveColumnType("date");
    }  
}