namespace GeofencesSample.Droid.Services.Geofences
{
	using Android.Content;
    using Android.Gms.Location;
    using GeofencesSample.Droid.Services.Notifications;
    using System;
    using System.Collections.Generic;

    [BroadcastReceiver()]
	public class GeofenceBroadcastReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			GeofencingEvent geofencingEvent = GeofencingEvent.FromIntent(intent);
			if (geofencingEvent.HasError)
			{
				string error = GeofenceStatusCodes.GetStatusCodeString(geofencingEvent.ErrorCode);
				Console.WriteLine($"Error Code: {geofencingEvent.ErrorCode}. Error: {error}");
				return;
			}

			// Get the transition type.
			int geofenceTransition = geofencingEvent.GeofenceTransition;
			// Get the geofences that were triggered. A single event can trigger
			// multiple geofences.
			IList<IGeofence> triggeringGeofences = geofencingEvent.TriggeringGeofences;
			NotificationsService notificationsService = new NotificationsService(context);

			switch (geofenceTransition)
			{
				case Geofence.GeofenceTransitionEnter:
					notificationsService.SendNotification("¡Enter!", "iWe have caught you close!", 10000);
					break;
				case Geofence.GeofenceTransitionExit:
					notificationsService.SendNotification("¡You went out!", "¡We caught you coming out!", 10000);
					break;
				case Geofence.GeofenceTransitionDwell:
					notificationsService.SendNotification("¡You stay!", "¿¡You don't move!?", 10000);
					break;
				default:
					// Log the error.
					Console.WriteLine("Broadcast not implemented.");
					break;
			}
		}
	}
}