using System;
using Android.Support.V4.App;
using Android.Content;
using Android.App;
using Android.Views;
using Android.OS;
using Android.Support.V4.View;
using ViewPagerIndicator;
using Android.Widget;

namespace monodroidfragmentbootstrapper.Fragments
{
	public class SecondListFragment : Fragment
	{
		OnFragmentUpdateListener mCallback;
		private Context context;
		private int mExampleId = 0;

		public int ExampleId 
		{ 
			get 
			{ 
				return mExampleId; 
			} 
		}


		/// <summary>
		/// Fragment update interface
		/// </summary>
		public interface OnFragmentUpdateListener
		{
			void OnFragmentUpdate(int position, bool forceUpdate);

			int ExampleId { get; }
		}

		/// <summary>
		/// Make sure the activity has implemented OnFragmentUpdateListener
		/// </summary>
		/// <param name='activity'>
		/// Our activity
		/// </param>
		public override void OnAttach (Activity activity)
		{
			base.OnAttach(activity);

			try
			{
				mCallback = activity as OnFragmentUpdateListener;
			} 
			catch(InvalidCastException ex)
			{
				throw new InvalidCastException(activity.ToString() + " must implement OnFragmentUpdateListener");
			}
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			if(container != null)
				container.RemoveAllViews();

			mExampleId = Arguments.GetInt("mExampleId");
			context = Activity.ApplicationContext;

			View view = inflater.Inflate(Resource.Layout.viewpager_second, container, false);
			CreatePagerView(view);

			return view;
		}

		public override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState(outState);
			outState.PutInt("mExampleId", mExampleId);
		}

		public void CreatePagerView(View view)
		{
			ViewPager awesomePager = view.FindViewById(Resource.Id.titles) as ViewPager;
			awesomePager.Adapter = new ExamplePagerAdapter();

			TabPageIndicator titleIndicator = view.FindViewById(Resource.Id.titles) as TabPageIndicator;
			titleIndicator.SetViewPager(awesomePager);
		}

		public static SecondListFragment newInstance(int exampleId)
		{
			SecondListFragment f = new SecondListFragment();

			// Supply index input as argument.
			Bundle args = new Bundle();
			args.PutInt("mExampleId", exampleId);
			f.Arguments = args;

			return f;
		}

		/// <summary>
		/// ExamplePagerAdapter initializes the ListViews that we want to
		/// scroll between. It inflates the listview through a separate inflater.
		/// </summary>			
		private class ExamplePagerAdapter : PagerAdapter
		{
			string[] pages = { "First page", "Second page", "Third page" };
			int[] pages_strings = { Resource.Array.second_list, Resource.Array.third_list };
			string pageContent;

			private ExamplePagerAdapter()
			{
			}

			public override int Count {
				get {
					return pages.Length;
				}
			}

			public override bool IsViewFromObject (View view, Java.Lang.Object o)
			{
				return view == (ListView) o;
			}

			public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object view)
			{
				(container as ViewPager).RemoveView(view as View);
			}

			public override Java.Lang.Object InstantiateItem (ViewGroup container, int position)
			{
				// create a new listview
				ListView listView = new ListView(context);
				IListAdapter adapter = new SecondListFragment(pages_strings[position], context);

				// bind the adapter to the listview
				listView.Adapter = adapter;

				(container as ViewPager).AddView(listView, 0);

				return listView;
			}

			public string GetPageTitle(int position)
			{
				return pages[position];
			}
		}
	}
}

