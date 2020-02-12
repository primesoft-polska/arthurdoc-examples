namespace ArthurDoc.Client.Models
{
    public class DownloadedFile
    {
        public string Name { get; set; }
        public byte[] FileContent { get; set; }

        public string Path { get; set; }
    }
}
