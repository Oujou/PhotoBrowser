using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using PhotoBrowser.Services;

namespace PhotoBrowser.Components
{
    public partial class CardPhoto
    {
        [Parameter]
        public Photo? PhotoData { get; set; }
        
        [Parameter]
        public EventCallback<int> PhotoSelected { get; set; }

        [Inject]
        private IDataService? Data { get; set; }

        private void HandlePhotoClick()
        {
            if (PhotoData == null) { return; }
            PhotoSelected.InvokeAsync(PhotoData.id);
        }
    }
}