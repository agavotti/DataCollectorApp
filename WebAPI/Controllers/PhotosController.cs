using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly IPhotoRepository _photoRepository;

    public PhotosController(IPhotoRepository photoRepository)
    {
        _photoRepository = photoRepository;
    }

    [HttpGet]
    public IActionResult GetPhotos(string? title = null, int? albumId = null)
    {
        var photos = _photoRepository.GetPhotos(title, albumId);
        return Ok(photos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetPhotoById(int id)
    {
        var photo = _photoRepository.GetPhotoById(id);
        if (photo == null) return NotFound();
        return Ok(photo);
    }

    // POST: api/photos
    [HttpPost]
    [Route("")]
    public IActionResult CreatePhoto([FromBody] Photo photo)
    {
        try
        {
            _photoRepository.CreatePhoto(photo);
            return Created($"api/photos/{photo.Id}", photo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // PUT: api/photos/{id}
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdatePhoto(int id, [FromBody] Photo photo)
    {
        if (id != photo.Id) return BadRequest();
        _photoRepository.UpdatePhoto(photo);
        return Ok(photo);
    }

    // DELETE: api/photos/{id}
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeletePhoto(int id)
    {
        _photoRepository.DeletePhoto(id);
        return Ok();
    }
}