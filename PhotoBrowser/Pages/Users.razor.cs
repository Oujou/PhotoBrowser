using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace PhotoBrowser.Pages
{
    public partial class Users
    {
        [Inject]
        public IDataService? Data { get; set; }

        [Inject]
        public NavigationManager? nav { get; set; }

        [SupplyParameterFromQuery]
        [Parameter]
        public string? user { get; set; }

        private int SelectedUser { get; set; } = -1;

        private bool IsSelected(int id) => SelectedUser == id;

        protected override void OnParametersSet()
        {
            if (user != null)
            {
                try
                {
                    SelectedUser = int.Parse(user);
                }
                catch (Exception)
                {
                    SelectedUser = -1;
                }

            }
            base.OnParametersSet();
        }
        private void HandleUserSelection(int id)
        {
            Data?.FirstPage();
            nav?.NavigateTo($"/photos/user/{id}");
        }

        private void HandleUserToggle(int id)
        {   
            SelectedUser = id;
            nav?.NavigateTo("/users?user=" + id);
        }
    }
}