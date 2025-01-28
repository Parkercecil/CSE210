using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Journal myJournal = new Journal();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. View journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Please select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    myJournal.WriteNewEntry();
                    break;
                case "2":
                    myJournal.DisplayEntries();
                    break;
                case "3":
                    myJournal.SaveToFile();
                    break;
                case "4":
                    myJournal.LoadFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

public class Journal
{
    private List<JournalEntry> entries;
    private static List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public Journal()
    {
        entries = new List<JournalEntry>();
    }

    public void WriteNewEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.Clear();
        Console.WriteLine("Prompt: " + prompt);
        Console.WriteLine("\nYour response:");
        string response = Console.ReadLine();
        JournalEntry entry = new JournalEntry(prompt, response, DateTime.Now);
        entries.Add(entry);
        Console.WriteLine("Entry added successfully!");
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    public void DisplayEntries()
    {
        Console.Clear();
        if (entries.Count == 0)
        {
            Console.WriteLine("No journal entries found.");
        }
        else
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Date: {entry.Date.ToShortDateString()}");
                Console.WriteLine($"Prompt: {entry.Prompt}");
                Console.WriteLine($"Response: {entry.Response}");
                Console.WriteLine(new string('-', 40));
            }
        }
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    public void SaveToFile()
    {
        Console.Clear();
        Console.Write("Enter the filename to save the journal: ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    sw.WriteLine($"{entry.Date.ToString()}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine($"Journal saved to {filename} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }

    public void LoadFromFile()
    {
        Console.Clear();
        Console.Write("Enter the filename to load the journal: ");
        string filename = Console.ReadLine();

        try
        {
            entries.Clear();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    DateTime date = DateTime.Parse(parts[0]);
                    string prompt = parts[1];
                    string response = parts[2];
                    entries.Add(new JournalEntry(prompt, response, date));
                }
            }
            Console.WriteLine($"Journal loaded from {filename} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}

public class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public JournalEntry(string prompt, string response, DateTime date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }
}
