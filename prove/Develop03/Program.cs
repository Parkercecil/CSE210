using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    public string Reference { get; set; }
    public string Text { get; set; }
    private List<string> words;
    private HashSet<int> hiddenIndexes;

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Text = text;
        words = Text.Split(' ').ToList();
        hiddenIndexes = new HashSet<int>();
    }

    public void HideRandomWord()
    {
        // Hide a random word (even if it's already hidden)
        Random random = new Random();
        int index = random.Next(words.Count);
        hiddenIndexes.Add(index);
    }

    public bool IsComplete()
    {
        return hiddenIndexes.Count == words.Count;
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", words.Select((word, index) => hiddenIndexes.Contains(index) ? "_____" : word)));
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Example scripture
        var scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        // Main loop
        while (true)
        {
            scripture.Display();
            
            // Prompt user
            Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
            {
                break;
            }
            
            // Hide a random word
            scripture.HideRandomWord();

            // If all words are hidden, end the program
            if (scripture.IsComplete())
            {
                scripture.Display();
                Console.WriteLine("\nAll words are hidden. The program is ending.");
                break;
            }
        }
    }
}
