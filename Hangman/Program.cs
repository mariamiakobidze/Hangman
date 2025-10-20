// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main()
    {
        string[] words = {
            "computer", "apple", "classrom", "program",
            "sunshine", "mountain", "river", "school",
            "internet", "puzzle"
        };

        Random random = new Random();
        string word = words[random.Next(words.Length)];

        char[] hiddenWord = new char[word.Length];
        for (int i = 0; i < word.Length; i++)
            hiddenWord[i] = '_';

        List<char> guessedLetters = new List<char>();
        int triesLeft = 6;
        bool wordGuessed = false;

        Console.WriteLine("🎯 Welcome to Hangman!");
        Console.WriteLine("----------------------");
    }
}

