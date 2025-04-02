using System;

class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {ZipCode}";
    }
}

abstract class Event
{
    protected string Title { get; private set; }
    protected string Description { get; private set; }
    protected DateTime Date { get; private set; }
    protected string Time { get; private set; }
    protected Address Location { get; private set; }

    public Event(string title, string description, DateTime date, string time, Address location)
    {
        Title = title;
        Description = description;
        Date = date;
        Time = time;
        Location = location;
    }

    public virtual string GetStandardDetails()
    {
        return $"Event: {Title}\nDescription: {Description}\nDate: {Date:MMMM dd, yyyy}\nTime: {Time}\nLocation: {Location}";
    }

    public abstract string GetFullDetails();
    public virtual string GetShortDescription()
    {
        return $"{this.GetType().Name}: {Title} on {Date:MMMM dd, yyyy}";
    }
}

class Lecture : Event
{
    private string Speaker { get; set; }
    private int Capacity { get; set; }

    public Lecture(string title, string description, DateTime date, string time, Address location, string speaker, int capacity)
        : base(title, description, date, time, location)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {Speaker}\nCapacity: {Capacity} attendees";
    }
}

class Reception : Event
{
    private string RSVPEmail { get; set; }

    public Reception(string title, string description, DateTime date, string time, Address location, string rsvpEmail)
        : base(title, description, date, time, location)
    {
        RSVPEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP at: {RSVPEmail}";
    }
}

class OutdoorGathering : Event
{
    private string Weather { get; set; }

    public OutdoorGathering(string title, string description, DateTime date, string time, Address location, string weather)
        : base(title, description, date, time, location)
    {
        Weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {Weather}";
    }
}

class Program
{
    static void Main()
    {
        Address address1 = new Address("123 Main St", "New York", "NY", "10001");
        Address address2 = new Address("456 Elm St", "Los Angeles", "CA", "90012");
        Address address3 = new Address("789 Oak St", "Chicago", "IL", "60610");

        Event lecture = new Lecture("Tech Innovations", "A talk on the future of technology.", DateTime.Now.AddDays(5), "6:00 PM", address1, "Dr. Jane Smith", 150);
        Event reception = new Reception("Networking Night", "An evening of networking and cocktails.", DateTime.Now.AddDays(10), "7:30 PM", address2, "rsvp@eventplanner.com");
        Event outdoor = new OutdoorGathering("Summer Festival", "Music and fun in the sun!", DateTime.Now.AddDays(15), "2:00 PM", address3, "Sunny with mild winds");

        Event[] events = { lecture, reception, outdoor };

        foreach (var e in events)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine(e.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(e.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(e.GetShortDescription());
            Console.WriteLine("------------------------------\n");
        }
    }
}