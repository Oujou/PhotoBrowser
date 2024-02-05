namespace PhotoBrowser.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public Address? address { get; set; }
        public string phone { get; set; } = string.Empty;
        public string website { get; set; } = string.Empty;
        public Company? company { get; set; }

    }
}
