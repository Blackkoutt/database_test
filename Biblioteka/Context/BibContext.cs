using Biblioteka.Areas.Identity.Data;
using Biblioteka.Extensions;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteka.Context
{
    public class BibContext : IdentityDbContext<BibUser>
    {
        public BibContext(DbContextOptions<BibContext> options) : base(options)
        {

        }
        public DbSet<Author> Author {get; set;}      
        public DbSet<Book_Author> Book_Author { get; set; }

        public DbSet<Book> Book { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<BookType> BookType { get; set; }
        public DbSet<Book_Tag> Book_Tag { get; set; }
        public DbSet<Book_Opinions> Book_Opinions { get; set; }
        public DbSet<Tag> Tag { get; set; }
        
        public DbSet<Borrowing> Borrowing{ get; set; }
        public DbSet<Reader> Reader { get; set; }
        public DbSet<Reader_Borrowings> Reader_Borrowings { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeData> EmployeeData { get; set; }      
        public DbSet<Position> Position { get; set; }
        
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomReservation> RoomReservation { get; set; }

        public DbSet<EmployeeConfirmingReturn> EmployeeConfirmingReturnsBook { get; set; }
        public DbSet<EmployeeConfirmingPayment> EmployeeConfirmingPaymentsBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.SetMaxIdentifierLength(30);

            // Klucze główne tabel
            modelBuilder.Entity<BookType>().HasKey(p => p.typeId);
            modelBuilder.Entity<Borrowing>().HasKey(p => p.borrowId);
            modelBuilder.Entity<Employee>().HasKey(p => p.employeeId);
            modelBuilder.Entity<EmployeeData>().HasKey(p => p.employeeId);
            modelBuilder.Entity<Room>().HasKey(p => p.roomNumber);
            modelBuilder.Entity<RoomReservation>().HasKey(p => p.reservationId);

            //modelBuilder.Entity<Author>().Property(p => p.authorId).UseIdentityColumn(seed:1, increment:1);

            // Relacja 1:1
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.employeeData)
                .WithOne(x => x.employee);

            // Relacje N:N
            modelBuilder.Entity<Book_Opinions>().ToTable("Book_Opinions");
            modelBuilder.Entity<Book_Opinions>().HasKey(p => new { p.bookId, p.readerId });
            modelBuilder.Entity<Book_Opinions>()
                .HasOne(x => x.book)
                .WithMany(x => x.opinions)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Opinions>()
                .HasOne(x => x.reader)
                .WithMany(x => x.bookOpinions)
                .HasForeignKey(x => x.readerId);


            modelBuilder.Entity<Book_Author>().ToTable("Book_Author");
            modelBuilder.Entity<Book_Author>().HasKey(p => new { p.bookId, p.authorId });
            modelBuilder.Entity<Book_Author>()
                .HasOne(x => x.book)
                .WithMany(x => x.authors)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Author>()
                .HasOne(x => x.author)
                .WithMany(x => x.books)
                .HasForeignKey(x => x.authorId);

            modelBuilder.Entity<Book_Tag>().ToTable("Book_Tag");
            modelBuilder.Entity<Book_Tag>().HasKey(p => new { p.bookId, p.tagId });
            modelBuilder.Entity<Book_Tag>()
                .HasOne(x => x.book)
                .WithMany(x => x.tags)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Tag>()
                .HasOne(x => x.tag)
                .WithMany(x => x.books)
                .HasForeignKey(x => x.tagId);

            modelBuilder.Entity<Reader_Borrowings>().ToTable("Reader_Borrowings");
            modelBuilder.Entity<Reader_Borrowings>().HasKey(p => new { p.readerId, p.borrowId });
            modelBuilder.Entity<Reader_Borrowings>()
                .HasOne(x => x.reader)
                .WithMany(x => x.borrowings)
                .HasForeignKey(x => x.readerId);
            modelBuilder.Entity<Reader_Borrowings>()
                .HasOne(x => x.borrow)
                .WithMany(x => x.readers)
                .HasForeignKey(x => x.borrowId);

            //Konfiguracja danych nowych użytkowników
            modelBuilder.ApplyConfiguration(new BibUserEntityConfiguration());

            //Dodanie ról i przykładowych kont z danymi rolami
            //modelBuilder.Entity<IdentityRole>().HasData(
            //new IdentityRole
            //{
            //    Id = "1",
            //    Name = "Admin"
            //},
            //new IdentityRole
            //{
            //    Id = "2",
            //    Name = "Employee"
            //},
            //new IdentityRole
            //{
            //    Id = "3",
            //    Name = "Reader"
            //});

            //var hasher = new PasswordHasher<BibUser>();

            //modelBuilder.Entity<BibUser>().HasData(
            //new BibUser
            //{
            //    Id = "1",
            //    name = "Imię",
            //    surname = "Nazwisko",
            //    Email = "admin@admin.com",
            //    NormalizedEmail = "ADMIN@ADMIN.COM",
            //    UserName = "admin@admin.com",
            //    NormalizedUserName = "ADMIN",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "admin123"),
            //},
            //new BibUser
            //{
            //    Id = "2",
            //    name = "Imię",
            //    surname = "Nazwisko",
            //    Email = "employee@employee.com",
            //    NormalizedEmail = "EMPLOYEE@EMPLOYEE.COM",
            //    UserName = "employee@employee.com",
            //    NormalizedUserName = "EMPLOYEE",
            //    EmailConfirmed = true,
            //    PasswordHash = hasher.HashPassword(null, "employee123")
            //});

            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //new IdentityUserRole<string>
            //{
            //    RoleId = "1",
            //    UserId = "1"
            //},
            //new IdentityUserRole<string>
            //{
            //    RoleId = "2",
            //    UserId = "2"
            //});

            // Dodawanie przykładowych danych
            // modelBuilder.Seed();
        } 
    }
}

internal class BibUserEntityConfiguration : IEntityTypeConfiguration<BibUser>
{
    public void Configure(EntityTypeBuilder<BibUser> builder)
    {
        builder.Property(u => u.name).IsRequired().HasMaxLength(20);
        builder.Property(u => u.surname).IsRequired().HasMaxLength(40);
        builder.Property(u => u.Email).HasMaxLength(40);
    }
}