package md50d7622d338265f9bda3c37163374dd43;


public class MainActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.OnMapReadyCallback,
		com.google.android.gms.maps.GoogleMap.OnMyLocationClickListener,
		com.google.android.gms.maps.GoogleMap.OnMyLocationButtonClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onMapReady:(Lcom/google/android/gms/maps/GoogleMap;)V:GetOnMapReady_Lcom_google_android_gms_maps_GoogleMap_Handler:Android.Gms.Maps.IOnMapReadyCallbackInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_onMyLocationClick:(Landroid/location/Location;)V:GetOnMyLocationClick_Landroid_location_Location_Handler:Android.Gms.Maps.GoogleMap/IOnMyLocationClickListenerInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"n_onMyLocationButtonClick:()Z:GetOnMyLocationButtonClickHandler:Android.Gms.Maps.GoogleMap/IOnMyLocationButtonClickListenerInvoker, Xamarin.GooglePlayServices.Maps\n" +
			"";
		mono.android.Runtime.register ("Chuber.MainActivity, Chuber", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("Chuber.MainActivity, Chuber", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onMapReady (com.google.android.gms.maps.GoogleMap p0)
	{
		n_onMapReady (p0);
	}

	private native void n_onMapReady (com.google.android.gms.maps.GoogleMap p0);


	public void onMyLocationClick (android.location.Location p0)
	{
		n_onMyLocationClick (p0);
	}

	private native void n_onMyLocationClick (android.location.Location p0);


	public boolean onMyLocationButtonClick ()
	{
		return n_onMyLocationButtonClick ();
	}

	private native boolean n_onMyLocationButtonClick ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
