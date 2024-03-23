using BusinessObject;

namespace DAO
{
    public class NewsDao
    {
        private readonly ReasContext? context;
        private static NewsDao? instance = null;
        public static NewsDao Instance
        {
            get
            {
                instance ??= new NewsDao();
                return instance;
            }
        }

        private NewsDao()
        {
            if (context == null)
            {
                try
                {
                    context = new ReasContext();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Cannot connect to the database: {ex.Message}");
                }
            }
        }

        // Create
        public async Task CreateNewsAsync(News news)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            context.News.Add(news);
            await context.SaveChangesAsync();
        }

        // Read
        public News GetNewsById(int id)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            return context.News.FirstOrDefault(x => x.Id == id);
        }

        // Update
        public async Task UpdateNewsAsync(News news)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            context.News.Update(news);
            await context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteNewsAsync(int id)
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            var news = context.News.Find(id);
            if (news != null)
            {
                context.News.Remove(news);
                await context.SaveChangesAsync();
            }
        }

        // List
        public IQueryable<News> GetAllNews()
        {
            if (context == null)
                throw new InvalidOperationException("Database context is not initialized.");

            return context.News;
        }
    }
}