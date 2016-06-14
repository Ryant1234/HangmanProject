using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Hangman.Resources;
using Mono.Data.Sqlite;
using Environment = Android.OS.Environment;


namespace Hangman
{
   public  class ConnectionClass
    {

       private string dbpath { get; set; }

      private SQLiteConnection db { get; set; }

      



           public ConnectionClass()
       {

           string dbPath =
               Path.Combine(System.Environment.GetFolderPath
                   (System.Environment.SpecialFolder.Personal), "HangmanDB.sqlite");

           db = new SQLiteConnection(dbPath);

            db.CreateTable<HangmanScore>();
       }




        public List<HangmanScore> ViewAll()
        {
            try
            {
               ;
                return db.Query<HangmanScore>("select *  from HangmanScore  ORDER BY Score DESC");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }
        }







        public string UpdateScore(int id, string name, int score)
        {

         
              try
                {
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                        "HangmanDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                    var item = new HangmanScore();

                    item.Id = id;
                    item.Name = name;
                    item.Score = score;
                  
                    db.Update(item);
                    return "Record Updated...";
                }
                catch (Exception ex)
                {
                    return "Error : " + ex.Message;
                }
            
        }



        public string InsertNewPlayer(string name, int score)
        {


            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    "HangmanDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new HangmanScore();
                item.Name = name;
                item.Score = score;

                db.Insert(item);
                return "You have been added to the database";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }

        }


       public string DeletePlayer(int id)
       {

            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    "HangmanDB.sqlite");
                var db = new SQLiteConnection(dbPath);
                var item = new HangmanScore();
                item.Id = id;
              

                db.Delete(item);
                return "You have been added to the database";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }


        }

   }
    }
