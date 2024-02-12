using PhotoBrowser.Models;

namespace PhotoBrowser.Services
{
    public interface IDataService
    {
        List<Album> Albums { get; }
        List<Photo> Photos { get; }
        List<User> Users { get; }
        List<Photo> AllPhotos { get; }

        public event Action? OnChange;

        int PhotosCount { get; }

        bool HasPhotos { get; }
        bool HasAlbums { get; }
        bool HasUsers { get; }

        void SetAlbums(List<Album> albums);
        void SetPhotos(List<Photo> photos);
        void SetUsers(List<User> users);

        int? SelectedAlbumId { get; set; }
        int? SelectedUserId { get; set; }
     
        // Pagination
        bool HasPagination { get; }
        int Page { get; }
        int TotalPages { get; }

        void IncPage();
        void DecPage();
        void FirstPage();
        void LastPage();

        string GetUserNameByAlbum(int id);
        int GetPhotoCountByAlbum(int id);
    }
}