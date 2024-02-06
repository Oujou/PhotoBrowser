using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;

namespace PhotoBrowser.Pages
{
    public partial class Users
    {
        [Inject]
        public IDataService? Data { get; set; }

    }
}