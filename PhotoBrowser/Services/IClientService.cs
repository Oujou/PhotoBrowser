using PhotoBrowser.Models;

namespace PhotoBrowser.Services
{
    public interface IClientService
    {
        Task<List<User>> GetUsers();
        Task<List<Album>> GetAlbums();
        Task<List<Photo>> GetPhotos();
    }
}