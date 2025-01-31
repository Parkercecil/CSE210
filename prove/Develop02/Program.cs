using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }
    public string Emotion { get; set; }  // New property for emotion

    // Constructor to initialize the entry
    public Entry(string prompt, string response, string emotion)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Emotion = emotion;
    }

    // Method to display the entry in a readable format
    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {Date} | Emotion: {Emotion}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}\n");
    }
}

public class Journal
{
    public List<Entry> Entries { get; set; }
    private static readonly string separator = "~|~";

    // Constructor to initialize the journal
    public Journal()
    {
        Entries = new List<Entry>();
    }

    // Method to add an entry to the journal
    public void AddEntry(string prompt, string response, string emotion)
    {
        Entries.Add(new Entry(prompt, response, emotion));
    }

    // Method to display all entries
    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            entry.DisplayEntry();
        }
    }

    // Method to save entries to a file
    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                writer.WriteLine($"{entry.Date}{separator}{entry.Prompt}{separator}{entry.Response}{separator}{entry.Emotion}");
            }
        }
        Console.WriteLine($"Journal saved to {filename}");
    }

    // Method to load entries from a file
    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            Entries.Clear();
            foreach (var line in File.ReadLines(filename))
            {
                var parts = line.Split(new string[] { separator }, StringSplitOptions.None);
                if (parts.Length == 4)
                {
                    Entries.Add(new Entry(parts[1], parts[2], parts[3]) { Date = parts[0] });
                }
            }
            Console.WriteLine("Journal loaded from file.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    // Method to search entries by emotion
    public void SearchByEmotion(string emotion)
    {
        var filteredEntries = Entries.Where(entry => entry.Emotion.Equals(emotion, StringComparison.OrdinalIgnoreCase)).ToList();
        foreach (var entry in filteredEntries)
        {
            entry.DisplayEntry();
        }
    }

    // Method to show emotion statistics
    public void ShowEmotionStatistics()
    {
        var emotionStats = Entries
            .GroupBy(entry => entry.Emotion)
            .Select(group => new { Emotion = group.Key, Count = group.Count() })
            .ToList();

        foreach (var stat in emotionStats)
        {
            Console.WriteLine($"{stat.Emotion}: {stat.Count} entries");
        }
    }
}

public class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        string[] prompts = new string[]
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        string[] emotions = new string[] { "Happy", "Sad", "Grateful", "Angry", "Anxious", "Neutral" };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Search entries by emotion");
            Console.WriteLine("6. Show emotion statistics");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Write a new entry
                    string randomPrompt = prompts[new Random().Next(prompts.Length)];
                    Console.WriteLine($"Prompt: {randomPrompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();

                    Console.WriteLine("How did you feel today?");
                    for (int i = 0; i < emotions.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {emotions[i]}");
                    }
                    string emotionChoice = Console.ReadLine();
                    string emotion = emotionChoice switch
                    {
                        "1" => "Happy",
                        "2" => "Sad",
                        "3" => "Grateful",
                        "4" => "Angry",
                        "5" => "Anxious",
                        _ => "Neutral"
                    };

                    journal.AddEntry(randomPrompt, response, emotion);
                    break;

                case "2":
                    // Display the journal
                    journal.DisplayEntries();
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case "3":
                    // Save the journal to a file
                    Console.Write("Enter the filename to save to: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case "4":
                    // Load the journal from a file
                    Console.Write("Enter the filename to load from: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case "5":
                    // Search entries by emotion
                    Console.WriteLine("Enter emotion to filter by: ");
                    foreach (var e in emotions)
                    {
                        Console.WriteLine(e);
                    }
                    string emotionFilter = Console.ReadLine();
                    journal.SearchByEmotion(emotionFilter);
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case "6":
                    // Show emotion statistics
                    journal.ShowEmotionStatistics();
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    break;

                case "7":
                    // Quit the program
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}
