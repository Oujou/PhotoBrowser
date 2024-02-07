using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;
using PhotoBrowser.Models;

namespace PhotoBrowser.Components
{
    public partial class CardAlbum
    {
        [Parameter]
        public Album? AlbumData { get; set; }

        [Parameter]
        public EventCallback<int> AlbumSelected { get; set; }

        [Inject]
        private IDataService? Data { get; set; }

        private void HandleAlbumSelection()
        {
            if (AlbumData == null) { return; }
            AlbumSelected.InvokeAsync(AlbumData.id);
        }
    }
}