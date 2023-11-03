using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using HangmanBlazor.Data;



namespace HangmanBlazor.Models
{
    public class GameBoard
    {

        /*
        - A string to hold the secret random word.
        - An integer to keep track of the number of incorrect guesses.
        - Or try to integrate the guesses on the index page?? - A list of characters that stores the guesses made by the user.
       */

        private string randomword;

        public string Randomword
        {
            get { return randomword; }
            set { randomword = value; }
        }

        private string checkedWord;

        public string CheckedWord
        {
            get { return checkedWord; }
            set { checkedWord = value; }
        }

        public int guessQty;

        public int GuessQty
        {
            get { return guessQty; }
            set { guessQty = value; }
        }

        public string winStatus;


        public List<char> guesses = new List<char>{' '};
    

        public GameBoard()
        {
            Reset();
        }

        public void Reset() 
        {
            Randomword = GenerateSecretWord();
            GuessQty = 0;
            guesses = new List<char> {' '};
            WordCheck(Randomword, guesses);
            winStatus = "";

        }


        // A method to generate a secret word (you can use a hardcoded list of words or refer to an external source). 

         public string GenerateSecretWord()
            {

                Random random = new Random();
                int index = random.Next(Words.words.Count);
                
                return Words.words[index];
            }

        // A method to display the masked word with underscores for unguessed letters and showing correctly guessed letters.
        
        public void WordCheck(string randomword, List<char> guessedChars)
        {
            
            char[] charArray = guessedChars.ToArray();
            char[] returnValue = new char[randomword.Length];

            for(int i = 0; i < randomword.Length; i++)
            {
                for(int j = 0; j < charArray.Length; j++)
                {
                    if(randomword[i] == charArray[j])
                    {
                        returnValue[i] = charArray[j];
                    }                    
                    if(randomword[i] != returnValue[i])
                    {
                        returnValue[i] = '_';
                    }
                    if(randomword[i] == ' ')
                    {
                        returnValue[i] = '-';
                    }


                }

                
            }


            string output = "";

            foreach(char letter in returnValue)
            {
                output += letter;
                output += ' ';
                
            }

            CheckedWord = output;
        }

        // A method to check if the user's guess is correct or incorrect.




        public void AddChar(char x)
        {
            guesses.Add(x);
            WordCheck(Randomword, guesses);

            int count = 0;

            foreach(char y in Randomword)
            {
                if(x == y)
                {
                    count ++;
                }
            }

            if(count == 0)
            {
                GuessQty++;
            }

            if(GuessQty > 5)
            {
                winStatus = "You Lose!";
            }

            string output = "";
            foreach(char z in Randomword)
            {
             output += z;
             output += ' ';   
            }

            if(guessQty < 6 && CheckedWord == output)
            {
                winStatus = "You Win!";
            }

        }



    }
}
