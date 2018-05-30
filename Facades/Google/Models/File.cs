namespace server.Facades.Google.Models
{
    public class File : Resource
    {
      public const string FolderMimeType = "application/vnd.google-apps.folder";

      public string Name { get; set; }

      public string MimeType { get; set; }
    }
}