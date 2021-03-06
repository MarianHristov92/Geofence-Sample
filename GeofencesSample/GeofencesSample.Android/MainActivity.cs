namespace GeofencesSample.Droid
{
    using Android;
    using Android.App;
	using Android.Content;
	using Android.Content.PM;
	using Android.OS;
	using Android.Runtime;

	[Activity(Label = "GeofencesSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static Context MainContext { get; set; }

		const int RequestLocationId = 0;

		readonly string[] LocationPermissions =
		{
			Manifest.Permission.AccessCoarseLocation,
			Manifest.Permission.AccessFineLocation
		};

		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);
			MainContext = this;


			if ((int)Build.VERSION.SdkInt >= 28)
			{
				if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
				{
					RequestPermissions(LocationPermissions, RequestLocationId);
				}
				else
				{
					// Permissions already granted - display a message.
				}
			}

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			Xamarin.FormsMaps.Init(this, savedInstanceState);

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			if (requestCode == RequestLocationId)
			{
				if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
				{
					// Permissions granted - display a message.
				}
				else
				{
					// Permissions denied - display a message.
				}
			}
			else
			{
				base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			}
		}
	}
}