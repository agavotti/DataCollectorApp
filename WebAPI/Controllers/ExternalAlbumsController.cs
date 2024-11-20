using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Domain.Entities;
using Infrastructure;

[ApiController]
[Route("api/[controller]")]
public class ExternalAlbumsController : ControllerBase
{
    private readonly ExternalApiService _externalApiService;

    public ExternalAlbumsController(ExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    [HttpGet("albums")]
    public IActionResult GetExternalAlbums()
    {
        var albums = _externalApiService.GetAlbums();
        return Ok(albums);
    }

    [HttpGet("photos")]
    public IActionResult GetExternalPhotos()
    {
        var photos = _externalApiService.GetPhotos();
        return Ok(photos);
    }
}