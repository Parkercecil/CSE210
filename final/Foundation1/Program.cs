using System;
using System.Collections.Generic;

// Abstract class defining common behavior
abstract class MediaContent
{
    public string Title { get; set; }
    public string Author { get; set; }

    public MediaContent(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

// Comment class
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Video class inheriting from MediaContent
class Video : MediaContent
{
    public int LengthInSeconds { get; set; }
    private List<Comment> comments;

    public Video(string title, string author, int lengthInSeconds) : base(title, author)
    {
        LengthInSeconds = lengthInSeconds;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}\nAuthor: {Author}\nLength: {LengthInSeconds} seconds\nNumber of Comments: {GetCommentCount()}\n");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
        }
        Console.WriteLine("----------------------------------");
    }
}

class Program
{
    static void Main()
    {
        // Creating videos
        Video video1 = new Video("C# OOP Tutorial", "Tech Guru", 600);
        Video video2 = new Video("Intro to Abstraction", "Code Academy", 450);
        Video video3 = new Video("YouTube Analytics 101", "Data Insights", 720);

        // Adding comments
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Can you make a video on inheritance?"));

        video2.AddComment(new Comment("Dave", "Abstraction is now clear!"));
        video2.AddComment(new Comment("Emma", "Concise and informative."));
        video2.AddComment(new Comment("Frank", "Would love more examples."));

        video3.AddComment(new Comment("Grace", "Really insightful data analysis."));
        video3.AddComment(new Comment("Henry", "How can I track my own videos?"));
        video3.AddComment(new Comment("Isabel", "Loved the breakdown!"));

        // Storing videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Displaying video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
