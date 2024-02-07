using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Photos
    {
        [Inject]
        private IDataService? Data { get; set; }

        [Inject]
        private NavigationManager? Nav { get; set; }

        [Parameter]
        public int? albumId { get; set; }
        
        [Parameter]
        public int? userId { get; set; }
        
        protected override void OnParametersSet()
        {
            if (Data is not null)
            {
                Data.SelectedAlbumId = albumId;
                Data.SelectedUserId = userId;
            }
            base.OnParametersSet();
        }

        private void HandlePhotoSelection(int id)
        {
            Nav?.NavigateTo($"/photo/{id}");
        }
    }
}