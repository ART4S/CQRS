﻿using WebFeatures.Application.Interfaces.Data;
using WebFeatures.Common.Time;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.DataContext
{
    public static class AppContextSeed
    {
        public static void Seed(this IAppContext context)
        {
            var user = new User()
            {
                Name = "admin",
                PasswordHash = string.Empty
            };
            context.Add<User, int>(user);

            var blog = new Blog()
            {
                Author = user,
                Title = "Admin blog"
            };
            context.Add<Blog, int>(blog);

            var post = new Post()
            {
                Blog = blog,
                Title = "First post",
                Content = "Hello, world!",
                CreatedAt = DateTimeProvider.Instance.Now
            };
            context.Add<Post, int>(post);

            context.SaveChanges();
        }
    }
}
