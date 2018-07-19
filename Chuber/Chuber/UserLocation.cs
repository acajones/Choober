using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chuber
{
    class UserLocation : Java.Lang.Object, ILocationListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        public void OnLocationChanged(Location location)
        {
            MainActivity.UpdateLocation(location);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }
    }
}