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


	}
}
