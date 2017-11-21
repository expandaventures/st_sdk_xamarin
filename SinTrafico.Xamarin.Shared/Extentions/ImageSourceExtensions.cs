using System;
using Xamarin.Forms;
using System.Threading.Tasks;
#if __IOS__
using Xamarin.Forms.Platform.iOS;
using UIKit;
#elif __ANDROID__
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Content;
#endif

namespace SinTrafico.Xamarin.Shared.Extentions
{
    internal static class ImageSourceExtensions
    {
        internal static IImageSourceHandler GetHandler(this ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }


#if __IOS__
        internal static Task<UIImage> GetNativeSourceAsync(this ImageSource source) => source.GetHandler().LoadImageAsync(source);
#elif __ANDROID__
        internal static Task<Bitmap> GetNativeSourceAsync(this ImageSource source, Context context) => source.GetHandler().LoadImageAsync(source, context);
#endif
    }
}
