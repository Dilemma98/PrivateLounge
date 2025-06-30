using Backend.Models;
using Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(_context.Posts.OrderByDescending(p => p.CreatedAt).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }
            return Ok(post);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromForm] PostWithImageDto postDto)
        {
            if (string.IsNullOrWhiteSpace(postDto.Title) ||
                string.IsNullOrWhiteSpace(postDto.Content)
            )
            {
                return BadRequest("Invalid post data.");
            }

            // Spara alla bilder och samla deras paths
            var imagePaths = new List<string>();
            if (postDto.Images != null && postDto.Images.Count > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                foreach (var image in postDto.Images)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    imagePaths.Add($"/uploads/{uniqueFileName}");
                }
            }

            // Spara bild-URL:er som kommaseparerad sträng (eller byt till en egen tabell om du vill)
            var post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CreatedAt = DateTime.Now,
                ImageUrl = imagePaths.Count > 0 ? string.Join(",", imagePaths) : null
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            if (updatedPost == null || string.IsNullOrEmpty(updatedPost.Title) || string.IsNullOrEmpty(updatedPost.Content))
            {
                return BadRequest("Invalid post data.");
            }

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}