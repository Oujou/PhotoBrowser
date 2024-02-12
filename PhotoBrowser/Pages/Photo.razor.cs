using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Photo : IDisposable
    {
        [Inject]
        public IDataService? Data { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }

        [Parameter]
        public int photoId { get; set; }

        private PhotoBrowser.Models.Photo? photo => Data?.AllPhotos.FirstOrDefault(x => x.id == photoId);
        private string Owner
        {
            get
            {
                if (photo is not null && Data is not null) 
                    return Data.GetUserNameByAlbum(photo.albumId);
                return "";
            }
        }

        public void Dispose()
        {
            if (Data is not null) Data.OnChange -= Update;
            GC.SuppressFinalize(this);
        }

        protected override Task OnInitializedAsync()
        {
            if (Data is not null) Data.OnChange += Update;
            return base.OnInitializedAsync();
        }

        private void Update()
        {
            StateHasChanged();
        }

        private void UserClick()
        {
            try
            {
                var userId = Data?.Albums.Find(x => x.id == photo?.albumId)?.userId;
                nav?.NavigateTo("/users?user=" + userId);
            }
            catch (ArgumentNullException)
            {
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Data != null) await Data.UpdateData();
        }
    }
}