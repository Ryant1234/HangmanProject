using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Drm;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Hangman.Resources;
using Java.Security;
using Stream = System.IO.Stream;

namespace Hangman.Assets
{
    [Activity(Label = "HangmanGame")]
    public class HangmanGame : Activity
   
    {
     

        //Buttons A-Z
        private Button btnA;
        private Button btnB;
        private Button btnC;
        private Button btnD;
        private Button btnE;
        private Button btnF;
        private Button btnG;
        private Button btnH;
        private Button btnI;
        private Button btnJ;
        private Button btnK;
        private Button btnL;
        private Button btnM;
        private Button btnN;
        private Button btnO;
        private Button btnP;
        private Button btnQ;
        private Button btnR;
        private Button btnS;
        private Button btnT;
        private Button btnU;
        private Button btnV;
        private Button btnW;
        private Button btnX;
        private Button btnY;
        private Button btnZ;

        
     
        private TextView txtWordToGuess;
        private Button btngameMainMenu;
        private Button btnNewGame;
        private ImageView imgHangman;
        private TextView txtCurrentScore;
        private TextView txtGuessesLeft;

        public static int Id;
        public static string PlayerName;
        public static int score;
        private string letter;
        private string rand;

        private int GuessesLeft = 8;

        private char[] wordToGuess;
        private char[] HiddenWord;

        private bool GuessedCorrect;

        private List<string> wordList = new List<string>();

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.GameScreenLayout);
            LoadDictionaryWords();
         

            btnNewGame = FindViewById<Button>(Resource.Id.btnNewGame);
            txtWordToGuess = FindViewById<TextView>(Resource.Id.txtWordToGuess);
            btngameMainMenu = FindViewById<Button>(Resource.Id.gamebtnMainMenu);
            btngameMainMenu.Click += btngameMainMenu_Click;
            btnNewGame.Click += btnNewGame_Click;
            txtCurrentScore = FindViewById<TextView>(Resource.Id.txtCurrentScore);
            txtCurrentScore.Text = score.ToString();
            txtGuessesLeft = FindViewById<TextView>(Resource.Id.txtGuessesLeft);
            txtGuessesLeft.Text = GuessesLeft.ToString();


            btnA = FindViewById<Button>(Resource.Id.btnA);
            btnB = FindViewById<Button>(Resource.Id.btnB);
            btnC = FindViewById<Button>(Resource.Id.btnC);
            btnD = FindViewById<Button>(Resource.Id.btnD);
            btnE = FindViewById<Button>(Resource.Id.btnE);
            btnF = FindViewById<Button>(Resource.Id.btnF);
            btnG = FindViewById<Button>(Resource.Id.btnG);
            btnH = FindViewById<Button>(Resource.Id.btnH);
            btnI = FindViewById<Button>(Resource.Id.btnI);
            btnJ = FindViewById<Button>(Resource.Id.btnJ);
            btnK = FindViewById<Button>(Resource.Id.btnK);
            btnL = FindViewById<Button>(Resource.Id.btnL);
            btnM = FindViewById<Button>(Resource.Id.btnM);
            btnN = FindViewById<Button>(Resource.Id.btnN);
            btnO = FindViewById<Button>(Resource.Id.btnO);
            btnP= FindViewById<Button>(Resource.Id.btnP);
            btnQ = FindViewById<Button>(Resource.Id.btnQ);
            btnR = FindViewById<Button>(Resource.Id.btnR);
            btnS = FindViewById<Button>(Resource.Id.btnS);
            btnT = FindViewById<Button>(Resource.Id.btnT);
            btnU= FindViewById<Button>(Resource.Id.btnU);
            btnV = FindViewById<Button>(Resource.Id.btnV);
            btnW = FindViewById<Button>(Resource.Id.btnW);
            btnX = FindViewById<Button>(Resource.Id.btnX);
            btnY = FindViewById<Button>(Resource.Id.btnY);
            btnZ = FindViewById<Button>(Resource.Id.btnZ);
            //Disable all the buttons now so nobody can go on a click frenzy
            DisableButtons();
            imgHangman = FindViewById<ImageView>(Resource.Id.imgHangman);
            DefaultImage();

