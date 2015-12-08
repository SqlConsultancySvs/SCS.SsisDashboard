using System.Data.Entity;

namespace SCS.SissDashboard.DAL
{
    public partial class EfContext : DbContext
    {
        static EfContext()
        {
            Database.SetInitializer<EfContext>(null);
        }

        public EfContext()
            : base("Name=EfContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
