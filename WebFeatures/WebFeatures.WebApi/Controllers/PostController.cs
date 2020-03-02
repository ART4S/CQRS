using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Posts.CreatePost;
using WebFeatures.Application.Features.Posts.DeletePost;
using WebFeatures.Application.Features.Posts.GetPostById;
using WebFeatures.Application.Features.Posts.GetPosts;
using WebFeatures.Application.Features.Posts.UpdatePost;
using WebFeatures.Domian.Entities.Model;
using WebFeatures.QueryFiltering.Extensions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.WebApi.Controllers.Base;
using WebFeatures.WebApi.Filters;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Посты
    /// </summary>
    [ExistsInDatabase(typeof(Post))]
    [Authorize]
    public class PostController : BaseController
    {
        /// <summary>
        /// Получить информацию по всем постам
        /// </summary>
        /// <param name="filter">Фильтр</param>
        [HttpGet]
        public IActionResult Get(QueryFilter filter)
        {
            var posts = Mediator.SendQuery(new GetPostsQuery()).ApplyFilter(filter);
            return Ok(posts);
        }

        /// <summary>
        /// Получить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var post = Mediator.SendQuery(new GetPostByIdQuery() { Id = id });
            return Ok(post);
        }

        /// <summary>
        /// Создать пост
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreatePostCommand command)
        {
            Mediator.SendCommand(command);
            return Ok();
        }

        /// <summary>
        /// Редактировать пост
        /// </summary>
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody, Required] UpdatePostCommand command)
        {
            Mediator.SendCommand(command);
            return Ok();
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Mediator.SendCommand(new DeletePostCommand() { Id = id });
            return Ok();
        }
    }
}
