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

        public async Task<IActionResult> Index(string filterTitle)
        {
            var photos = await _apiService.GetStoredPhotosAsync();

            if (!string.IsNullOrEmpty(filterTitle))
            {
                photos = photos.FindAll(a => a.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
            }


            var viewModel = new PhotoViewModel
            {

                Photos = photos,
                FilterTitle = filterTitle
            };

            ViewData["filterTitle"] = filterTitle;
            return View(photos);
        }

        //public async Task<IActionResult> Details(int photoId)
        //{
        //    var photos = await _apiService.GetPhotos();

        //    photos = photos.FindAll(a => a.PhotoId.Equals(photoId));

        //    return View(photos);
        //}


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