using AvaliaAe.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaliaAe.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CodeModel> Code { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<InstitutionModel> Institution { get; set; }
        public DbSet<AssociationsModel> Associations { get; set; }
        public DbSet<DocumentationModel> Documentations { get; set; }
        public DbSet<AvaliationModel> Avaliations { get; set; }
        public DbSet<UserTypeModel> UserType { get; set; }
        public DbSet<DenouncesModel> Denounces { get; set; }
        public DbSet<InstitutionTypeModel> InstitutionType { get; set; }
        public DbSet<CodeInstitutionModel> CodeInstitutions { get; set; }
        public DbSet<CertificationModel> Certification { get; set; }
        public DbSet<AverageModel> Average { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvaliationModel>()
                .HasOne(a => a.Average)
                .WithMany()
                .HasForeignKey(a => a.AverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AvaliationModel>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AvaliationModel>()
                .HasOne(a => a.Institution)
                .WithMany()
                .HasForeignKey(a => a.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AverageModel>()
               .HasOne(a => a.Institution)
               .WithOne(a => a.Average)
               .HasForeignKey<AverageModel>(a => a.InstitutionId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DocumentationModel>()
               .HasOne(d => d.User)
               .WithMany()
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DocumentationModel>()
                .HasOne(d => d.Institution)
                .WithMany()
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.NoAction);


            base.OnModelCreating(modelBuilder);
        }


    }
}