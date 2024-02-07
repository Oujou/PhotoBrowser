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
        
        public int PhotosCount => _photos.Count;
        private List<Photo> _photos
        {
            get
            {
                if (SelectedUserId is not null) return _photosByUserId;
                if (SelectedAlbumId is not null) return _photosByAlbumId;
                return _Photos;
            }
        }

        public bool HasPagination => _photos.Count > PageSize;

        private int? _selectedAlbumId { get; set; }
        private int? _selectedUserId { get; set; }
        private List<Photo> _photosByUserId { get; set; } = new();
        private List<Photo> _photosByAlbumId { get; set; } = new();

        public int? SelectedAlbumId
        {
            get
            {
                return _selectedAlbumId;
            }
            set
            {
                Console.WriteLine($"SET SelectedAlbumId '{value}' / Old is '{_selectedAlbumId}'");
                if (_selectedAlbumId != value)
                {
                    if (value is null)
                    {
                        _photosByAlbumId.Clear();
                    }
                    else
                    {
                        _photosByAlbumId = _Photos.Where(photo => photo.albumId == value).ToList();
                    }
                    _selectedAlbumId = value;
                }
            }
        }

        public int? SelectedUserId
        {
            get
            {
                return _selectedUserId;
            }
            set
            {
                if (_selectedUserId != value)
                {
                    if (value is null)
                    {
                        _photosByUserId.Clear();
                    }
                    else
                    {
                        _photosByUserId = _Photos.Where(photo => _Albums.FindAll(album => album.userId == value).Select(item => item.id).Contains(photo.albumId)).ToList();
                    }
                    _selectedUserId = value;
                }
            }
        }

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

        public string GetUserNameByAlbum(int id) // for Debug
        {
            var user = _Users.Find(user => user.id == _Albums.Find(album => album.id == id)?.userId);
            return user?.name + " / ID: " + user?.id;
        }
    }
}
