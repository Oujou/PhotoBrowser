using Microsoft.AspNetCore.Components;
using PhotoBrowser.Services;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace PhotoBrowser.Pages
{
    public partial class Users : IDisposable
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

        protected override Task OnInitializedAsync()
        {
            if (Data is not null) Data.OnChange += Update;
            return base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Data != null)
            {
                await Data.UpdateData();
            }
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
            if (SelectedUser == id)
            {
                id = -1;
            }
            SelectedUser = id;
            nav?.NavigateTo("/users?user=" + id);
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