﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebFormsApp.Models;
using WebFormsApp.Services;

namespace WebFormsApp.Controllers
{
    public class PhotosController : Controller
    {
        private readonly ApiService _apiService;

        public PhotosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string filterTitle, int? filterAlbum = null)
        {
            var photos = await _apiService.GetPhotos();


            if (!string.IsNullOrEmpty(filterTitle))
            {
                photos = photos.FindAll(a => a.Title.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
            }

            if (filterAlbum != null)
            {
                photos = photos.FindAll(a => a.AlbumId.Equals(filterAlbum));

            }

            ViewData["filterTitle"] = filterTitle;
            ViewData["filterAlbum"] = filterAlbum;


            return View(photos);
        }

        public async Task<IActionResult> AddPhoto(string title, int albumId, string thumbnailUrl, string url)
        {
            var photo = new Photo { Title = title, AlbumId = albumId, ThumbnailUrl = thumbnailUrl, Url = url };
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