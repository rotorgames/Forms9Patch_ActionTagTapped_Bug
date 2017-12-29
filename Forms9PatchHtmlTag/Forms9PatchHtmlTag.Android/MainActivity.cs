using Android.App;
using Android.Content.PM;
using Android.OS;
using Forms9Patch.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Forms9PatchHtmlTag.Droid
{
    [Activity(Label = "Forms9PatchHtmlTag", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Settings.Initialize(this, "23MQ-3B4R-WYE2-Y86S-RUZH-SRQM-ZEPT-XZR8-WAVD-QLUT-WL7V-B9Z8-5KM4");
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}