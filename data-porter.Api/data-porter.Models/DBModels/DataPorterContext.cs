using core.Models.AppSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace data_porter.Models.DBModels;

/// <summary>
/// Data Porter Context
/// </summary>
public partial class DataPorterContext : DbContext
{
    private readonly string _connectionString;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connectionString">Connection String</param>
    public DataPorterContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options">Data Potrter Context <see cref="DbContextOptions{DataPorterContext}"/></param>
    /// <param name="connectionStringConfiguration">Connectionstring Configuration <see cref="IOptions{ConnectionStringSettings}"/></param>
    public DataPorterContext(DbContextOptions<DataPorterContext> options,
        IOptions<ConnectionStringSettings> connectionStringConfiguration)
        : base(options)
    {
        _connectionString = connectionStringConfiguration.Value.PostgresConnectionString;
    }

    /// <summary>
    /// Azure Blob References
    /// </summary>
    public virtual DbSet<AzureBlobReference> AzureBlobReferences { get; set; }

    /// <summary>
    /// On Configuring
    /// </summary>
    /// <param name="optionsBuilder">Options Builder <see cref="DbContextOptionsBuilder"/></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionString,
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null
                        );
                });
        }
    }

    /// <summary>
    /// On Model Creating
    /// </summary>
    /// <param name="modelBuilder">Model Builder <see cref="ModelBuilder"/></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AzureBlobReference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_tenant_id");

            entity.ToTable("azure_blob_reference");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.FileName)
                .HasMaxLength(200)
                .HasColumnName("file_name");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.FileType)
                .HasMaxLength(200)
                .HasColumnName("file_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
