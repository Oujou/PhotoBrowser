using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Photo
    {
        [Inject]
        public IDataService? Data { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }
        [Parameter]
        public int photoId { get; set; }

        private PhotoBrowser.Models.Photo? photo => Data?.Photos.FirstOrDefault(x => x.id == photoId);
    }
}