using PhotoBrowser.Models;

namespace PhotoBrowser.Services
{
    public interface IDataService
    {
        List<Album> Albums { get; }
        List<Photo> Photos { get; }
        List<User> Users { get; }

        void SetAlbums(List<Album> albums);
        void SetPhotos(List<Photo> photos);
        List<Photo> GetPhotosByAlbum(int albumId);
        List<Photo> GetPhotosByUser(int userId);
        void SetUsers(List<User> users);
    }
}