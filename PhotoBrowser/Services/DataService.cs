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
        public List<User> Users => _Users;
        
        public List<Photo> Photos => _photos.Skip(Skip * PageSize).Take(PageSize).ToList();

        private List<Photo> _photos
        {
            get
            {
                List<Photo> list = new List<Photo>();
                if (SelectedUserId is not null) list = _Photos.Where(photo => _Albums.FindAll(album => album.userId == SelectedUserId).Select(item => item.id).Contains(photo.albumId)).ToList();
                if (SelectedAlbumId is not null) list = _Photos.Where(photo => photo.albumId == SelectedAlbumId).ToList();
                if (SelectedUserId is null && SelectedAlbumId is null) list = _Photos;
                return list;
            }
        }

        public bool HasPagination =>_photos.Count > PageSize;
        
        public int? SelectedAlbumId { get; set; }
        public int? SelectedUserId { get; set; }

        public bool HasPhotos => _Photos.Count > 0;

        public bool HasAlbums => _Albums.Count > 0;

        public bool HasUsers => _Users.Count > 0;

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

        private int Skip { get; set; } = 0;
        private int PageSize { get; set; } = 25;
        public int Page => Skip + 1;
        public int TotalPages => _photos.Count / PageSize;

        public void IncPage()
        {
            if (Skip < (_photos.Count / PageSize) - 1) Skip++;
        }

        public void DecPage()
        {
            if (Skip > 0) Skip--;
        }

        public void FirstPage()
        {
            Skip = 0;
        }

        public void LastPage()
        {
            Skip = (_photos.Count - 1) / PageSize;
        }

        public string GetUserNameByAlbum(int id)
        {
            var user = _Users.Find(user => user.id == _Albums.Find(album => album.id == id)?.userId);
            return user?.name + " / ID: " + user?.id;
        }
    }
}
