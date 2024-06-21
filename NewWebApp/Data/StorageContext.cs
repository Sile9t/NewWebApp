using NewWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NewWebApp.Data
{
    public class StorageContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }

        public StorageContext() { }

        public StorageContext(DbContextOptions<StorageContext> dbContext) : base(dbContext) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-U893DOI;Initial Catalog = Products;TrustServerCertificate=True;Trusted_Connection=True")
                .UseLazyLoadingProxies().LogTo(Console.WriteLine);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");
                entity.HasKey(x => x.Id).HasName("product_pk");

                entity.Property(x => x.Id)
                      .HasColumnName("id")
                      .IsRequired();
                entity.Property(x => x.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);
                entity.Property(x => x.Price)
                      .HasColumnName("price");
                entity.Property(x => x.Description)
                      .HasColumnName("description")
                      .HasMaxLength(255);
                entity.HasOne(x => x.Group)
                      .WithMany(x => x.Products)
                      .HasForeignKey(x => x.GroupId);
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("product_group");
                entity.HasKey(x => x.Id).HasName("product_group_pk");

                entity.Property(x => x.Id)
                      .HasColumnName("id");
                entity.Property(x => x.Name)
                      .HasColumnName("name")
                      .HasMaxLength(255);
                entity.Property(x => x.Description)
                      .HasColumnName("description")
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("storage");
                entity.HasKey(x => x.Id).HasName("storage_pk");

                entity.HasOne(x => x.Product)
                      .WithMany(x => x.Storages)
                      .HasForeignKey(x => x.ProductId);
            });
        }
    }
}
