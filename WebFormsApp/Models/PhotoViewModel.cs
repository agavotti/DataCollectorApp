using Domain.Entities;

namespace WebFormsApp.Models
{
    public class PhotoViewModel
    {
        public List<Photo> Photos { get; set; }
        public string FilterTitle { get; set; }
    }
}
