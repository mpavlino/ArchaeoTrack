using Android.Content;
using Android.Widget;
using ArchaeoTrack;
using ArchaeoTrack.Interfaces;
using Application = Android.App.Application;
using Uri = Android.Net.Uri;

[assembly: Microsoft.Maui.Controls.Dependency(typeof( AndroidFileDownloader ) )]
namespace ArchaeoTrack {
    public class AndroidFileDownloader : IFileDownloader {
        public void DownloadFile( string base64Image, string fileName ) {
            // Remove the metadata prefix from the base64 string
            string base64Data = base64Image.Substring( base64Image.IndexOf( "," ) + 1 );

            // Decode the base64 string to a byte array
            byte[] imageBytes = Convert.FromBase64String( base64Data );

            // Create a file path to save the image
            //string fileName = "skica.png"; // You can customize the file name
            string path = Path.Combine( Android.OS.Environment.GetExternalStoragePublicDirectory( Android.OS.Environment.DirectoryPictures ).AbsolutePath, fileName );

            // Write the byte array to a file
            File.WriteAllBytes( path, imageBytes );

            // Notify the user that the file is downloaded
            Toast.MakeText( Application.Context, "Download completed: " + path, ToastLength.Long ).Show();
        }
    }
}
