using System;
using Android;
using Android.AccessibilityServices;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;

namespace AccessibilityServiceExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}

    [Service(Label = "AccessibilityServiceExample", Permission = Manifest.Permission.BindAccessibilityService)]
    [IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
    [MetaData("android.accessibilityservice", Resource = "@xml/accessibility_service")]
    public class MyAccessibilityService : AccessibilityService
    {
        public override void OnAccessibilityEvent(AccessibilityEvent e)
        {
            try
            {

                Console.WriteLine("PackageName: " + e.PackageName);
                Console.WriteLine("EventType: " + e.EventType);
                Console.WriteLine("Source: " + e.Source);
                Console.WriteLine("WindowId: " + e.WindowId);
               
                Console.WriteLine("ContentDescription: " + e.ContentDescription);
                

                //RootInActiveWindow

                var Text=  e.Text;
                if (Text!=null)
                {
                    Console.WriteLine("Text: " + string.Join(" ", Text));
                }
                Console.WriteLine("EventTime: " + e.EventTime);



            }
            catch (Exception)
            {
               
            }
        }
        public void performViewClick(AccessibilityNodeInfo nodeInfo)
        {
            if (nodeInfo == null)
            {
                return;
            }
            while (nodeInfo != null)
            {
                if (nodeInfo.Clickable)
                {
                    nodeInfo.PerformAction(Android.Views.Accessibility.Action.Click);
                    break;
                }
                nodeInfo = nodeInfo.Parent;
            }
        }

        public override void OnInterrupt()
        {
            //throw new NotImplementedException();
        }
        protected override void OnServiceConnected()
        {
            base.OnServiceConnected();

        }
    }
}

