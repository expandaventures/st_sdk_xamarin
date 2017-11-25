using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace SinTrafico.Xamarin.Forms
{
    public static class GeoLocator
    {
        public static event EventHandler<PositionEventArgs> PositionChanged;

        public static event EventHandler<PositionErrorEventArgs> PositionError;

        public static Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPositionAsync(TimeSpan? timeout = null, CancellationToken? token = null, bool includeHeading = false)
        {
            if (!CrossGeolocator.IsSupported)
            {
                throw new NotSupportedException("Location feature not supported");
            }
            return CrossGeolocator.Current.GetPositionAsync(timeout, token, includeHeading);
        }

        public static Task<Plugin.Geolocator.Abstractions.Position> GetLastKnownPositionAsync()
        {
            if (!CrossGeolocator.IsSupported)
            {
                throw new NotSupportedException("Location feature not supported");
            }
            return CrossGeolocator.Current.GetLastKnownLocationAsync();
        }

        public static Task<bool> ListenUserLocationUpdatesAsync(TimeSpan minimumTime, double minimumDistance, bool includeHeading = false, ListenerSettings listenerSettings = null)
        {
            if (!CrossGeolocator.IsSupported)
            {
                throw new NotSupportedException("Location feature not supported");
            }
            if (PositionChanged == null)
            {
                throw new MissingMemberException("To start listen user lodation updates first set a handler in SinTrafico.Xamarin.Forms.Forms.PositionChanged");
            }
            RemoveLocationHandlers();
            AddLocationHandlers();
            return CrossGeolocator.Current.StartListeningAsync(minimumTime, minimumDistance, includeHeading, listenerSettings);
        }

        public static Task<bool> StopListenUserLocationUpdates()
        {
            RemoveLocationHandlers();
            return CrossGeolocator.Current.StopListeningAsync();
        }

        static void AddLocationHandlers()
        {
            CrossGeolocator.Current.PositionError += Handle_PositionError;
            CrossGeolocator.Current.PositionChanged += Handle_PositionChanged;
        }

        static void RemoveLocationHandlers()
        {
            CrossGeolocator.Current.PositionError -= Handle_PositionError;
            CrossGeolocator.Current.PositionChanged -= Handle_PositionChanged;
        }

        static void Handle_PositionChanged(object sender, PositionEventArgs e) => PositionChanged?.Invoke(sender, e);

        static void Handle_PositionError(object sender, PositionErrorEventArgs e) => PositionError?.Invoke(sender, e);
    }
}
