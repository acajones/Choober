using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using static Android.Gms.Maps.GoogleMap;
using Android.Locations;
using Android.Content.PM;
using System.Threading;
using Android.Widget;
using Android.Views;
using Geocoding.Google;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Net.Http;

namespace Chuber
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback, IOnMyLocationClickListener,
        IOnMyLocationButtonClickListener
    {
        private const int PERMISSION_REQUEST_CODE = 1;
        private static GoogleMap _map;
        private MapFragment _mapFragment;
        private static Location _myLocation;
        private LocationManager _locationManager;
        private static Marker deviceMarker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            InitMapFragment();
            GeoTest();
        }

        private async void GeoTest()
        {
            Geocoding.IGeocoder geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyAXD-mMtl9NDibrKZzYt6J2CvGr5_NKThI" };
            IEnumerable<Geocoding.Address> addresses = await geocoder.GeocodeAsync("1600 pennsylvania ave washington dc");
            Console.WriteLine("Formatted: " + addresses.First().FormattedAddress); //Formatted: 1600 Pennsylvania Ave SE, Washington, DC 20003, USA
            Console.WriteLine("Coordinates: " + addresses.First().Coordinates.Latitude + ", " + addresses.First().Coordinates.Longitude); //Coordinates: 38.8791981, -76.9818437
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            // asks the user for the required permissions and checks if they are Granted
            RequestPermissions(new String[] { "android.permission.ACCESS_FINE_LOCATION" }, PERMISSION_REQUEST_CODE);
            Permission mapPermission = CheckSelfPermission("android.permission.ACCESS_FINE_LOCATION");
            if (mapPermission == Permission.Granted)
            {
                _map.MyLocationEnabled = true;
                _map.SetOnMyLocationClickListener(this);
                _map.SetOnMyLocationButtonClickListener(this);

            }

            // might be able to be used to continuously update the device's location
            _locationManager = (LocationManager)GetSystemService(LocationService);
            var locationListener = new UserLocation();
            _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 2000, 0, locationListener); // for testing
            // 5 seconds between location checks and a min of 5 meters for an update
            // These values should save on battery
            //_locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 5000, 5, locationListener);

            // gets the device's last known location
            _myLocation = _locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
            LatLng latLng = new LatLng(_myLocation.Latitude, _myLocation.Longitude);

            // update the camera location to the device location and set initial marker
            UpdateCamera(latLng, 10);
            AddPoint();
        }

        public bool OnMyLocationButtonClick()
        {
            return false;
        }

        public void OnMyLocationClick(Location location)
        {
        }

        /// <summary>
        /// Initializes the map fragment on the screen
        /// </summary>
        private void InitMapFragment()
        {
            _mapFragment = (MapFragment)FragmentManager.FindFragmentByTag("map");

            if (_mapFragment == null)
            {
                GoogleMapOptions mapOptions = new GoogleMapOptions()
                    .InvokeMapType(GoogleMap.MapTypeNormal)
                    .InvokeZoomControlsEnabled(false)
                    .InvokeCompassEnabled(true);
                FragmentTransaction fragTx = FragmentManager.BeginTransaction();
                _mapFragment = MapFragment.NewInstance(mapOptions);
                fragTx.Add(Resource.Id.map, _mapFragment, "map");
                fragTx.Commit();
            }
            _mapFragment.GetMapAsync(this);
        }

        /// <summary>
        /// Updates the camera location and attributes 
        /// </summary>
        /// <param name="latLng"></param>
        private void UpdateCamera(LatLng latLng, int zoom)
        {
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(latLng);
            builder.Zoom(zoom);
            builder.Bearing(0);
            builder.Tilt(0);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            // if the map is not null, set the needed features of the map
            if (_map != null)
            {
                _map.UiSettings.ZoomControlsEnabled = true;
                _map.UiSettings.CompassEnabled = true;
                _map.MoveCamera(cameraUpdate);
            }
        }

        /// <summary>
        /// Called when user position is updated according to params in RequestLocationUpdates
        /// and adds marker to the map
        /// </summary>
        /// <param name="location">The location of the device</param>
        public static void UpdateLocation(Location location)
        {
            _myLocation = location;
        }

        List<Marker> markerList = new List<Marker>();
        public void AddPoint()
        {
            //markerList.RemoveAt(0);
            deviceMarker = _map.AddMarker(new MarkerOptions()
                .SetPosition(new LatLng(_myLocation.Latitude, _myLocation.Longitude))
                .Draggable(true));
            markerList.Add(deviceMarker);
        }
    }
}

