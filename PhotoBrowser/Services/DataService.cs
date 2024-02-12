using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using System.Reflection;

namespace PhotoBrowser.Services
{
    public class DataService : IDataService
    {
        public IClientService<User>? UserService { get; set; }
        public IClientService<Photo>? PhotoService { get; set; }
        public IClientService<Album>? AlbumService { get; set; }

        [Inject]
        public ILogger<DataService>? Logger { get; set; }

        private List<User> _Users { get; set; } = new List<User>();
        private List<Photo> _Photos { get; set; } = new List<Photo>();
        private List<Album> _Albums { get; set; } = new List<Album>();

        public List<Album> Albums => _Albums;
        public List<User> Users => _Users;

        public List<Photo> Photos => _photos.Skip(Skip * PageSize).Take(PageSize).ToList();
        public List<Photo> AllPhotos => _photos;

        public int PhotosCount => _photos.Count;
        private List<Photo> _photos
        {
            get
            {
                if (SelectedUserId != null) return _photosByUserId;
                if (SelectedAlbumId != null) return _photosByAlbumId;
                return _Photos;
            }
        }

        public async Task UpdateData()
        {
            if (!HasAlbums) _Albums = await GetAlbums();
            if (!HasUsers) _Users = await GetUsers();
            if (!HasPhotos) _Photos = await GetPhotos();
            OnChange?.Invoke();
        }

        public ResponseStatus ResponseStatus { get; private set; }

        public string ErrorMessage { get; private set; }

        private async Task<List<User>> GetUsers()
        {
            if (UserService == null) return new List<User>();
            var userResponse = await UserService.Get(CancellationToken.None);
            if (userResponse != null)
            {
                switch (userResponse.StatusCode)
                {
                    case ResponseStatus.Success:
                        ResponseStatus = ResponseStatus.Success;
                        ErrorMessage = string.Empty;
                        return userResponse.ResponseData;
                    case ResponseStatus.Failure:
                        Logger?.LogError(message: userResponse.ErrorMessage);
                        break;
                    default:
                        Logger?.LogError(message: "Error in response status.");
                        break;
                }
                ResponseStatus = ResponseStatus.Failure;
                ErrorMessage = userResponse.ErrorMessage;
            }
            return new List<User>();
        }

        private async Task<List<Photo>> GetPhotos()
        {
            if (PhotoService == null) return new List<Photo>();
            var photoResponse = await PhotoService.Get(CancellationToken.None);
            if (photoResponse != null)
            {
                switch (photoResponse.StatusCode)
                {
                    case ResponseStatus.Success:
                        ResponseStatus = ResponseStatus.Success;
                        ErrorMessage = string.Empty;
                        return photoResponse.ResponseData;
                    case ResponseStatus.Failure:
                        Logger?.LogError(message: photoResponse.ErrorMessage);
                        break;
                    default:
                        Logger?.LogError(message: "Error in response status.");
                        break;
                }
                ResponseStatus = ResponseStatus.Failure;
                ErrorMessage = photoResponse.ErrorMessage;
            }
            return new List<Photo>();
        }

        private async Task<List<Album>> GetAlbums()
        {
            if (AlbumService == null) return new List<Album>();
            var albumResponse = await AlbumService.Get(CancellationToken.None);
            if (albumResponse != null)
            {
                switch (albumResponse.StatusCode)
                {
                    case ResponseStatus.Success:
                        ResponseStatus = ResponseStatus.Success;
                        ErrorMessage = string.Empty;
                        return albumResponse.ResponseData;
                    case ResponseStatus.Failure:
                        Logger?.LogError(message: albumResponse.ErrorMessage);
                        break;
                    default:
                        Logger?.LogError(message: "Error in response status.");
                        break;
                }
                ResponseStatus = ResponseStatus.Failure;
                ErrorMessage = albumResponse.ErrorMessage;
            }
            return new List<Album>();
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

        public event Action? OnChange;

        public void SetUsers(List<User> users)
        {
            _Users = users;
            OnChange?.Invoke();
        }

        public void SetPhotos(List<Photo> photos)
        {
            _Photos = photos;
            OnChange?.Invoke();
        }

        public void SetAlbums(List<Album> albums)
        {
            _Albums = albums;
            OnChange?.Invoke();
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
            try
            {
                var user = _Users.Find(user => user.id == _Albums.Find(album => album.id == id)?.userId);
                return user?.name ?? "";

            }
            catch (ArgumentNullException)
            {
                return "";
            }
        }

        public int GetPhotoCountByAlbum(int id)
        {
            try
            {
                var photoCount = _Photos.FindAll(x => x.albumId == id).Count;
                return photoCount;

            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }
    }
}
