namespace PhotoBrowser.Models
{
    public class ResponseModel<T>
    {
        public List<T> ResponseData { get; set; } = new List<T>();
        public string ErrorMessage { get; set; } = string.Empty;
        public ResponseStatus StatusCode { get; set; }
    }

    public enum ResponseStatus { Success, Failure }
}
