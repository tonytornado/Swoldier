using SocialLibrary.Data;

namespace SwoldierCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDB context)
        {
            context.Database.EnsureCreated();
        }
        public static void Initialize(SocialDB context)
        {
            context.Database.EnsureCreated();
        }
    }
}