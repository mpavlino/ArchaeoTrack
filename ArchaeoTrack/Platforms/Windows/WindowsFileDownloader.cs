using ArchaeoTrack.Interfaces;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ArchaeoTrack.WinUI {
    public class WindowsFileDownloader : IFileDownloader {
        public async void DownloadFile( string base64Image ) {
            // Remove the prefix from the base64 string
            var base64 = base64Image.Substring( base64Image.IndexOf( "," ) + 1 );
            var imageBytes = Convert.FromBase64String( base64 );

            // Create a new file in the Pictures library
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            StorageFile file = await picturesFolder.CreateFileAsync( "skica.png", CreationCollisionOption.ReplaceExisting );

            // Write the image bytes to the file
            using( var stream = await file.OpenStreamForWriteAsync() ) {
                await stream.WriteAsync( imageBytes, 0, imageBytes.Length );
            }
            
            // Optionally, you can show a message to the user that the download is complete
            //var dialog = new Windows.UI.Popups.MessageDialog( "Image downloaded successfully!" );
            //await dialog.ShowAsync();
        }
    }
}
