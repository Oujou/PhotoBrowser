using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PhotoBrowser.Services;

namespace PhotoBrowser.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public IClientService? Client { get; set; }

        [Inject]
        public IDataService? Data { get; set; }
        
        
        protected override async Task OnInitializedAsync()
        {
            if (Client != null && Data != null)
            {
                Data.SetUsers(await Client.GetUsers());
                Data.SetAlbums(await Client.GetAlbums());
                Data.SetPhotos(await Client.GetPhotos());
            }
        }

        
    }
}