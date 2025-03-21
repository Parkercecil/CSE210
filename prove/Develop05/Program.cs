using System;
using System.Collections.Generic;
using System.IO;

// Base class for all goals
abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public abstract void RecordEvent(ref int totalPoints);
    public abstract string GetStatus();
    public abstract string SaveFormat();

    public int GetPoints() => _points;
}

// Simple goal that can be completed once
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent(ref int totalPoints)
    {
        if (!_isComplete)
        {
            _isComplete = true;
            totalPoints += _points;
            Console.WriteLine($"Goal '{_name}' completed! You gained {_points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{_name}' is already completed.");
        }
    }

    public override string GetStatus() => _isComplete ? "[✓]" : "[ ]";

    public override string SaveFormat() => $"Simple,{_name},{_description},{_points},{_isComplete}";
}

// Eternal goal that can be completed multiple times
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override void RecordEvent(ref int totalPoints)
    {
        totalPoints += _points;
        Console.WriteLine($"Goal '{_name}' recorded! You gained {_points} points.");
    }

    public override string GetStatus() => "[∞]";

    public override string SaveFormat() => $"Eternal,{_name},{_description},{_points}";
}

// Checklist goal that requires multiple completions
class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonus = bonus;
    }

    public override void RecordEvent(ref int totalPoints)
    {
        _currentCount++;
        totalPoints += _points;

        if (_currentCount >= _targetCount)
        {
            _isComplete = true;
            totalPoints += _bonus;
            Console.WriteLine($"Goal '{_name}' completed! You gained {_points} points plus a {_bonus} bonus!");
        }
        else
        {
            Console.WriteLine($"Recorded progress for '{_name}'. {_currentCount}/{_targetCount} completed. Gained {_points} points.");
        }
    }

    public override string GetStatus() => _isComplete ? "[X]" : $"[{_currentCount}/{_targetCount}]";

    public override string SaveFormat() => $"Checklist,{_name},{_description},{_points},{_targetCount},{_currentCount},{_bonus}";
}

// Goal tracker class
class GoalTracker
{
    private List<Goal> _goals = new List<Goal>();
    private int _totalScore = 0;

    public void CreateGoal()
    {
        Console.WriteLine("Choose a goal type:\n1. Simple\n2. Eternal\n3. Checklist");
        int choice = int.Parse(Console.ReadLine());

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter description: ");
        string desc = Console.ReadLine();
        Console.Write("Enter points: ");
        int points = int.Parse(Console.ReadLine());

        if (choice == 1) _goals.Add(new SimpleGoal(name, desc, points));
        else if (choice == 2) _goals.Add(new EternalGoal(name, desc, points));
        else if (choice == 3)
        {
            Console.Write("Enter target count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Select a goal to record:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()} {_goals[i].SaveFormat()}");
        }
        int choice = int.Parse(Console.ReadLine()) - 1;
        _goals[choice].RecordEvent(ref _totalScore);
    }

    public void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"{goal.GetStatus()} {goal.SaveFormat()}");
        }
        Console.WriteLine($"Current Points: {_totalScore} pts\n");
    }

    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(_totalScore);
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.SaveFormat());
            }
        }
        Console.WriteLine("Goals successfully saved.");
    }

    public void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            _goals.Clear();
            string[] lines = File.ReadAllLines("goals.txt");
            _totalScore = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == "Simple")
                    _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])));
                else if (parts[0] == "Eternal")
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                else if (parts[0] == "Checklist")
                    _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[6])));
            }
            Console.WriteLine("Goals successfully loaded.");
        }
        else Console.WriteLine("No saved goals found.");
    }

    public int GetTotalScore() => _totalScore;
}

// Main Program
class Program
{
    static void Main()
    {
        GoalTracker tracker = new GoalTracker();
        while (true)
        {
            Console.WriteLine("\nWelcome to The Goals Menu:\n1. Create a New Goal\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Event\n6. Quit");
            Console.WriteLine($"Current Points: {tracker.GetTotalScore()} pts");
            Console.Write("Select a choice from the Menu: ");

            int choice = int.Parse(Console.ReadLine());
            if (choice == 1) tracker.CreateGoal();
            else if (choice == 2) tracker.DisplayGoals();
            else if (choice == 3) tracker.SaveGoals();
            else if (choice == 4) tracker.LoadGoals();
            else if (choice == 5) tracker.RecordEvent();
            else if (choice == 6) break;
        }
    }
}
