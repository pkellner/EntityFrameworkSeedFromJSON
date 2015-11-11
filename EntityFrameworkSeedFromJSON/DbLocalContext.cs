using System.Data.Entity;


namespace EntityFrameworkSeedFromJSON
{
    [DbConfigurationType(typeof(DataConfiguration))]
    public class DbLocalContext : DbContext
    {
       
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }

   
}
