using System.Net;
using Function;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunction
{
    public class HttpExample
    {
        private readonly ILogger _logger;
        private readonly BloggingContext _context;

        public HttpExample(ILoggerFactory loggerFactory, BloggingContext context)
        {
            _logger = loggerFactory.CreateLogger<HttpExample>();
            _context = context;
        }

        [Function("HttpExample")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Using: " + _context.Database.GetConnectionString());

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            try
            {
                response.WriteString(_context.Database.CanConnect()
                    ? "Server Version: " + _context.Database.GetDbConnection().ServerVersion
                    : "No database connection");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to connect to the database.");
                response.WriteString("Error while trying to connect to the database.");
            }

            return response;
        }

        [Function("GetPosts")]
        public async Task<HttpResponseData> GetPosts(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "posts")] HttpRequestData req,
                FunctionContext context)
        {
            var logger = context.GetLogger("GetPosts");
            logger.LogInformation("Get Posts HTTP trigger function processed a request.");
            var postsArray = _context.Posts.OrderBy(p => p.Title).ToArray();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(postsArray);
            return response;
        }

        [Function("CreateBlog")]
        public async Task<HttpResponseData> CreateBlogAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "blog")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("CreateBlog");
            logger.LogInformation("Create Blog HTTP trigger function processed a request.");

            var blog = await req.ReadFromJsonAsync<Blog>();

            if (blog != null)
            {
                var entity = await _context.Blogs.AddAsync(blog, CancellationToken.None);
                await _context.SaveChangesAsync(CancellationToken.None);

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(entity.Entity);
                return response;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString("Invalid blog data");
                return response;
            }
        }

        [Function("CreatePost")]
        public async Task<HttpResponseData> CreatePostAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "blog/{id}/post")] HttpRequestData req,
            int id,
            FunctionContext context)
        {
            var logger = context.GetLogger("CreatePost");
            logger.LogInformation("Create Post HTTP trigger function processed a request.");

            var post = await req.ReadFromJsonAsync<Post>();
            if (post != null)
            {
                post.BlogId = id;

                var entity = await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync(CancellationToken.None);

                var response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(entity.Entity);
                return response;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString("Invalid post data");
                return response;
            }
        }
    }
}
