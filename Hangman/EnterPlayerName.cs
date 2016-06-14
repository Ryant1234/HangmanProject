using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Hangman.Assets;
using Hangman.Resources;
using Java.Security;
using DataAdapter = System.Data.Common.DataAdapter;

namespace Hangman
{
    [Activity(Label = "EnterPlayerName")]
    public class EnterPlayerName : Activity

    {
        List<HangmanScore> myList;
       
        public TextView txtEnterPlayerName;
        private Spinner PlayerNameSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectPlayer);

            ConnectionClass myConnectionClass = new ConnectionClass();
            myList = myConnectionClass.ViewAll();

         PlayerNameSpinner = FindViewById<Spinner>(Resource.Id.selectPlayerSpinner);
            Hangman.Resources.DataAdapter da = new Resources.DataAdapter(this, myList);

            PlayerNameSpinner.Adapter = da;
      
            PlayerNameSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            txtEnterPlayerName = FindViewById<TextView>(Resource.Id.txtEnterName);

            Button btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);
            btnStartGame.Click += btnStartGame_Click;


            Button btnAddPlayer = FindViewById<Button>(Resource.Id.btnAddProfile);
            btnAddPlayer.Click += btnAddPlayer_Click;

            Button btnselectplayerMainMenu = FindViewById<Button>(Resource.Id.btnselectplayerMainMenu);

            btnselectplayerMainMenu.Click += btnselectplayerMainMenu_Click;

        }

        private void btnselectplayerMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
           StartActivity(typeof(HangmanGame));
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
             // If the length of the text is more then 0.. do this..
            if (txtEnterPlayerName.Text.Length > 0)
            {
                // Set the new PlayerName to the text in the textfield
                HangmanGame.PlayerName = txtEnterPlayerName.Text.ToString();
                // Give them a score of 0 to begin with
                HangmanGame.score = 0;
                var cc = new ConnectionClass();
                // Insert the Players name and score into the database
                cc.InsertNewPlayer(HangmanGame.PlayerName, HangmanGame.score);

                // And update the list
                myList = cc.ViewAll();

            
                var da = new Resources.DataAdapter(this, myList);
                // And display the updated list on the spinner
                PlayerNameSpinner.Adapter = da;
                
            }
            // Display a message if there is an empty textfield
            else
            {


                Toast.MakeText(this, "Please enter at least 1 character for your name", ToastLength.Short).Show();
            }
        }


        


        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner) sender;
            // The Player Name and their score is collected from here 
            HangmanGame.Id = this.myList.ElementAt(e.Position).Id;
            HangmanGame.PlayerName = this.myList.ElementAt(e.Position).Name;
            HangmanGame.score = this.myList.ElementAt(e.Position).Score;
        }


     
    


        
    }
}