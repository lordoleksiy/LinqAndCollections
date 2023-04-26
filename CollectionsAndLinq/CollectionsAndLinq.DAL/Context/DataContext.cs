using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Task = CollectionsAndLinq.DAL.Entities.Task;

namespace CollectionsAndLinq.DAL.DB;

public class DataContext: DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }

    public DataContext()
    {}
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Initial Catalog=LinqAndCollectionsDB;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany<Project>(p => p.Projects)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId)
            .IsRequired();

        modelBuilder.Entity<Team>()
            .HasMany<Project>(p => p.Projects)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany<Task>(p => p.Tasks)
            .WithOne(p => p.Performer)
            .HasForeignKey(p => p.PerformerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Team>()
            .HasMany<User>(p => p.Users)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId);

        modelBuilder.Entity<Project>()
            .HasMany<Task>(p => p.Tasks)
            .WithOne(p => p.Project)
            .HasForeignKey(p => p.ProjectId)
            .IsRequired();

        modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
        modelBuilder.Entity<User>().Property(p => p.FirstName).IsRequired();
        modelBuilder.Entity<User>().Property(p => p.LastName).IsRequired();
        modelBuilder.Entity<Team>().Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Task>().Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Task>().Property(p => p.ProjectId).IsRequired();
        modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired();

        modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
        modelBuilder.Entity<Team>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Project>().HasIndex(p => p.Name).IsUnique();

        modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnType("VARCHAR").HasMaxLength(20);
        modelBuilder.Entity<User>().Property(p => p.LastName).HasColumnType("VARCHAR").HasMaxLength(20);
        modelBuilder.FillInData();
    }
}