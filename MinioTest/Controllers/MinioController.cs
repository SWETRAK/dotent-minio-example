using Microsoft.AspNetCore.Mvc;
using MinioTest.Services;

namespace MinioTest.Controllers;

[ApiController]
[Route("test")]
public class MinioController: Controller
{
    private readonly IMinioService _minioService;

    public MinioController(IMinioService minioService)
    {
        _minioService = minioService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetFile([FromQuery] Guid id)
    {
        var result = await _minioService.GetFile(id.ToString());
        return File(result.Item1.GetBuffer(), result.Item2, id.ToString());
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> CreateFile(IFormFile file)
    { 
        using var memoryStream = new MemoryStream();
        await using var fileStream = file.OpenReadStream();
        await fileStream.CopyToAsync(memoryStream);
        var filename = Guid.NewGuid();
        await _minioService.InsertFile(filename.ToString(), file.ContentType, memoryStream);
        return Ok(filename);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateFile([FromQuery] Guid id, IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await using var fileStream = file.OpenReadStream();
        await fileStream.CopyToAsync(memoryStream);
        await _minioService.InsertFile(id.ToString(), file.ContentType, memoryStream);
        return File(memoryStream.GetBuffer(), file.ContentType, id.ToString());
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteFile([FromQuery] Guid id)
    {
        await _minioService.RemoveFile(id.ToString());
        return Ok();
    }
}