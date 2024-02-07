using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhotoBrowser.Components
{
    public partial class CardUser
    {
        [Parameter]
        public User? UserData { get; set; }

        [Parameter]
        public EventCallback<int> UserSelected { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }


        private void HandleUserSelection()
        {
            if (UserData is not null) UserSelected.InvokeAsync(UserData.id);
        }
    }
}