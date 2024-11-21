using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebFormsApp.Models;
using WebFormsApp.Services;

namespace WebFormsApp.Controllers
{
    public class StoredPhotosController : Controller
    {
        private readonly ApiService _apiService;

        public StoredPhotosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string filterTitle, int? filterAlbum = null)
        {
            var photos = await _apiService.GetStoredPhotosAsync();

            if (!string.IsNullOrEmpty(filterTitle))
            {
                photos = photos.FindAll(a => a.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
            }

            if (filterAlbum != null)
            {
                photos = photos.FindAll(a => a.AlbumId.Equals(filterAlbum));

            }

            var viewModel = new PhotoViewModel
            {

                Photos = photos,
                FilterTitle = filterTitle
            };

            ViewData["filterTitle"] = filterTitle;
            ViewData["filterAlbum"] = filterAlbum;
            return View(photos);
        }

        public async Task<IActionResult> AddPhoto(string title, int albumId)
        {
            var photo = new Photo { Title = title, AlbumId = albumId };
            try
            {
                var response = await _apiService.AddPhoto(photo);

                return View(response);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;

                return View(photo);
            }
        }
    }
}