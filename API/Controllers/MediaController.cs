using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IFileService _fileService;

    public MediaController(IFileService fileService) => _fileService = fileService;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest("ფაილი არ არის არჩეული");

        var imageUrl = await _fileService.SaveFileAsync(file, "events");

        return Ok(new { url = imageUrl });
    }
}