using ArchaeoTrack;
using ArchaeoTrack.Interfaces;
using Foundation;
using SafariServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

[assembly: Dependency( typeof( iOSFileDownloader ) )]
namespace ArchaeoTrack {
    public class iOSFileDownloader : IFileDownloader {
        public void DownloadFile( string base64Image ) {
            try {
                // Convert base64 string to byte array
                var base64 = base64Image.Substring( base64Image.IndexOf( "," ) + 1 );
                var imageBytes = Convert.FromBase64String( base64 );

                // Get the documents directory
                var documentsDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
                var fileName = "skica.png"; // Set your desired file name
                var filePath = Path.Combine( documentsDirectory, fileName );

                // Write the file to storage
                using( var stream = new FileStream( filePath, FileMode.Create ) ) {
                    stream.Write( imageBytes, 0, imageBytes.Length );
                }

                // Notify the user (using a UIAlertController)
                var alert = UIAlertController.Create( "Success", "Image downloaded successfully", UIAlertControllerStyle.Alert );
                alert.AddAction( UIAlertAction.Create( "OK", UIAlertActionStyle.Default, null ) );
                var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                topController.PresentViewController( alert, true, null );
            }
            catch( Exception ex ) {
                // Handle exceptions and notify the user
                var alert = UIAlertController.Create( "Error", ex.Message, UIAlertControllerStyle.Alert );
                alert.AddAction( UIAlertAction.Create( "OK", UIAlertActionStyle.Default, null ) );
                var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                topController.PresentViewController( alert, true, null );
            }
        }
    }
}
