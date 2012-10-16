using System;
using Android.Content;
using Android.Net;
using Android.OS;

namespace monodroidfragmentbootstrapper
{
	public class FragBootstrapHelper
	{
		/// <summary>
		/// Change this is you want debugging
		/// </summary>
		public const bool DEBUG = true;

		/// <summary>
		/// Log string
		/// </summary>
		public const string LOGGER = "Bootstrapper";

		private static Context context;
		public FragBootstrapHelper ()
		{
		}

		/// <summary>
		/// Check if internet conncetion is up
		/// </summary>
		/// <returns>
		/// <c>true</c> if is online in specified context; otherwise, <c>false</c>.
		/// </returns>
		/// <param name='context'>
		/// The application context
		/// </param>
		public static bool IsOnline(Context context)
		{
			ConnectivityManager cm = context.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
			NetworkInfo netInfo = cm.ActiveNetworkInfo;
			return netInfo != null && netInfo.IsConnectedOrConnecting;
		}

		/// <summary>
		/// Shorthand-function for updating something when online and updating the UI when done
		/// </summary>
		/// <param name='context'>
		/// The application context
		/// </param>
		/// <param name='item'>
		/// Item.
		/// </param>
		/// <param name='callback'>
		/// Callback.
		/// </param>
		/// <param name='position'>
		/// Position.
		/// </param>
		public static void UpdateDataWithABSRefresh(Context context, 
		                                            MenuItem item, 
		                                            SecondListFragment.OnFragmentUpdateListener callback, 
		                                            int position)
		{
			if(IsOnline(context))
				new UpdateDataWithABSRefreshTask(context, item, callback, position).Execute();
		}

		/// <summary>
		/// Shorthand-function for doing something ASyncTask-y while online only
		/// </summary>
		/// <param name='context'>
		/// The application context.
		/// </param>
		public static void UpdateData(Context context)
		{
			if(IsOnline())
				new UpdateDataTask(context).Execute();
		}

		/// <summary>
		/// Updating something with ASyncTask while updating the UI after it's done.
		/// </summary>			
		private static class UpdateDataWithABSRefreshTask : AsyncTask<Void, Void, Void>
		{
			SecondListFragment.OnFragmentUpdateListener callback;
			Context context;
			MenuItem item;
			int position;

			private UpdateDataWithABSRefreshTask(Context context, MenuItem item, SecondListFragment.OnFragmentUpdateListener callback, int position) : base() { 
				this.context = context;
				this.item = item;
				this.callback = callback;
				this.position = position;
			}

			protected override void OnPreExecute ()
			{
				item.SetActionView(Resource.Layout.menuitem_action_refresh);
			}

			protected override Java.Lang.Object DoInBackground (params Object[] native_parms)
			{
				if(IsOnline(context)) {
					// do fancy download stuff here
				}

				return null;
			}

			protected override void OnPostExecute (Void result)
			{
				callback.OnFragmentUpdate(position, true);
				item.SetActionView(null);
			}
		}

		/// <summary>
		/// Do something and don't update the UI
		/// </summary>			
		private static class UpdateDataTask : AsyncTask<Void, Void, Void>
		{
			Context context;

			private UpdateDataTask(Context context) : base()
			{
				this.context = context;
			}

			protected override Java.Lang.Object DoInBackground (params Object[] native_parms)
			{
				if(IsOnline(context))
				{
					// do fancy online stuff here
				}

				return null;
			}
		}
		
		/**
     * Do something and don't update the UI
     */


	}
}

