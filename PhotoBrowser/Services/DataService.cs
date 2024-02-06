using PhotoBrowser.Models;
using System.Reflection;

namespace PhotoBrowser.Services
{
    public class DataService : IDataService
    {
        private List<User> _Users { get; set; } = new List<User>();
        private List<Photo> _Photos { get; set; } = new List<Photo>();
        private List<Album> _Albums { get; set; } = new List<Album>();

        public List<Album> Albums => _Albums;

        public event Action? OnChange;

        private int Skip { get; set; } = 0;
        private int PageSize { get; set; } = 25;
        public int Page => Skip + 1;
        public int TotalPages => _PhotosCount / PageSize;

        public List<Photo> Photos
        {
            get
            {
                if (SelectedAlbumId is null) return _Photos.Skip(Skip * PageSize).Take(PageSize).ToList();
                return _Photos.Where(x => x.albumId == SelectedAlbumId).Skip(Skip * PageSize).Take(PageSize).ToList();
            }
        }

        public bool HasPagination => SelectedAlbumId is null 
            ? _Photos.Count > PageSize 
            : _Photos.Where(x => x.albumId == SelectedAlbumId).Count() > PageSize;

        private int _PhotosCount => SelectedAlbumId is null
            ? _Photos.Count
            : _Photos.Where(x => x.albumId == SelectedAlbumId).Count();

        public List<User> Users => _Users;

        public int? SelectedAlbumId { get; set; }

        public bool HasPhotos => _Photos.Count > 0;

        public bool HasAlbums => _Albums.Count > 0;

        public bool HasUsers => _Users.Count > 0;

        public List<Photo> GetPhotosByUser(int userId)
        {
            return _Photos.Where(x => _Albums.FindAll(x => x.userId == userId).Select(x => x.id).ToList().Contains(x.albumId)).ToList();
        }

        public void SetUsers(List<User> users)
        {
            _Users = users;
        }

        public void SetPhotos(List<Photo> photos)
        {
            _Photos = photos;
        }

        public void SetAlbums(List<Album> albums)
        {
            _Albums = albums;
        }

        public void IncPage()
        {
            Console.WriteLine($"{Skip} < {_PhotosCount} / {PageSize}");
            if (Skip < (_PhotosCount / PageSize) - 1) Skip++;
            OnChange?.Invoke();
        }

        public void DecPage()
        {
            if (Skip > 0) Skip--;
            OnChange?.Invoke();
        }

        public void FirstPage()
        {
            Skip = 0;
            OnChange?.Invoke();
        }

        public void LastPage()
        {
            Console.WriteLine("LastPage");
            Skip = (_PhotosCount - 1) / PageSize;
            OnChange?.Invoke();
        }
    }
}
