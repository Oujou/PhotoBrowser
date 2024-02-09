using PhotoBrowser.Models;

namespace PhotoBrowser.Services
{
    public interface IClientService<T>
    {
        Task<ResponseModel<T>> Get(CancellationToken CToken);
    }
}