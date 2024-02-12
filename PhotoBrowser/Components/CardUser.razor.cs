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
        [Parameter]
        public EventCallback<int> UserClicked { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }

        [Parameter]
        public bool Selected { get; set; } = false;

        private string CardClass => Selected ? "card-content" : "card-content hidden";
        private string CardOpened => Selected ? "ArrowRight.png" : "ArrowDown.png";

        private void HandleUserSelection()
        {
            if (UserData is not null) UserSelected.InvokeAsync(UserData.id);
        }

        private void HandleUserActivation()
        {
            if (UserData is not null) UserClicked.InvokeAsync(UserData.id);
        }

        private void HandleOnClick()
        {
            if (UserData is not null) UserSelected.InvokeAsync(UserData.id);
        }

    }
}