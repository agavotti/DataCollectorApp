using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebFormsApp.Models;
using WebFormsApp.Services;

namespace WebFormsApp.Controllers
{
    public class StoredAlbumsController : Controller
    {
        private readonly ApiService _apiService;

        public StoredAlbumsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string filterTitle)
        {
            var albums = await _apiService.GetStoredAlbumsAsync();

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


        public async Task<IActionResult> AddAlbum(string title, int userId)
        {
            var album = new Album { Title = title, UserId = userId };
            try
            {
                var response = await _apiService.AddAlbum(album);

                return View(response);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;

                return View(album);
            }
        }
    }
}