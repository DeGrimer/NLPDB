using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NLPDB.Web.Infrastructure;

namespace NLPDB.Web.Data
{
    public static class DataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var isExists = context!.Database.CanConnect();
            if (isExists)
            {
                return;
            }

            await context.Database.MigrateAsync();

            var roles = AppData.Roles.ToArray();
            var roleStore = new RoleStore<IdentityRole>(context);
            foreach (var role in roles)
            {
                if (!context.Roles.Any(x => x.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper() });
                }
            }

            const string username = "d@f.com";

            if (context.Users.Any(x => x.Email == username))
            {
                return;
            }

            var user = new IdentityUser
            {
                Email = username,
                EmailConfirmed = true,
                NormalizedEmail = username.ToUpper(),
                PhoneNumber = "+7900800553535",
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456");

            var userStore = new UserStore<IdentityUser>(context);
            var identityResult = await userStore.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                throw new ArgumentException();
            }
            var userManger = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            foreach (var role in roles)
            {
                var identityResultRole = await userManger!.AddToRoleAsync(user, role);
                if (!identityResultRole.Succeeded)
                {
                    throw new ArgumentException();
                }
            }
            var category = new Category()
            {
                Name = "Классификация текста",
                Description = "Text classification is the task of assigning a sentence or document an appropriate category. The categories depend on the chosen dataset and can range from topics."
            };
            await context.Categories.AddAsync(category);
            category = new Category()
            {
                Name = "Анализ тональности",
                Description = "Sentiment analysis is the task of classifying the polarity of a given text. For instance, a text-based tweet can be categorized into either \"positive\", \"negative\", or \"neutral\". ",
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var algorithm = new Algorithm()
            {
                Category = context.Categories.Where(c => c.Name == "Классификация текста").First(),
                Name = "Модель заголовков",
                Content = "A topic model is a type of statistical model for discovering the abstract that occur in a collection of documents. Topic modeling is a frequently used text-mining tool for the discovery of hidden semantic structures in a text body.",
                Link = "https://github.com/mind-Lab/octis"
            };
            await context.Algorithms.AddAsync(algorithm);
            algorithm = new Algorithm()
            {
                Category = context.Categories.Where(c => c.Name == "Анализ тональности").First(),
                Name = "Анализ тональности твиттера",
                Content = "Twitter sentiment analysis is the task of performing sentiment analysis on tweets from Twitter.",
                Link = "https://github.com/lopezbec/COVID19_Tweets_Dataset"
            };
            await context.Algorithms.AddAsync(algorithm);
            algorithm = new Algorithm()
            {
                Category = context.Categories.Where(c => c.Name == "Классификация текста").First(),
                Name = "Двойное контрастное обучение",
                Content = "Contrastive learning has achieved remarkable success in representation learning via self-supervision in unsupervised settings. However, effectively adapting contrastive learning to supervised learning tasks remains as a challenge in practice.",
                Link = "https://github.com/hiyouga/dual-contrastive-learning"
            };
            await context.Algorithms.AddAsync(algorithm);
            algorithm = new Algorithm()
            {
                Category = context.Categories.Where(c => c.Name == "Классификация текста").First(),
                Name = "Контекстуализированные тематические модели",
                Content = "Contextualized Topic Models are based on the Neural-ProdLDA variational autoencoding approach by Srivastava and Sutton (2017).",
                Link = "https://github.com/MilaNLProc/contextualized-topic-models"
            };
            await context.Algorithms.AddAsync(algorithm);
            await context.SaveChangesAsync();

            var tag = new TaskAlg()
            {
                Title = "Анализ заголовков",
                Description = "ddd",
                Algorithms = new List<Algorithm>() 
                { 
                    context.Algorithms.Where(t => t.Name == "Модель заголовков").First(),
                    context.Algorithms.Where(t => t.Name == "Контекстуализированные тематические модели").First()
                }
            };
            await context.TaskAlg.AddAsync(tag);
            tag = new TaskAlg()
            {
                Title = "Субъективный анализ",
                Description = "Анализирует отношение, мнение, чувства о продукте, услуге, явлении",
                Algorithms = new List<Algorithm>()
                {
                    context.Algorithms.Where(t => t.Name == "Двойное контрастное обучение").First(),
                    context.Algorithms.Where(t => t.Name == "Анализ тональности твиттера").First()
                }
            };
            await context.TaskAlg.AddAsync(tag);
            await context.SaveChangesAsync();
        }
    }
}