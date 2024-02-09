using Microsoft.AspNetCore.Components;
using PhotoBrowser.Models;
using System;
using System.Net.Http.Json;
using System.Threading;

namespace PhotoBrowser.Services
{
    public class UserService : IClientService<User>
    {
        private HttpClient Http { get; set; }

        public UserService(HttpClient http)
        {
            Http = http;
        }

        public async Task<ResponseModel<User>> Get(CancellationToken CToken)
        {
            ResponseModel<User> responseModel = new ResponseModel<User>();

            try
            {
                var response = await Http.GetFromJsonAsync<List<User>>($"https://jsonplaceholder.typicode.com/users", CToken);
                if (response is not null)
                {
                    responseModel.ResponseData = response;
                    responseModel.StatusCode = ResponseStatus.Success;
                }
                else
                {
                    responseModel.StatusCode = ResponseStatus.Failure;
                    responseModel.ErrorMessage = "Response from server was invalid";
                }
            }
            catch (Exception e)
            {
                responseModel.ErrorMessage = "Error at data transfer.";
                responseModel.StatusCode = ResponseStatus.Failure;
            }
            return responseModel;
        }
    }

    public class AlbumService : IClientService<Album>
    {
        private HttpClient Http { get; set; }

        public AlbumService(HttpClient http)
        {
            Http = http;
        }

        public async Task<ResponseModel<Album>> Get(CancellationToken CToken)
        {
            ResponseModel<Album> responseModel = new ResponseModel<Album>();

            try
            {
                var response = await Http.GetFromJsonAsync<List<Album>>($"https://jsonplaceholder.typicode.com/albums", CToken);
                if (response is not null)
                {
                    responseModel.ResponseData = response;
                    responseModel.StatusCode = ResponseStatus.Success;
                }
                else
                {
                    responseModel.StatusCode = ResponseStatus.Failure;
                    responseModel.ErrorMessage = "Response from server was invalid";
                }
            }
            catch (Exception e)
            {
                responseModel.ErrorMessage = "Error at data transfer.";
                responseModel.StatusCode = ResponseStatus.Failure;
            }
            return responseModel;
        }
    }

    public class PhotoService : IClientService<Photo>
    {
        private HttpClient Http { get; set; }

        public PhotoService(HttpClient http)
        {
            Http = http;
        }

        public async Task<ResponseModel<Photo>> Get(CancellationToken CToken)
        {
            ResponseModel<Photo> responseModel = new ResponseModel<Photo>();

            try
            {
                var response = await Http.GetFromJsonAsync<List<Photo>>($"https://jsonplaceholder.typicode.com/photos", CToken);
                if (response is not null)
                {
                    responseModel.ResponseData = response;
                    responseModel.StatusCode = ResponseStatus.Success;
                }
                else
                {
                    responseModel.StatusCode = ResponseStatus.Failure;
                    responseModel.ErrorMessage = "Response from server was invalid";
                }
            }
            catch (Exception e)
            {
                responseModel.ErrorMessage = "Error at data transfer.";
                responseModel.StatusCode = ResponseStatus.Failure;
            }
            return responseModel;
        }
    }
}
