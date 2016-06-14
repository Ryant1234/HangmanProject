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

namespace Hangman
{
    [Activity(Label = "EditDB")]
    public class EditDB : Activity
    {
        List<HangmanScore> myList;
        private Button btnDeleteEntry;
        private Spinner spinnerEditDB;
        private Button btnEditDBMainMenu;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditDB);
            ConnectionClass myConnectionClass = new ConnectionClass();
            myList = myConnectionClass.ViewAll();

            btnEditDBMainMenu = FindViewById<Button>(Resource.Id.btnEditDBMainMenu);
            btnEditDBMainMenu.Click += btnEditDBMainMenu_Click;
            spinnerEditDB = FindViewById<Spinner>(Resource.Id.spinnereditDB);
            Hangman.Resources.DataAdapter da = new Resources.DataAdapter(this, myList);

            spinnerEditDB.Adapter = da;
           
            spinnerEditDB.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerEditDB_ItemSelected);

            btnDeleteEntry = FindViewById<Button>(Resource.Id.btnDeleteEntry);
            btnDeleteEntry.Click += btnDeleteEntry_Click;
            btnDeleteEntry.Enabled = false;


        }

        private void btnEditDBMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            var cc = new ConnectionClass();
            cc.DeletePlayer(HangmanGame.Id);
            myList = cc.ViewAll();


            var da = new Resources.DataAdapter(this, myList);

            spinnerEditDB.Adapter = da;
        }

        private void spinnerEditDB_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {


            Spinner spinner = (Spinner) sender;
            HangmanGame.Id = this.myList.ElementAt(e.Position).Id;
            btnDeleteEntry.Enabled = true; 

        }


      
    }
}