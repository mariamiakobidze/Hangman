// ფაილიდან წამოიღოს 
// Data ფოლდერში დაამატეთ words.txt ფაილი

// Game კლასი
// Start მეთოდი

class Program
{
    static void Main()
    {
        HangmanGame game = new HangmanGame();
        game.Start();
    }
}

class HangmanGame
{
    private string[] words;
    private string word;
    private char[] hiddenWord;
    private List<char> guessedLetters;
    private int triesLeft;
    private bool wordGuessed;

    public void Start()
    {
        LoadWords();
        Random random = new Random();
        word = words[random.Next(words.Length)];

        hiddenWord = new char[word.Length];
        for (int i = 0; i < word.Length; i++) hiddenWord[i] = '_';

        guessedLetters = new List<char>();
        triesLeft = 6;
        wordGuessed = false;

        Console.WriteLine("🎯 Welcome to Hangman!");
        Console.WriteLine("----------------------");

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
                Console.WriteLine("⚠️ Please enter a single valid letter!");
                continue;
            }

            char guess = input[0];

            if (guessedLetters.Contains(guess))
            {
                Console.WriteLine("⚠️ You already guessed that letter!");
                continue;
            }

            guessedLetters.Add(guess);

            if (word.Contains(guess))
            {
                Console.WriteLine("✅ Good guess!");
                for (int i = 0; i < word.Length; i++)
                    if (word[i] == guess) hiddenWord[i] = guess;

                wordGuessed = !Array.Exists(hiddenWord, c => c == '_');
            }
            else
            {
                Console.WriteLine("❌ Wrong guess!");
                triesLeft--;
            }
        }

        DrawHangman(triesLeft);
        Console.WriteLine("\nWord: " + string.Join(" ", hiddenWord));
        if (wordGuessed)
            Console.WriteLine($"\n🎉 You won! The word was: {word}");
        else
            Console.WriteLine($"\n💀 Game Over! The word was: {word}");
    }

    private void LoadWords()
    {
        string dataFile = Path.Combine("Data", "words.txt");
        if (File.Exists(dataFile))
        {
            words = File.ReadAllLines(dataFile)
                        .Select(w => w.Trim().ToLower())
                        .Where(w => !string.IsNullOrEmpty(w))
                        .ToArray();
        }
        else
        {
            words = new string[]
            {
                "computer", "apple", "classroom", "program",
                "sunshine", "mountain", "river", "school",
                "internet", "puzzle"
            };
        }
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
    }
}
