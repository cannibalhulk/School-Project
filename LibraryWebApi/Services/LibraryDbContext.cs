using System;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.Models;

namespace LibraryWebApi.Services;

public class LibraryDbContext : DbContext
{
	public LibraryDbContext(DbContextOptions<LibraryDbContext> options) :base(options)
	{
      // Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Определение отношений между таблицами (например, User и FavoriteBook)
        modelBuilder.Entity<FavoriteBook>()
            .HasOne(fb => fb.User)
            .WithMany(u => u.FavoriteBooks)
            .HasForeignKey(fb => fb.UserId);

        modelBuilder.Entity<FavoriteBook>()
            .HasOne(fb => fb.Book)
            .WithMany()
            .HasForeignKey(fb => fb.BookId);

        // Определение ограничений и индексов

        var faker = new Faker();
        int id = 0;
        // Генерация 20 книг
        var books = Enumerable.Range(1, 20).Select(i => new Book
        {
            Id = id++,
            Title = faker.Lorem.Word(),
            Author = faker.Name.FullName(),
            Description = faker.Lorem.Sentence(),
            Genre = faker.Lorem.Word(),
            DateAdded = faker.Date.Past(),
            DateCreated = DateTime.Now

        }).ToList();
        modelBuilder.Entity<Book>().HasData(books);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<FavoriteBook> FavoriteBooks { get; set; }
    public DbSet<Rating> Ratings { get; set; }
}
