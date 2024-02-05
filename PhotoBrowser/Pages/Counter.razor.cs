using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Counter
    {

        [Inject]
        public IClientService? Client { get; set; }

        [Inject]
        public IDataService? Data { get; set; }

        private int currentCount = 0;

        private List<User> users = [];

        protected override async Task OnInitializedAsync()
        {
            if (Client != null && Data != null)
            {
                Data.SetUsers(await Client.GetUsers());
                Data.SetAlbums(await Client.GetAlbums());
                Data.SetPhotos(await Client.GetPhotos());
            }
        }

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}