using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PhotoBrowser.Services;
using PhotoBrowser.Models;
using Microsoft.Extensions.Logging;

namespace PhotoBrowser.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public IClientService<User>? UserClient { get; set; }
        [Inject]
        public IClientService<Photo>? PhotoClient { get; set; }
        [Inject]
        public IClientService<Album>? AlbumClient { get; set; }
        [Inject]
        public ILogger<MainLayout>? Logger { get; set; }

        [Inject]
        public IDataService? Data { get; set; }
                
        protected override async Task OnInitializedAsync()
        {
            CancellationToken ctx = CancellationToken.None;

            if (UserClient != null && PhotoClient != null && AlbumClient != null && Data != null)
            {
                var userResponse = await UserClient.Get(ctx);
                var photoResponse = await PhotoClient.Get(ctx);
                var albumResponse = await AlbumClient.Get(ctx);
;
                if (userResponse != null)
                {
                    switch (userResponse.StatusCode)
                    {
                        case ResponseStatus.Success:
                            Data.SetUsers(userResponse.ResponseData);
                            break;
                        case ResponseStatus.Failure:
                            Logger?.LogError(message: userResponse.ErrorMessage);
                            break;
                        default:
                            Logger?.LogError(message: "Error in response status.");
                            break;
                    }
                }
                if (albumResponse != null)
                {
                    switch (albumResponse.StatusCode)
                    {
                        case ResponseStatus.Success:
                            Data.SetAlbums(albumResponse.ResponseData);
                            break;
                        case ResponseStatus.Failure:
                            Logger?.LogError(message: albumResponse.ErrorMessage);
                            break;
                        default:
                            Logger?.LogError(message: "Error in response status.");
                            break;
                    }
                }
                if (photoResponse != null)
                {
                    switch (photoResponse.StatusCode)
                    {
                        case ResponseStatus.Success:
                            Data.SetPhotos(photoResponse.ResponseData);
                            break;
                        case ResponseStatus.Failure:
                            Logger?.LogError(message: photoResponse.ErrorMessage);
                            break;
                        default:
                            Logger?.LogError(message: "Error in response status.");
                            break;
                    }
                }
            }
        }

        
    }
}