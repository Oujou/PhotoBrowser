namespace PhotoBrowser.Models
{
    public class Address
    {
        public string street { get; set; } = string.Empty;
        public string suite { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string zipcode { get; set; } = string.Empty;
        public Geo? geo { get; set; }
    }
}
