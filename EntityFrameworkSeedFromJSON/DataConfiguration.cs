namespace EntityFrameworkSeedFromJSON
{
    public class DataConfiguration : System.Data.Entity.DbConfiguration
    {
        public DataConfiguration()
        {
            //SetDatabaseInitializer<DbLocalContext>(null); // back door way to disable initialization
            // want to use next line: http://stackoverflow.com/questions/33307726/how-to-use-createdatabaseifnotexists-in-dbconfiguration-not-global-asax-cs
            SetDatabaseInitializer(new MultiTenantContextInitializer());
        }
    }
}
