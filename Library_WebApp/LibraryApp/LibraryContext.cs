using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LibraryApp.Models;

namespace LibraryApp
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<Authorship> Authorship { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BookCopy> BookCopy { get; set; }
        public virtual DbSet<BookCopyState> BookCopyState { get; set; }
        public virtual DbSet<Borrowing> Borrowing { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Edition> Edition { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Librarian> Librarian { get; set; }
        public virtual DbSet<PublishingHouse> PublishingHouse { get; set; }
        public virtual DbSet<Reader> Reader { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => new { e.Street, e.City, e.Country })
                    .HasName("AK_Address")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zipCode")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasIndex(e => new { e.LastName, e.FirstName })
                    .HasName("IX_Author");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Authorship>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId });

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Authorship)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authorship_Author");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Authorship)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Authorship_Book");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Title)
                    .HasName("IX_Book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("genre")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.Genre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Book_Genre");
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.HasIndex(e => e.EditionId)
                    .HasName("IX_BookCopy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BranchNumber).HasColumnName("branchNumber");

                entity.Property(e => e.Condition)
                    .HasColumnName("condition")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.EditionId).HasColumnName("editionId");

                entity.HasOne(d => d.BranchNumberNavigation)
                    .WithMany(p => p.BookCopy)
                    .HasForeignKey(d => d.BranchNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCopy_Branch");

                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.BookCopy)
                    .HasForeignKey(d => d.EditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookCopy_Edition");
            });

            modelBuilder.Entity<BookCopyState>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BookCopyState");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.CopyId, e.BorrowDate });

                entity.HasIndex(e => new { e.CopyId, e.BorrowDate })
                    .HasName("IX_Borrowing");

                entity.Property(e => e.UserLogin)
                    .HasColumnName("userLogin")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CopyId).HasColumnName("copyId");

                entity.Property(e => e.BorrowDate).HasColumnName("borrowDate");

                entity.Property(e => e.ReturnDate).HasColumnName("returnDate");

                entity.HasOne(d => d.Copy)
                    .WithMany(p => p.Borrowing)
                    .HasForeignKey(d => d.CopyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Borrowing_BookCopy");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Borrowing)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Borrowing_Reader");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchNumber);

                entity.HasIndex(e => e.AddressId)
                    .HasName("AK_Branch")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("AK_Branch_Name")
                    .IsUnique();

                entity.Property(e => e.BranchNumber)
                    .HasColumnName("branchNumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OpeningHours)
                    .IsRequired()
                    .HasColumnName("openingHours")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithOne(p => p.Branch)
                    .HasForeignKey<Branch>(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Address");
            });

            modelBuilder.Entity<Edition>(entity =>
            {
                entity.HasIndex(e => e.BookId)
                    .HasName("IX_Edition");

                entity.HasIndex(e => new { e.BookId, e.ReleaseDate, e.PublishingHouse })
                    .HasName("AK_Edition")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.PublishingHouse)
                    .IsRequired()
                    .HasColumnName("publishingHouse")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("releaseDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Edition)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Edition_Book");

                entity.HasOne(d => d.PublishingHouseNavigation)
                    .WithMany(p => p.Edition)
                    .HasForeignKey(d => d.PublishingHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Edition_PublishingHouse");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Librarian>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.HasIndex(e => e.BranchNumber)
                    .HasName("IX_Librarian");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.BranchNumber).HasColumnName("branchNumber");

                entity.Property(e => e.EmploymentDate)
                    .HasColumnName("employmentDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.BranchNumberNavigation)
                    .WithMany(p => p.Librarian)
                    .HasForeignKey(d => d.BranchNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Librarian_Branch");

                entity.HasOne(d => d.LoginNavigation)
                    .WithOne(p => p.Librarian)
                    .HasForeignKey<Librarian>(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Librarian_User");
            });

            modelBuilder.Entity<PublishingHouse>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.HasIndex(e => e.AddressId)
                    .HasName("AK_PublishingHouse")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.HasOne(d => d.Address)
                    .WithOne(p => p.PublishingHouse)
                    .HasForeignKey<PublishingHouse>(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PublishingHouse_Address");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.LoginNavigation)
                    .WithOne(p => p.Reader)
                    .HasForeignKey<Reader>(d => d.Login)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reader_User");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => new { e.UserLogin, e.CopyId, e.ReservationDate });

                entity.HasIndex(e => e.CopyId)
                    .HasName("IX_Reservation");

                entity.Property(e => e.UserLogin)
                    .HasColumnName("userLogin")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CopyId).HasColumnName("copyId");

                entity.Property(e => e.ReservationDate).HasColumnName("reservationDate");

                entity.HasOne(d => d.Copy)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.CopyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_BookCopy");

                entity.HasOne(d => d.UserLoginNavigation)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.UserLogin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Reader");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.HasIndex(e => new { e.LastName, e.FirstName })
                    .HasName("IX_User");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
