using System;
using System.Threading;

namespace ActivityProgram
{
    public abstract class Activity
    {
        protected string _activityName;
        protected string _description;
        protected int _duration;

        public Activity(string activityName, string description)
        {
            _activityName = activityName;
            _description = description;
        }

        public void StartActivity()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_activityName}!");
            Console.WriteLine(_description);
            Console.Write("Please enter the duration of the activity in seconds: ");
            _duration = int.Parse(Console.ReadLine());

            Console.WriteLine("\nGet ready...");
            DisplayPauseAnimation(3);
        }

        public void EndActivity()
        {
            Console.WriteLine("\nGood job! You've completed the activity.");
            Console.WriteLine($"You spent {_duration} seconds.");
            DisplayPauseAnimation(3);
        }

        // Spinner animation during pauses
        protected void DisplayPauseAnimation(int seconds)
        {
            string[] spinner = new string[] { "|", "/", "-", "\\" };
            int spinnerIndex = 0;

            for (int i = 0; i < seconds; i++)
            {
                Console.Write($"[{spinner[spinnerIndex]}] ");
                spinnerIndex = (spinnerIndex + 1) % spinner.Length;
                Thread.Sleep(1000); // Delay 1 second
                Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop); // Move cursor back
            }
            Console.WriteLine();
        }

        public abstract void PerformActivity();
    }

    public class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        public override void PerformActivity()
        {
            StartActivity();

            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                Console.WriteLine("\nBreathe in...");
                DisplayPauseAnimation(4);

                Console.WriteLine("\nBreathe out...");
                DisplayPauseAnimation(4);
            }

            EndActivity();
        }
    }

    public class ReflectionActivity : Activity
    {
        private readonly string[] _prompts = new string[]
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly string[] _questions = new string[]
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        public override void PerformActivity()
        {
            StartActivity();

            Random random = new Random();
            string selectedPrompt = _prompts[random.Next(_prompts.Length)];
            Console.WriteLine("\n" + selectedPrompt);
            DisplayPauseAnimation(3);

            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                string selectedQuestion = _questions[random.Next(_questions.Length)];
                Console.WriteLine("\n" + selectedQuestion);
                DisplayPauseAnimation(4);
            }

            EndActivity();
        }
    }

    public class ListingActivity : Activity
    {
        private readonly string[] _prompts = new string[]
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        public override void PerformActivity()
        {
            StartActivity();

            Random random = new Random();
            string selectedPrompt = _prompts[random.Next(_prompts.Length)];
            Console.WriteLine("\n" + selectedPrompt);
            DisplayPauseAnimation(3);

            Console.WriteLine("\nStart listing your items now:");
            int itemCount = 0;
            DateTime endTime = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                itemCount++;
            }

            Console.WriteLine($"\nYou listed {itemCount} items.");
            EndActivity();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO THE MINDFULNESS PROGRAM!");
                Console.WriteLine("Please select an activity!:");
                Console.WriteLine("__________________________");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine();
                Activity selectedActivity = null;

                switch (choice)
                {
                    case "1":
                        selectedActivity = new BreathingActivity();
                        break;
                    case "2":
                        selectedActivity = new ReflectionActivity();
                        break;
                    case "3":
                        selectedActivity = new ListingActivity();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        continue;
                }

                selectedActivity.PerformActivity();
            }
        }
    }
}
