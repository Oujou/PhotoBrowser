using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Photos : IDisposable
    {
        [Inject]
        private IDataService? Data { get; set; }

        [Inject]
        private NavigationManager? Nav { get; set; }

        [Inject]
        public IJSRuntime? JsRuntime { get; set; }

        [Parameter]
        public int? albumId { get; set; }

        [Parameter]
        public int? userId { get; set; }

        public string ForWhat
        {
            get
            {
                if (albumId != null) return $"album {albumId}";
                if (userId != null) return $"user {userId}";
                return "you here...";
            }
        }

        protected override Task OnInitializedAsync()
        {
            if (Data is not null) Data.OnChange += Update;
            return base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Data is not null)
            {
                await Data.UpdateData();
                Data.SelectedAlbumId = albumId;
                Data.SelectedUserId = userId;
            }
        }

        private void HandlePhotoSelection(int id)
        {
            Nav?.NavigateTo($"/photo/{id}");
        }

        private async Task ScrollToTop()
        {
            if (JsRuntime is not null) await JsRuntime.InvokeVoidAsync("OnScrollTopEvent");
        }

        private void Update()
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            if (Data is not null) Data.OnChange -= Update;
            GC.SuppressFinalize(this);
        }
    }
}