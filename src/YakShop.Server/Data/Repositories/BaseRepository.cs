namespace YakShop.Server.Data.Repositories
{
    public interface IBaseRepository<T> : IDisposable
    {
        int Save();
    }

    public class BaseRepository<T>(YakShopDbContext dbContext) : IBaseRepository<T>
    {
        internal readonly YakShopDbContext db =
            dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public int Save()
        {
            return db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
