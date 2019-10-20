using System;

public class EdgeValueException : Exception
{
    public EdgeValueException() : base("You must set 'EdgeValue' delegate, tou use this method.")
    {
        HelpLink = "https://github.com/TheLamed/Graph";
    }
    public EdgeValueException(string message) : base("You must set 'EdgeValue' delegate, tou use this method. " + message)
    {
        HelpLink = "https://github.com/TheLamed/Graph";
    }
}