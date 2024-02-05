using PhotoBrowser.Models;

namespace PhotoBrowser.Services
{
    public class DataService : IDataService
    {
        private List<User> _Users { get; set; } = new List<User>();
        private List<Photo> _Photos { get; set; } = new List<Photo>();
        private List<Album> _Albums { get; set; } = new List<Album>();

        public List<Album> Albums => _Albums;
        public List<Photo> Photos => _Photos;
        public List<User> Users => _Users;

        public List<Photo> GetPhotosByAlbum(int albumId)
        {
            return _Photos.Where(x => x.albumId == albumId).ToList();
        }

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
    }
}
