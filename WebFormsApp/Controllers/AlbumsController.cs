using Microsoft.AspNetCore.Mvc;
using WebFormsApp.Models;
using WebFormsApp.Services;

namespace WebFormsApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ApiService _apiService;

        public AlbumsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string filterTitle)
        {
            var albums = await _apiService.GetAlbumsAsync();

            if (!string.IsNullOrEmpty(filterTitle))
            {
                albums = albums.FindAll(a => a.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
            }


            var viewModel = new AlbumViewModel
            {

                Albums = albums,
                FilterTitle = filterTitle
            };

            ViewData["filterTitle"] = filterTitle;
            return View(albums);
        }

        public async Task<IActionResult> Details(int albumId)
        {
            var photos = await _apiService.GetPhotos();

            photos = photos.FindAll(a => a.AlbumId.Equals(albumId));

            return View(photos);
        }
    }
}