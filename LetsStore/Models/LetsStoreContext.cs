using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LetsStore.Models;

public partial class LetsStoreContext : DbContext
{
    private readonly IConfiguration _configuration;

    public LetsStoreContext(DbContextOptions<LetsStoreContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<CategoryMap> CategoryMaps { get; set; }

    public virtual DbSet<FileCategory> FileCategories { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<StorageMap> StorageMaps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConn");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryMap>(entity =>
        {
            entity.ToTable("CategoryMap");

            entity.Property(e => e.CategoryMapId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryMapID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.StorageId).HasColumnName("StorageID");

            entity.HasOne(d => d.Cat).WithMany(p => p.CategoryMaps)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryMap_FileCategories");

            entity.HasOne(d => d.Storage).WithMany(p => p.CategoryMaps)
                .HasForeignKey(d => d.StorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryMap_Storage");
        });

        modelBuilder.Entity<FileCategory>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.Property(e => e.CatId)
                .ValueGeneratedNever()
                .HasColumnName("CatID");
            entity.Property(e => e.CatName).HasMaxLength(50);
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.StorageId).HasName("PK__Storage__8A247E370BC6105E");

            entity.ToTable("Storage");

            entity.Property(e => e.StorageId)
                .ValueGeneratedNever()
                .HasColumnName("StorageID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.FilePath).HasColumnType("text");
            entity.Property(e => e.StoredFileType).HasMaxLength(20);
        });

        modelBuilder.Entity<StorageMap>(entity =>
        {
            entity.HasKey(e => e.StorageMapId).HasName("PK__StorageM__02975C2C9B229590");

            entity.ToTable("StorageMap");

            entity.Property(e => e.StorageMapId)
                .ValueGeneratedNever()
                .HasColumnName("StorageMapID");
            entity.Property(e => e.StorageId).HasColumnName("StorageID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Storage).WithMany(p => p.StorageMaps)
                .HasForeignKey(d => d.StorageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorageMap_Storage");

            entity.HasOne(d => d.User).WithMany(p => p.StorageMaps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StorageMap_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC391A4D07");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF8105CEFDF").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.UserEmail).HasMaxLength(50);
            entity.Property(e => e.UserLastName).HasMaxLength(20);
            entity.Property(e => e.UserName).HasMaxLength(20);
            entity.Property(e => e.UserPassword).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
