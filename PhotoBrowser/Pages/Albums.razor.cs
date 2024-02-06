using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Albums
    {
        [Inject]
        public IDataService? Data { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }

        private void HandleAlbumSelection(int id)
        {
            Data?.FirstPage();
            nav?.NavigateTo($"/photos/album/{id}");
        }
    }
}