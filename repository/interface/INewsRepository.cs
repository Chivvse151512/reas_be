using System;
using BusinessObject;

namespace repository
{
	public interface INewsRepository
	{
        Task CreateNewsAsync(News news);
        News GetNewsById(int id);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);
        IQueryable<News> GetAllNews();
    }
}

