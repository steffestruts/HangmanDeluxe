using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HangmanDeluxe
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public string currentWord;
        public string guessWord;
        public int lives = 7;
        public StringBuilder builder = new StringBuilder();
    
        public GameWindow()
        {
            InitializeComponent();

            currentWord = SelectWord();
            CreateKeyboard();

            for (int i = 0; i < currentWord.Length; i++)
            {
                builder.Append("?");
            }

            TextBlockLives.Text = lives.ToString();
            TextBlockUnderscore.Text = builder.ToString();
        }

        string SelectWord()
        {
            Random rnd = new Random();
            // Wordbank with hard words.
            string[] hardWords = new string[] { "Assembly", "Anniversary", "Electrocardiograph", "Eschatology", "Hepatology", "Psychopannychism", "Quantimeter", "Tesseract", "Transfluent", "Vocabulary" };
            // Take a random word and make it to uppercase.
            return hardWords[rnd.Next(0, hardWords.Length)].ToUpper();
        }

        void CreateKeyboard()
        {
            for (int i = 65; i < 91; i++)
            {
                Button b = new Button();
                b.Click += new RoutedEventHandler(WordGuess);
                b.Content = ((char)i);
                b.Height = 30;
                b.Width = 30;
                WrapPanelKeyboard.Children.Add(b);
            }
        }

        void WordGuess(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            char letter = Convert.ToChar(b.Content);
            b.IsEnabled = false;
            if (currentWord.Contains(letter))
            {
                // Get index from currentWord than check with strinbuilder index.
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (currentWord[i] == letter)
                    {
                        builder[i] = currentWord[i];
                        guessWord = builder.ToString();
                        TextBlockUnderscore.Text = builder.ToString();
                    }
                }

                // Compare the secret word with fragmented word.
                if (currentWord.Contains(guessWord))
                {
                    MessageBox.Show(string.Format("Du vann! Ordet var {0}", currentWord));
                }
            }
            else
            {
                // Show wrong guessed letter.
                TextBlockWrongGuess.Text += letter.ToString();
                // Countdown lives every wrong guess and display it.
                lives--;
                TextBlockLives.Text = lives.ToString();
                // No more lives left, display the word inside messagebox.
                if (lives == 0)
                {
                    MessageBox.Show(string.Format("Du förlora! Ordet var {0}", currentWord));
                }
            }
        }
    }
}
