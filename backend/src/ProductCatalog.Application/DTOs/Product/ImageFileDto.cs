public class ImageFileDto
{
    public Stream ImageStream { get; set; } = Stream.Null;
    public string ImageFileName { get; set; } = string.Empty;
    public string ImageContentType { get; set; } = string.Empty;
}