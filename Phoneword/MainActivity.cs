using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Telephony;

namespace Phoneword
{
    [Activity(Label = "Phoneword", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);

            // Disable the "Call" button
            callButton.Enabled = false;

            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += (object sender, EventArgs e) =>
            {
                // On "Call" button click, try to dial phone number.
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate {
                    // Create intent to dial phone
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                // Show the alert dialog to the user and wait for response.
                callDialog.Show();
            };
        }

        /*protected override void OnStop()
        {
            base.OnPause();

            /*EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);

            phoneNumberText.Text = "Kulawy chuj";
            translateButton.Text = "Kulawy chuj";
            callButton.Text = "Kulawy chuj";
            
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("Kulawy chuj\nI na co Ci było minimalizowanie?\nNie masz przycisku, żeby stąd wyjść i co? Jesteś w pułapce");
            //callDialog.SetNegativeButton("Cancel", delegate { });
            callDialog.Show();

            var callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:" + 609941530));
            StartActivity(callIntent);

            SmsManager.Default.SendTextMessage("609941530", null,"Kulawy chuj", null, null);
            var callDialog = new AlertDialog.Builder(this);
            callDialog.SetMessage("SMS wysłany");
            callDialog.SetNeutralButton("Ok", delegate { });

            // Show the alert dialog to the user and wait for response.
            callDialog.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            var callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:" + 609941530));
            StartActivity(callIntent);
        }
        //protected override ON*/
    }
}

