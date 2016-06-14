using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Hangman.Assets;
using Hangman.Resources;
using Java.Security;

namespace Hangman
{
    [Activity(Label = "Hangman", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button btnNewGameScreen;
        private Button btnHighScores;
       
       // private Button btnQuit;
     
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

          

            btnNewGameScreen = FindViewById<Button>(Resource.Id.btnNewGameScreen);
            btnNewGameScreen.Click += btnNewGameScreen_Click;

            btnHighScores = FindViewById<Button>(Resource.Id.btnHighScores);
            btnHighScores.Click += btnHighScores_Click;

            Button btnEditDB = FindViewById<Button>(Resource.Id.btnEditDB);
            btnEditDB.Click += btnEditDB_Click;

            Button btnQuit = FindViewById<Button>(Resource.Id.btnQuit);
            btnQuit.Click += btnQuit_Click;



        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnEditDB_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EditDB));
        }


    

      

        private void btnHighScores_Click(object sender, EventArgs e)
        {
          StartActivity(typeof(HighScores));
        }

        private void btnNewGameScreen_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(EnterPlayerName));
        }
    }
}

