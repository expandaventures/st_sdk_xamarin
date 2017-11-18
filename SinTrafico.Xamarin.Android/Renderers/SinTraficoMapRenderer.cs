using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Gms.Maps;
using SinTrafico.Xamarin.Android;
using SinTrafico.Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using NativePolyline = Android.Gms.Maps.Model.Polyline;
using NativePosition = Android.Gms.Maps.Model.LatLng;
using BitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;
using BitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
using Marker = Android.Gms.Maps.Model.Marker;
using System.Threading.Tasks;
using SinTrafico.Xamarin.Android.Helpers;
using System.Reflection;
using System.Net.Http;
using System.IO;
using Android.Graphics.Drawables;
using SinTrafico.Xamarin.Shared.Extentions;

[assembly: ExportRenderer(typeof(SinTraficoMap), typeof(SinTraficoMapRenderer))]
namespace SinTrafico.Xamarin.Android
{
    public class SinTraficoMapRenderer : MapRenderer
    {
        static HttpClient _defaultHttpClient = new HttpClient();

        public const string DEFAULT_PIN_DOWNLOAD_URL = "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|{0}";

        public new SinTraficoMap Element => base.Element as SinTraficoMap;

        List<NativePolyline> _polylines;

        public static void Init()
        {
            var dummy = DateTime.Now;
        }

        public SinTraficoMapRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var referenceMap = e.NewElement as SinTraficoMap;
                (referenceMap.PolyLines as INotifyCollectionChanged).CollectionChanged += PolyLines_CollectionChanged;
            }
            if (e.OldElement != null)
            {
                var referenceMap = e.OldElement as SinTraficoMap;
                (referenceMap.PolyLines as INotifyCollectionChanged).CollectionChanged -= PolyLines_CollectionChanged;
            }
        }

        void PolyLines_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddPolylines(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovePolylines(e.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    RemovePolylines(e.OldItems);
                    AddPolylines(e.NewItems);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _polylines?.ForEach(m => m.Remove());
                    _polylines = null;
                    AddPolylines((IList)Element.PolyLines);
                    break;
                case NotifyCollectionChangedAction.Move:
                    //do nothing
                    break;
            }
        }

        void AddPolylines(IList items)
        {
            GoogleMap map = this.NativeMap;
            if (map == null)
            {
                return;
            }
            if (this._polylines == null)
            {
                this._polylines = new List<NativePolyline>();
            }
            this._polylines.AddRange(items.Cast<Polyline>().Select(p =>
            {
                var options = this.CreatePolyline(p);
                var poly = map.AddPolyline(options);
                p.Id = poly.Id;
                return poly;
            }));
        }

        void RemovePolylines(IList items)
        {
            if (this.NativeMap == null)
            {
                return;
            }
            if (this._polylines == null)
            {
                return;
            }
            IEnumerator enumerator = items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Polyline p = (Polyline)enumerator.Current;
                    var polyline = this._polylines.FirstOrDefault((NativePolyline m) => m.Id == (string)p.Id);
                    if (polyline != null)
                    {
                        polyline.Remove();
                        this._polylines.Remove(polyline);
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        protected override global::Android.Gms.Maps.Model.MarkerOptions CreateMarker(Pin pin)
        {
            var extendedPin = pin as SinTraficoPin;
            if (extendedPin != null)
            {
                if (extendedPin.Icon == null)
                {
                    Task.Run(async () => await LoadPinColorResource(extendedPin));
                }
                else
                {
                    Task.Run(async () => await LoadPinSourceAsync(extendedPin));
                }
            }
            return base.CreateMarker(pin); ;
        }

        protected virtual global::Android.Gms.Maps.Model.PolylineOptions CreatePolyline(Polyline polyline) => new global::Android.Gms.Maps.Model.PolylineOptions()
                                         .Add(polyline.Points.Select(point => new NativePosition(point.Latitude, point.Longitude)).ToArray())
                                         .InvokeWidth((float)polyline.LineWidth)
                                         .InvokeColor(polyline.LineColor.ToAndroid().ToArgb());

        //TODO: review in the future if there's a better way to achieve this
        async Task LoadPinColorResource(SinTraficoPin extendedPin)
        {
            var hexValue = extendedPin.PinColor.GetHexString();
            var filePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{hexValue}.png");
            if (!File.Exists(filePath))
            {
                var urlToDownload = string.Format(DEFAULT_PIN_DOWNLOAD_URL, hexValue);
                var bytes = await _defaultHttpClient.GetByteArrayAsync(urlToDownload);
                File.WriteAllBytes(filePath, bytes);
            }

            if (extendedPin.Id == null)
            {
                await Task.Delay(10);
            }

            var markers = GetInterMarkersList();
            var markerToUpdate = markers.FirstOrDefault((Marker m) => m.Id == (string)extendedPin.Id);
            if (markerToUpdate != null)
            {
                using (var bitmapdraw = (BitmapDrawable)Drawable.CreateFromPath(filePath))
                {
                    using (var b = bitmapdraw.Bitmap)
                    {
                        var customMarker = global::Android.Graphics.Bitmap.CreateScaledBitmap(b, 63, 102, false);

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            markerToUpdate.SetIcon(BitmapDescriptorFactory.FromBitmap(customMarker));
                        });
                    }
                }
            }
        }


        async Task LoadPinSourceAsync(SinTraficoPin extendedPin)
        {
            var nativeImage = await extendedPin.Icon.GetNativeSourceAsync(Context);
            if (extendedPin.Id == null)
            {
                await Task.Delay(10);
            }

            var markers = GetInterMarkersList();
            var markerToUpdate = markers.FirstOrDefault((Marker m) => m.Id == (string)extendedPin.Id);
            if (markerToUpdate != null && nativeImage != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    markerToUpdate.SetIcon(BitmapDescriptorFactory.FromBitmap(nativeImage));
                });
            }
        }

        List<Marker> GetInterMarkersList()
        {
            var markersField = typeof(MapRenderer).GetField("_markers", BindingFlags.Instance | BindingFlags.NonPublic);
            return (List<Marker>)markersField.GetValue(this) ?? new List<Marker>();
        }
    }
}
