using EFCore1.Entities;
using EFCore1.EntityConfigurations;

namespace EFCore1;

internal class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly bool _isMigration;

    public AppDbContext(IConfiguration configuration, bool isMigration = false)
    {
        _configuration = configuration;
        _isMigration = isMigration;
    }

    private const string LogFileName = "logs.txt";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_isMigration)
        {
            optionsBuilder.UseSqlite(_configuration["ConnectionStrings:SQLite_Migration"]);
        }
        else
        {
            optionsBuilder.UseSqlite(_configuration["ConnectionStrings:SQLite"]);
        }


        // "SQLite": "Data Source=app.db;"

        File.Delete(LogFileName);
        optionsBuilder.LogTo(message => File.AppendAllText(LogFileName, message));
        // optionsBuilder.LogTo(Console.WriteLine);

        // optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

        modelBuilder.Entity<Publisher>()
            .HasData(new List<Publisher>
            {
                new Publisher { Id = -1, Name = "Electronic Arts" },
                new Publisher { Id = -2, Name = "Ubisoft" },
                new Publisher { Id = -3, Name = "Activision Blizzard" },
                new Publisher { Id = -4, Name = "Nintendo" },
                new Publisher { Id = -5, Name = "Sony Interactive Entertainment" },
                new Publisher { Id = -6, Name = "Microsoft Gaming" },
                new Publisher { Id = -7, Name = "Square Enix" },
                new Publisher { Id = -8, Name = "Bandai Namco" },
                new Publisher { Id = -9, Name = "Sega" },
                new Publisher { Id = -10, Name = "Capcom" }
            });

        modelBuilder.Entity<Category>()
            .HasData(new List<Category>
            {
                new Category { Id = -1, Name = "Action" },
                new Category { Id = -2, Name = "Adventure" },
                new Category { Id = -3, Name = "RPG" },
                new Category { Id = -4, Name = "Strategy" },
                new Category { Id = -5, Name = "Sports" }
            });

        modelBuilder.Entity<CategoryGame>()
            .HasData(new List<CategoryGame>
            {
                new CategoryGame { CategoryId = -1, GameId = -1 },
                new CategoryGame { CategoryId = -2, GameId = -2 },
                new CategoryGame { CategoryId = -3, GameId = -3 },
                new CategoryGame { CategoryId = -4, GameId = -4 },
                new CategoryGame { CategoryId = -5, GameId = -5 },
                new CategoryGame { CategoryId = -1, GameId = -6 },
                new CategoryGame { CategoryId = -2, GameId = -7 },
                new CategoryGame { CategoryId = -3, GameId = -8 },
                new CategoryGame { CategoryId = -4, GameId = -9 },
                new CategoryGame { CategoryId = -5, GameId = -10 }
            });

        modelBuilder.Entity<Game>()
            .HasData(new List<Game>
            {
                new Game { Id = -1, Name = "The Witcher 3: Wild Hunt", Price = 29.99, PublisherId = -1 },
                new Game { Id = -2, Name = "Cyberpunk 2077", Price = 39.99, PublisherId = -1 },
                new Game { Id = -3, Name = "Red Dead Redemption 2", Price = 59.99, PublisherId = -2 },
                new Game { Id = -4, Name = "Elden Ring", Price = 49.99, PublisherId = -3 },
                new Game { Id = -5, Name = "God of War: Ragnarök", Price = 69.99, PublisherId = -4 },
                new Game { Id = -6, Name = "Horizon Forbidden West", Price = 49.99, PublisherId = -4 },
                new Game { Id = -7, Name = "Starfield", Price = 59.99, PublisherId = -5 },
                new Game
                {
                    Id = -8, Name = "The Legend of Zelda: Tears of the Kingdom", Price = 59.99, PublisherId = -6
                },
                new Game { Id = -9, Name = "Final Fantasy XVI", Price = 69.99, PublisherId = -7 },
                new Game { Id = -10, Name = "Resident Evil 4 Remake", Price = 49.99, PublisherId = -8 }
            });
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<EarlyAccessGame> EarlyAccessGames { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryGame> CategoryGames { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
}