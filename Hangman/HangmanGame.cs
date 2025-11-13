

namespace Hangman
{
    abstract class Game     {
        public abstract void Start();
    }
    class HangmanGame: Game
    {

        public override void Start()
        {
            string filePath = GetFilePath("Data", "words.txt");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("⚠️ File not found! Make sure 'words.txt' exists in the Data folder.");
                return;
            }

            string[] words = File.ReadAllLines(filePath);

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

            while (triesLeft > 0 && !wordGuessed)
            {
                DrawHangman(triesLeft);
                ShowRules();
                Console.WriteLine("\nWord: " + string.Join(" ", hiddenWord));
                Console.WriteLine("Guessed letters: " + string.Join(" ", guessedLetters));
                Console.WriteLine("Tries left: " + triesLeft);

                Console.Write("Guess a letter: ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("⚠️ Please enter a single valid letter!");
                    Thread.Sleep(1000);
                    continue;
                }

                char guess = input[0];

                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("⚠️ You already guessed that letter!");
                    Thread.Sleep(1000);
                    continue;
                }

                guessedLetters.Add(guess);

                if (word.Contains(guess))
                {
                    Console.WriteLine("✅ Good guess!");

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == guess)
                            hiddenWord[i] = guess;
                    }

                    wordGuessed = !Array.Exists(hiddenWord, c => c == '_');
                }
                else
                {
                    Console.WriteLine("❌ Wrong guess!");
                    triesLeft--;
                }

                Thread.Sleep(1000);
            }

            DrawHangman(triesLeft);

            Console.WriteLine("\nWord: " + string.Join(" ", hiddenWord));
            if (wordGuessed)
                Console.WriteLine($"\n🎉 You won! The word was: {word}");
            else
                Console.WriteLine($"\n💀 Game Over! The word was: {word}");
        }

        private static string GetFilePath(string folderName, string fileName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName ?? string.Empty;

            string filePath = Path.Combine(projectDirectory, folderName, fileName);
            return filePath;
        }

        private void DrawHangman(int triesLeft)
        {
            string[] stages =
            {
@"
  +---+
  |   |
      |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
      |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========",
@"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
========="
        };

            int stageIndex = 6 - triesLeft;
            if (stageIndex < 0) stageIndex = 0;
            if (stageIndex > 6) stageIndex = 6;

            Console.Clear();
            Console.WriteLine(stages[stageIndex]);
            GetFilePath("Data", "words.txt");

        }
        public void ShowRules()
        {
            Console.WriteLine("Guess letters to find the word!");

        }
    }
}
