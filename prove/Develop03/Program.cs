using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (true)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("\nPress Enter to hide some words or type 'quit' to exit.");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            if (scripture.HideRandomWords())
            {
                Console.Clear();
                scripture.Display();
            }
            else
            {
                Console.WriteLine("All words are hidden. Program will exit.");
                break;
            }
        }
    }
}

public class Scripture
{
    private ScriptureReference reference;
    private List<Word> words;

    public Scripture(string referenceText, string scriptureText)
    {
        reference = new ScriptureReference(referenceText);
        words = scriptureText.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"Scripture Reference: {reference.GetReference()}");
        Console.WriteLine("Scripture Text:");
        foreach (var word in words)
        {
            word.Display();
        }
    }

    public bool HideRandomWords()
    {
        // Check if all words are hidden
        if (words.All(w => w.IsHidden))
            return false;

        // Select a random word to hide that is not yet hidden
        Random rand = new Random();
        var nonHiddenWords = words.Where(w => !w.IsHidden).ToList();
        if (nonHiddenWords.Count == 0)
            return false;

        var wordToHide = nonHiddenWords[rand.Next(nonHiddenWords.Count)];
        wordToHide.Hide();
        return true;
    }
}

public class ScriptureReference
{
    public string ReferenceText { get; private set; }

    public ScriptureReference(string referenceText)
    {
        ReferenceText = referenceText;
    }

    public string GetReference()
    {
        return ReferenceText;
    }
}

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void Display()
    {
        if (IsHidden)
            Console.Write("_____ ");
        else
            Console.Write(Text + " ");
    }
}
