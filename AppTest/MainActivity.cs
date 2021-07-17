using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Speech;

namespace AppTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity 
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Button btn = (Button)FindViewById(Resource.Id.button1);
            btn.Click += delegate
            {
                Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                intent.PutExtra(RecognizerIntent.ExtraLanguage, "fa");
                intent.PutExtra(RecognizerIntent.ExtraMaxResults, 20);
                intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 4000);
                intent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 4000);
                intent.PutExtra(RecognizerIntent.ExtraPartialResults, false);
                intent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 500);
                intent.PutExtra(RecognizerIntent.ExtraPrompt, "اکنون صحبت کنید");
                StartActivityForResult(intent, 1);
            };
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok && requestCode == 1)
            {
                var nat = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                Toast.MakeText(this, nat[0], ToastLength.Long).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}