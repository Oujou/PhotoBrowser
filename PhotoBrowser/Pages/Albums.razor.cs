using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Albums : IDisposable
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
        protected override Task OnInitializedAsync()
        {
            if (Data is not null) Data.OnChange += Update;
            return base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Data is not null) Data.OnChange += Update;
            if (Data != null) await Data.UpdateData();
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