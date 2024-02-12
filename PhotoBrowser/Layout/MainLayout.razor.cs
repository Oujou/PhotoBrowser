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
            if (UserClient != null && PhotoClient != null && AlbumClient != null && Data != null)
            {
                Data.AlbumService = AlbumClient;
                Data.PhotoService = PhotoClient;
                Data.UserService = UserClient;
                await Data.UpdateData();
            }
        }


    }
}