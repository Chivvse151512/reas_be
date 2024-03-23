using System;
using BusinessObject;
using repository;

namespace service
{
	public class NewsService : INewsService
	{
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task CreateNewsAsync(News news)
        {
            if (news == null)
                throw new ArgumentNullException(nameof(news));


            ValidateNews(news);
            await _newsRepository.CreateNewsAsync(news);
        }

        public News GetNewsById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");

            return _newsRepository.GetNewsById(id);
        }

        public async Task UpdateNewsAsync(News news)
        {
            if (news == null)
                throw new ArgumentNullException(nameof(news));

            // Validate news data here
            ValidateNews(news);
            await _newsRepository.UpdateNewsAsync(news);
        }

        public async Task DeleteNewsAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID");

            await _newsRepository.DeleteNewsAsync(id);
        }

        public IQueryable<News> GetAllNews()
        {
            return _newsRepository.GetAllNews();
        }

        private void ValidateNews(News news)
        {
            if (string.IsNullOrWhiteSpace(news.Title))
                throw new ArgumentException("News title is required.");

            if (string.IsNullOrWhiteSpace(news.Content))
                throw new ArgumentException("News content is required.");

            if (string.IsNullOrWhiteSpace(news.Author))
                throw new ArgumentException("Author is required.");
        }
    }
}


