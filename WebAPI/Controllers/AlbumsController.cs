using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumsController(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    [HttpGet]
    public IActionResult GetAlbums(string title = null)
    {
        var albums = _albumRepository.GetAlbums(title);
        return Ok(albums);
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetAlbumById(int id)
    {
        var album = _albumRepository.GetAlbumById(id);
        if (album == null) return NotFound();
        return Ok(album);
    }

    // POST: api/albums
    [HttpPost]
    [Route("")]
    public IActionResult CreateAlbum([FromBody] Album album)
    {
        try
        {
            _albumRepository.CreateAlbum(album);
            return Created($"api/albums/{album.Id}", album);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    // PUT: api/albums/{id}
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult UpdateAlbum(int id, [FromBody] Album album)
    {
        if (id != album.Id) return BadRequest();
        _albumRepository.UpdateAlbum(album);
        return Ok(album);
    }

    // DELETE: api/albums/{id}
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult DeleteAlbum(int id)
    {
        try
        {


            _albumRepository.DeleteAlbum(id);
            return Ok();
        }
        catch (Exception ex) 
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}