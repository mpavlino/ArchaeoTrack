using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Webkit;

namespace ArchaeoTrack {
    [Activity( Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density )]
    public class MainActivity : MauiAppCompatActivity {
        protected override void OnCreate( Bundle savedInstanceState ) {
            base.OnCreate( savedInstanceState );

            // Enable download support in the WebView
            Android.Webkit.WebView.SetWebContentsDebuggingEnabled( true );
            Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping( "EnableDownload", ( handler, view ) => {
                handler.PlatformView.SetDownloadListener( new CustomDownloadListener() );
            } );
        }

        // Custom download listener for handling file downloads in WebView
        private class CustomDownloadListener : Java.Lang.Object, IDownloadListener {
            public void OnDownloadStart( string url, string userAgent, string contentDisposition, string mimetype, long contentLength ) {
                // Trigger file download using external download manager or other mechanism
                var intent = new Intent( Intent.ActionView );
                intent.SetData( Android.Net.Uri.Parse( url ) );
                intent.AddFlags( ActivityFlags.NewTask );
                Android.App.Application.Context.StartActivity( intent );
            }
        }
    }
}
