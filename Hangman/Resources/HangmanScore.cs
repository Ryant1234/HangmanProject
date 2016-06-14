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
using SQLite;

namespace Hangman.Resources
{


    [Table("HangmanScore")]
    public   class HangmanScore
    { 

        
       
        
            [PrimaryKey, AutoIncrement] //Column("_Id")]
            public int Id { get; set; }

            [MaxLength(25)]
            public string Name { get; set; }

            public int Score { get; set; }

          

        
    }


}