using System;
using System.Collections.Generic;
using System.Runtime;

class HockeyGame
{
    private string team1Name;
    private string team2Name;
    private int team1Score;
    private int team2Score;
    private Random random;

    public HockeyGame(string team1Name, string team2Name)
    {
        this.team1Name = team1Name;
        this.team2Name = team2Name;
        this.team1Score = 0;
        this.team2Score = 0;
        this.random = new Random();
    }

    public void Play()
    {
        Console.WriteLine($"Welcome to the hockey game between {team1Name} and {team2Name}!");
        Console.WriteLine("Let's start the game!\n");

        for (int period = 1; period <= 4; period++)
        {
            Console.WriteLine($"Period {period} begins!");
            PlayPeriod();
            Console.WriteLine($"End of period {period}. Scores: {team1Name} - {team1Score}, {team2Name} - {team2Score}\n");
        }

        Console.WriteLine("Game over!");
        if (team1Score > team2Score)
        {
            Console.WriteLine($"{team1Name} wins!");
        }
        else if (team1Score < team2Score)
        {
            Console.WriteLine($"{team2Name} wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    private void PlayPeriod()
    {
        for (int i = 0; i < 4; i++) // Each team has 3 attempts
        {
            PlayAttempt(team1Name);
            PlayAttempt(team2Name);
        }
    }

    private void PlayAttempt(string teamName)
    {
        Console.WriteLine($"{teamName}'s turn:");
        int scoreChance = random.Next(2); // Random number between 0 and 1
        if (scoreChance == 1)
        {
            Console.WriteLine("Goal!");
            if (teamName == team1Name)
            {
                team1Score++;
            }
            else
            {
                team2Score++;
            }
        }
        else
        {
            Console.WriteLine("Miss.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter name of Team 1: ");
        string team1Name = Console.ReadLine();
        Console.Write("Enter name of Team 2: ");
        string team2Name = Console.ReadLine();

        HockeyGame game = new HockeyGame(team1Name, team2Name);
        game.Play();
    }
}
