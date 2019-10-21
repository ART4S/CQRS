using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Blogs.CreateBlog;
using WebFeatures.Application.Features.Blogs.DeleteBlog;
using WebFeatures.Application.Features.Blogs.GetBlogsInfo;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Utils;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Блоги
    /// </summary>
    public class BlogController : BaseController
    {
        /// <summary>
        /// Получить информацию по всем блогам
        /// </summary>
        [HttpGet]
        public IActionResult Get(QueryFilter filter)
        {
            var blogs = Mediator.Send(new GetBlogInfosQuery()).ApplyFilter(filter);
            return Ok(blogs);
        }

        /// <summary>
        /// Создать блог
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreateBlogCommand command)
        {
            Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="id">Id блога</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Mediator.Send(new DeleteBlogCommand() { Id = id });
            return Ok();
        }
    }
}
