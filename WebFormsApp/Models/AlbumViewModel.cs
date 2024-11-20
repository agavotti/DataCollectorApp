using Domain.Entities;

namespace WebFormsApp.Models
{
    public class AlbumViewModel
    {
        public List<Album> Albums { get; set; }
        public string FilterTitle { get; set; }
    }
}
