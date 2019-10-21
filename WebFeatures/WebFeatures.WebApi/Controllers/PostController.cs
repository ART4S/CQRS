using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Features.Posts.CreatePost;
using WebFeatures.Application.Features.Posts.DeletePost;
using WebFeatures.Application.Features.Posts.GetPostById;
using WebFeatures.Application.Features.Posts.GetPostsInfo;
using WebFeatures.Application.Features.Posts.UpdatePost;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Utils;
using WebFeatures.WebApi.Controllers.Base;

namespace WebFeatures.WebApi.Controllers
{
    /// <summary>
    /// Посты
    /// </summary>
    public class PostController : BaseController
    {
        /// <summary>
        /// Получить информацию по всем постам
        /// </summary>
        /// <param name="filter">Фильтр</param>
        [HttpGet]
        public IActionResult Get(QueryFilter filter)
        {
            var posts = Mediator.Send(new GetPostInfosQuery()).ApplyFilter(filter);
            return Ok(posts);
        }

        /// <summary>
        /// Получить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var post = Mediator.Send(new GetPostByIdQuery() { Id = id });
            return Ok(post);
        }

        /// <summary>
        /// Создать пост
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody, Required] CreatePostCommand command)
        {
            Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Редактировать пост
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody, Required] UpdatePostCommand command)
        {
            Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="id">Id поста</param>
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Mediator.Send(new DeletePostCommand() { Id = id });
            return Ok();
        }
    }
}
