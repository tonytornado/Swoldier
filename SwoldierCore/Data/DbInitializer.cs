namespace SwoldierCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDB context)
        {
            context.Database.EnsureCreated();
        }
    }
}