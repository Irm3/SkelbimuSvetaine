using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.EntityFrameworkCore;

#nullable disable

namespace SkelbimuSvetaine.Models
{
    public partial class ld1_gynimasContext : DbContext
    {
        public ld1_gynimasContext()
        {
        }

        public ld1_gynimasContext(DbContextOptions<ld1_gynimasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost; port=3306; user=root; password=; SslMode=none; database=ld1_gynimas");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.ProductId, "fk_Comment_Product1_idx");

                entity.HasIndex(e => e.UserId, "fk_Comment_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Product_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("User_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Comment_Product1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Comment_User1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "fk_Product_Category1_idx");

                entity.HasIndex(e => e.UserId, "fk_Product_User_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Category_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnType("blob")
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("double(10)")
                    .HasColumnName("price");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("User_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Category1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_User");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("rating");

                entity.HasIndex(e => e.ProductId, "fk_Rating_Product1_idx");

                entity.HasIndex(e => e.UserId, "fk_Rating_User1_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("Product_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("User_id");

                entity.Property(e => e.Value)
                    .HasColumnType("int(11)")
                    .HasColumnName("value");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rating_Product1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Rating_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("phone");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Icon)
                    .HasColumnType("blob")
                    .HasColumnName("icon")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Miestas)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("miestas");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
