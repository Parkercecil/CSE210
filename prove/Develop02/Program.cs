using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    // Constructor to initialize the entry
    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd"); // Store the current date as a string
    }

    // Method to display the entry in a readable format
    public void DisplayEntry()
    {
        Console.WriteLine($"Date: {Date}");
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
    public void AddEntry(string prompt, string response)
    {
        Entries.Add(new Entry(prompt, response));
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
                writer.WriteLine($"{entry.Date}{separator}{entry.Prompt}{separator}{entry.Response}");
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
                if (parts.Length == 3)
                {
                    Entries.Add(new Entry(parts[1], parts[2]) { Date = parts[0] });
                }
            }
            Console.WriteLine("Journal loaded from file.");
        }
        else
        {
            Console.WriteLine("File not found.");
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

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Quit");
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
                    journal.AddEntry(randomPrompt, response);
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
                    // Quit the program
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}
