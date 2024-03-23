using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using service;

namespace reas.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateNews([FromBody] NewsRequestModel newsRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseModel { Status = "Error", Message = "Validation failed.", Data = ModelState });
                }
                News news = new()
                {
                    Title = newsRequest.Title,
                    Author = newsRequest.Author,
                    Content = newsRequest.Content,
                    Image = newsRequest.Image,
                    PublishDate = DateTime.Now,
                    LastUpdated = null,
                };
                await _newsService.CreateNewsAsync(news);
                return Ok(new ResponseModel { Status = "Success", Message = "News created successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetNewsById(int id)
        {
            try
            {
                var news = _newsService.GetNewsById(id);
                if (news == null)
                {
                    return NotFound(new ResponseModel { Status = "Error", Message = "News not found." });
                }
                return Ok(new ResponseModel { Status = "Success", Data = news });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromBody] NewsUpdateRequestModel newsUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseModel { Status = "Error", Message = "Validation failed.", Data = ModelState });
                }
                var existingNews = _newsService.GetNewsById(newsUpdateRequest.Id);
                if (existingNews == null)
                {
                    return NotFound(new ResponseModel { Status = "Error", Message = "News not found." });
                }

                if (newsUpdateRequest.Title != null)
                    existingNews.Title = newsUpdateRequest.Title;
                if (newsUpdateRequest.Author != null)
                    existingNews.Author = newsUpdateRequest.Author;
                if (newsUpdateRequest.Content != null)
                    existingNews.Content = newsUpdateRequest.Content;
                if (newsUpdateRequest.Image != null)
                    existingNews.Image = newsUpdateRequest.Image;

                existingNews.LastUpdated = DateTime.Now;

                // Save changes to the database
                await _newsService.UpdateNewsAsync(existingNews);
                return Ok(new ResponseModel { Status = "Success", Message = "News updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            try
            {
                await _newsService.DeleteNewsAsync(id);
                return Ok(new ResponseModel { Status = "Success", Message = "News deleted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseModel { Status = "Error", Message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllNews()
        {
            var news = _newsService.GetAllNews();
            if (news == null || !news.Any())
            {
                return NotFound(new ResponseModel { Status = "Error", Message = "No news found." });
            }
            return Ok(new ResponseModel { Status = "Success", Data = news });
        }
    }
}

