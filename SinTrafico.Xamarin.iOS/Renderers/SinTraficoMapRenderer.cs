using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using CoreLocation;
using MapKit;
using SinTrafico.Xamarin.Forms;
using SinTrafico.Xamarin.iOS;
using SinTrafico.Xamarin.Shared.Extentions;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SinTraficoMap), typeof(SinTraficoMapRenderer))]
namespace SinTrafico.Xamarin.iOS
{
    public class SinTraficoMapRenderer : SinTrafico.Xamarin.iOS.Renderers.MapRenderer
    {

        public new SinTraficoMap Element => base.Element as SinTraficoMap;

        public new MKMapView Control => base.Control as MKMapView;

        List<MKPolyline> _polylines;

        public static new void Init()
        {
            var dummy = DateTime.Now;
        }

        public SinTraficoMapRenderer()
        {
        }

        protected override void OnElementChanged(global::Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var referenceMap = e.NewElement as SinTraficoMap;
                (referenceMap.PolyLines as INotifyCollectionChanged).CollectionChanged += PolyLines_CollectionChanged;
                Control.OverlayRenderer = GetOverlayRenderer;
                Control.GetViewForAnnotation = GetViewForAnnotation;
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
                    _polylines?.ForEach(m => Control.RemoveOverlay(m));
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
            var map = Control;
            if (map == null)
            {
                return;
            }
            if (_polylines == null)
            {
                _polylines = new List<MKPolyline>();
            }

            foreach (Polyline p in items)
            {
                Polyline poly = p;
                var polilyne = CreatePolyline(poly);
                poly.Id = polilyne;
                Control.AddOverlay(polilyne);
                _polylines.Add(polilyne);
            }
        }

        void RemovePolylines(IList items)
        {
            var map = Control;
            if (map == null)
            {
                return;
            }
            if (_polylines == null)
            {
                return;
            }

            foreach (Polyline p in items)
            {
                var polyline = _polylines.FirstOrDefault(m => m == p.Id);
                if (polyline == null)
                {
                    continue;
                }
                Control.RemoveOverlay(polyline);
                _polylines.Remove(polyline);
            }
        }

        protected override IMKAnnotation CreateAnnotation(SinTrafico.Xamarin.Forms.Models.Pin pin) => (pin is SinTraficoPin) ? new SinTraficoMKPointAnnotation(pin as SinTraficoPin) : base.CreateAnnotation(pin);

        protected virtual MKPolyline CreatePolyline(Polyline polyline) => MKPolyline.FromCoordinates(polyline.Points.Select(pos => new CLLocationCoordinate2D(pos.Latitude, pos.Longitude)).ToArray());

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (overlayWrapper is MKPolyline)
            {
                var formsPoly = Element.PolyLines.Cast<Polyline>().FirstOrDefault(poly => poly.Id == overlayWrapper);
                if (formsPoly != null)
                {
                    var renderer = new MKPolylineRenderer(overlayWrapper as MKPolyline);
                    renderer.FillColor = formsPoly.LineColor.ToUIColor();
                    renderer.StrokeColor = formsPoly.LineColor.ToUIColor();
                    renderer.LineWidth = (nfloat)formsPoly.LineWidth;
                    return renderer;
                }
            }
            return new MKOverlayRenderer();
        }


        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            var extAnnotation = annotation as SinTraficoMKPointAnnotation;
            MKPinAnnotationView view = new MKPinAnnotationView(annotation, "sinTraficoPin");
            view.CanShowCallout = true;
            if(extAnnotation != null)
            {
                if (extAnnotation.Pin.Icon == null)
                {
                    view.PinTintColor = extAnnotation.Pin.PinColor.ToUIColor();
                }
                else
                {
                    LoadPinSourceAsync(view, extAnnotation.Pin).ConfigureAwait(false);
                }
            }
            return view;
        }

        async Task LoadPinSourceAsync(MKPinAnnotationView nativePinView, SinTraficoPin extendedPin)
        {
            var nativeImage = await extendedPin.Icon.GetNativeSourceAsync();
            if (extendedPin.Id == null)
            {
                await Task.Delay(10);
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                var markerToUpdate = Control.Annotations.FirstOrDefault((IMKAnnotation m) => m == extendedPin.Id);
                if (markerToUpdate != null && nativeImage != null)
                {
                    nativePinView.Image = nativeImage;
                }
            });
        }
    }
}