            // Tie all of the "Letter" button clicks to 1 event.
            btnA.Click += Letter_Click;
            btnB.Click += Letter_Click;
            btnC.Click += Letter_Click;
            btnD.Click += Letter_Click;
            btnE.Click += Letter_Click;
            btnF.Click += Letter_Click;
            btnG.Click += Letter_Click;
            btnH.Click += Letter_Click;
            btnI.Click += Letter_Click;
            btnJ.Click += Letter_Click;
            btnK.Click += Letter_Click;
            btnL.Click += Letter_Click;
            btnM.Click += Letter_Click;
            btnN.Click += Letter_Click;
            btnO.Click += Letter_Click;
            btnP.Click += Letter_Click;
            btnQ.Click += Letter_Click;
            btnR.Click += Letter_Click;
            btnS.Click += Letter_Click;
            btnT.Click += Letter_Click;
            btnU.Click += Letter_Click;
            btnV.Click += Letter_Click;
            btnW.Click += Letter_Click;
            btnX.Click += Letter_Click;
            btnY.Click += Letter_Click;
            btnZ.Click += Letter_Click;

        }

      
        private void btngameMainMenu_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
        }

       
        private void Letter_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(200);
            // the variable clickbutton references the button  that was clicked
            var clickedbutton = (Button) sender;
            // Disable the button that was clicked
            clickedbutton.Enabled = false;
            // the variable "letter" contains the text of the button that was clicked
             letter = clickedbutton.Text;
            //Change that letter to upper case
            letter = letter.ToUpper();
            // go through the array of the hidden word and see if we can find the letter
            for (int i = 0; i < HiddenWord.Length; i++)
            {
                // if the "letter" of the button clicked matches a letter of the word we are trying to guess
                if (letter == wordToGuess[i].ToString() )
                {
                   // // The position of the letter(i) in the word that is hidden(with underscores) is set.
                    HiddenWord[i] = letter.ToCharArray()[0];
                    txtWordToGuess.Text = string.Join(" ", HiddenWord);

                  
                    // Run the "LetterScore" method. Add to the score based upon the letter guessed
                    LetterScore();
                    ScoreUpdate();
                    // The condition "GuessedCorrect" is set to true
                    GuessedCorrect = true;

                }

               
               

            }
            // If the GuessedCorrect condition is false, reduce the "GuessesLeft" by 1
            if (GuessedCorrect == false)
            {
                GuessesLeft = GuessesLeft - 1;
           
                GuessFailed();
                GuessedWrongTextUpdate();
                ScoreUpdate();
            }
            else
            { // Set GuessedCorrect back to False for the next round
                GuessedCorrect = false;
            }

          // If the hidden word does not have underscores left(meaning it is a complete word), the game has been won.
            if (!HiddenWord.Contains('_') )

            {
                GameWon();
            }

        }
 
        private void btnNewGame_Click(object sender, EventArgs e)
        {// Loads a new word, disable the NewGame button and set the default image


            btnNewGame.Enabled = false;
            LoadNewRandomWord();
            btnNewGame.Enabled = false;
            DefaultImage();
        }

      
        private void LoadNewRandomWord()
        {// Enable the A-Z buttons, set the "guesses left" to 8 and choose a random word from the wordlist and set it to uppercase and then convert that word to an array
            ButtonEnable();
            GuessesLeft = 8;
            Random randomGen = new Random();
          rand = wordList[randomGen.Next(wordList.Count)];
           rand = rand.ToUpper();
            wordToGuess = rand.ToArray();
       
 

            //  The Hiddenword char array is set to the length of the Word to Guess 
            HiddenWord = new char[wordToGuess.Length];

            // For every letter of the word, set that letter to _ 
             for (int i = 0; i < HiddenWord.Length; i++)
             {
                 HiddenWord[i] = '_';
                // And display it on the textWordToGuess, seperating the letters with a space
                 txtWordToGuess.Text = string.Join(" ", HiddenWord);
             }
              
        }


        private void LoadDictionaryWords()
        {

         // Open the textfile
            Stream myStream = Assets.Open("HangmanDic.txt");
            using (StreamReader sr = new StreamReader(myStream))
            {
                
                string line;
                // while the line that is being read is not equal to null (meaning there is still text to be read)
                while ((line = sr.ReadLine()) != null)
                { // Add that line to the wordlist
                    wordList.Add(line);
                }
            }
        }



        // This switch statement is based upon how many guesses the player has left
        //From 7 through to 0. Each case statement displays a different picture and runs the "GuessedWrongText" method, which just displays  a text.
        private void GuessFailed()
        {
            switch (GuessesLeft)
            {
                case 7:
                   
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed1);
              
                    break;
                case 6:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed2);
                 
                    break;
                case 5:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed3);
                    break;

                case 4:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed4);

                    break;

                case 3:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed5);

                    break;

                case 2:
                   imgHangman.SetImageResource(Resource.Drawable.GuessFailed6);

                    break;

                case 1:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed7);

                    break;

                    // Case 0(0 turns left), the player has lost the game.  
                case 0:
                    imgHangman.SetImageResource(Resource.Drawable.GuessFailed8);
                
                    // For losing the game, the player incurs a 12 point penalty to their score. If it puts their score below 0, it will be set to 0
                    score = score - 12;
                        if(score < 0)
                        {
                            score = 0;
                        }
                    System.Threading.Thread.Sleep(200);
                    Toast.MakeText(this, "You have run out of guesses! You LOSE. Your Score was " + score, ToastLength.Short).Show();
                    var cc = new ConnectionClass();
                    cc.UpdateScore(Id, PlayerName, score);
                    System.Threading.Thread.Sleep(500);


                    StartActivity(typeof(MainActivity));
                    break;
            
            }
        }

        // The scoring System
        private void LetterScore()
        {

        
            // If any of these letters are correct, their score is increased by 4
            switch (letter)
            {
                case "A":
                case "E":
                case "I":
                case "O":
                case "U":
                case "L":
                case "N":
                case "R":
                case "S":
                case "T":
                    score = score + 4;
                  // These letters increase the score by 5.. and so on
                    break;
                case "D":
                case "G":                
                case "B":
                case "C":
                case "M":
                case "P":
                    score = score + 5;
                
                    break;
                case "F":
                    case "H":
                case "W":
                 case "Y":
                case "V":
                    score = score +6;
                 
                    break;
                case "K":                   
                case "J":
                case "X":
                    score = score +8;
                    
                    break;
                case "Q":
                case "Z":
                    score = score + 10;
                  
                    break;
            }

            

        }


        // This method enables all the buttons, it is used once a game has been won. 
        private void ButtonEnable()
        {
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnI.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            btnM.Enabled = true;
            btnN.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            btnQ.Enabled = true;
            btnR.Enabled = true;
            btnS.Enabled = true;
            btnT.Enabled = true;
            btnU.Enabled = true;
            btnV.Enabled = true;
            btnW.Enabled = true;
            btnX.Enabled = true;
            btnY.Enabled = true;
            btnZ.Enabled = true;
            btnNewGame.Enabled = true;
        }
             // This disables the "letter" buttons so you can no longer click them.
        private void DisableButtons()
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnI.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            btnM.Enabled = false;
            btnN.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            btnQ.Enabled = false;
            btnR.Enabled = false;
            btnS.Enabled = false;
            btnT.Enabled = false;
            btnU.Enabled = false;
            btnV.Enabled = false;
            btnW.Enabled = false;
            btnX.Enabled = false;
            btnY.Enabled = false;
            btnZ.Enabled = false;
         
        }
       

        private void GameWon()
        { 
         
            // Set the image to  default
            DefaultImage();
            // Display the text
            Toast.MakeText(this, "You guessed the word correctly", ToastLength.Short).
            Show();
            var cc = new ConnectionClass();
            cc.UpdateScore(Id, PlayerName, score);
           // And load a new word
            LoadNewRandomWord();

        }


        // This method is called when the player has guessed the wrong letter and it tells them how many guesses left they have
        private void GuessedWrongTextUpdate()
        {
            txtGuessesLeft.Text = GuessesLeft.ToString();
        }


        
        private void ScoreUpdate()
        {
            txtCurrentScore.Text = score.ToString();
        }
      

        private void DefaultImage()
        {

            imgHangman.SetImageResource(Resource.Drawable.blankscreen);
        }


    }
        }
      
                 
          



       
        


        
    

