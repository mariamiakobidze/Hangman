
namespace Hangman
{
    abstract class Game    
    {
        public abstract void Start();
    }

    class HangmanGame: Game
    {

        public override void Start()
        {
            string filePath = GetFilePath("Data", "words.txt");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            string[] words = File.ReadAllLines(filePath);

            Random random = new Random();
            string word = words[random.Next(words.Length)];

            char[] hiddenWord = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord[i] = '_';
            }

            List<char> guessedLetters = new List<char>();
            int triesLeft = 6;
            bool wordGuessed = false;

            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("----------------------");
            ShowRules();
            Console.WriteLine("Press enter to start:");
            Console.ReadKey();

            while (triesLeft > 0 && !wordGuessed)
            {
                DrawHangman(triesLeft);
                Console.WriteLine("\nWord: " + string.Join(" ", hiddenWord));
                Console.WriteLine("Guessed letters: " + string.Join(" ", guessedLetters));
                Console.WriteLine("Tries left: " + triesLeft);

                Console.Write("Guess a letter: ");
                string input = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("please enter a single valid letter!");
                    Thread.Sleep(1000);
                    continue;
                }

                char guess = input[0];

                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("you already guessed that letter!");
                    Thread.Sleep(1000);
                    continue;
                }

                guessedLetters.Add(guess);

                if (word.Contains(guess))
                {
                    Console.WriteLine("good guess!");

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == guess)
                        {
                            hiddenWord[i] = guess;
                        }
                    }

                    wordGuessed = !Array.Exists(hiddenWord, c => c == '_');
                }
                else
                {
                    Console.WriteLine("wrong guess!");
                    triesLeft--;
                }

                Thread.Sleep(1000);
            }

            DrawHangman(triesLeft);

            Console.WriteLine("\nWord: " + string.Join(" ", hiddenWord));
            if (wordGuessed)
                Console.WriteLine($"\n You won! The word was: {word}");
            else
                Console.WriteLine($"\n Game Over! The word was: {word}");
        }

        private static string GetFilePath(string folderName, string fileName)
        {
            string workingDirector = Environment.CurrentDirectory;
            string projectDirector = Directory.GetParent(workingDirector)?.Parent?.Parent?.FullName ?? string.Empty;

            string filePath = Path.Combine(projectDirector, folderName, fileName);
            return filePath;
        }

        private void DrawHangman(int triesLeft)
        {
            string[] steps =
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

            int stepIndex = 6 - triesLeft;
            if (stepIndex < 0) stepIndex = 0;
            if (stepIndex > 6) stepIndex = 6;

            Console.Clear();
            Console.WriteLine(steps[stepIndex]);
            GetFilePath("Data", "words.txt");

        }
        public void ShowRules()
        {
            Console.WriteLine("Guess letters to find the word!");
            Console.WriteLine("You have 6 tries before the hangman is complete.");

        }
    }
}
