using System;
using BusinessObject;

namespace service
{
	public interface INewsService
	{
        Task CreateNewsAsync(News news);
        News GetNewsById(int id);
        Task UpdateNewsAsync(News news);
        Task DeleteNewsAsync(int id);
        IQueryable<News> GetAllNews();
    }
}

