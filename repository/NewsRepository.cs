using System;
using BusinessObject;
using DAO;

namespace repository
{
	public class NewsRepository : INewsRepository
	{
        public Task CreateNewsAsync(News news) => NewsDao.Instance.CreateNewsAsync(news);

        public Task DeleteNewsAsync(int id) => NewsDao.Instance.DeleteNewsAsync(id);

        public IQueryable<News> GetAllNews() => NewsDao.Instance.GetAllNews();

        public News GetNewsById(int id) => NewsDao.Instance.GetNewsById(id);

        public Task UpdateNewsAsync(News news) => NewsDao.Instance.UpdateNewsAsync(news);
    }
}

