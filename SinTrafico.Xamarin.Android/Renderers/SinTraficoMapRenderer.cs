﻿using System;
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
using Android.Content;
using GoogleMapsUtils.Android.Data.Geojson;

[assembly: ExportRenderer(typeof(SinTraficoMap), typeof(SinTraficoMapRenderer))]
namespace SinTrafico.Xamarin.Android
{
    public class SinTraficoMapRenderer : MapRenderer
    {
        static HttpClient _defaultHttpClient = new HttpClient();

        public const string DEFAULT_PIN_DOWNLOAD_URL = "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|{0}";

        public new SinTraficoMap Element => base.Element as SinTraficoMap;

        List<GeoJsonLayer> _polylines;

        public static void Init()
        {
            var dummy = DateTime.Now;
        }

        public SinTraficoMapRenderer(Context context): base(context)
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
                    _polylines?.ForEach(m => m.RemoveLayerFromMap());
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
                this._polylines = new List<GeoJsonLayer>();
            }

            this._polylines.AddRange(items.Cast<Polyline>().Select(p =>
            {
                var json = "{'type': 'Feature', 'geometry': " + p.GeoJson + ", 'properties': { 'color': '#" + p.LineColor.GetHexString() + "' }}";
                var layer = this.CreatePolyline(json);
                layer.DefaultLineStringStyle.Color = p.LineColor.ToAndroid().ToArgb();
                layer.DefaultLineStringStyle.SetLineStringWidth((float)p.LineWidth);
                layer.AddLayerToMap();
                p.Id = layer;
                return layer;
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
                    var polyline = this._polylines.FirstOrDefault((GeoJsonLayer m) => m == p.Id);
                    if (polyline != null)
                    {
                        polyline.RemoveLayerFromMap();
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
                    LoadPinColorResource(extendedPin).ConfigureAwait(false);
                }
                else
                {
                    LoadPinSourceAsync(extendedPin).ConfigureAwait(false);
                }
            }
            return base.CreateMarker(pin); ;
        }

        protected virtual GeoJsonLayer CreatePolyline(string json) => new GeoJsonLayer(NativeMap, new Org.Json.JSONObject(json));

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
            double width = nativeImage.Width;
            double height = nativeImage.Height;
            if(height < 100)
            {
                double heightFactor = (1 * 100) / height;
                height = height * heightFactor;
                width = width * heightFactor;
            }
            var customMarker = global::Android.Graphics.Bitmap.CreateScaledBitmap(nativeImage, (int)width, (int)height, false);
            nativeImage.Dispose();

            var markers = GetInterMarkersList();
            var markerToUpdate = markers.FirstOrDefault((Marker m) => m.Id == (string)extendedPin.Id);
            if (markerToUpdate != null && customMarker != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    markerToUpdate.SetIcon(BitmapDescriptorFactory.FromBitmap(customMarker));
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
