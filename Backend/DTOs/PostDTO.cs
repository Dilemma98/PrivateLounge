public class PostWithImageDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<IFormFile> Images { get; set; } = new();
}
