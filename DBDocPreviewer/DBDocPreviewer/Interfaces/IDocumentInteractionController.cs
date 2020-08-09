using System;
namespace DBDocPreviewer.Interfaces
{
    public interface IDocumentInteractionController
    {
        void DownloadAndOpenFile(string url, string folder);
    }
}
