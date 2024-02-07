using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Users
    {
        [Inject]
        public IDataService? Data { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }

        private void HandleUserSelection(int id)
        {
            Data?.FirstPage();
            nav?.NavigateTo($"/photos/user/{id}");
        }
    }
}