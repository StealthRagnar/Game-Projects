using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class SnakeGame
{
    //basic screen starting of the game
    static int screenWidth = 100;
    static int screenHeight = 80;
    static char snakeSymbol = 'C';
    static char foodSymbol = 'O';
    static int score = 0;
    static bool isGameOver = false;

    static List<Position> snake = new List<Position>();
    static Position food = new Position();

    static Direction direction = Direction.Right;

    enum Direction //directions the snake can traverse
    {
        Up,
        Down,
        Left,
        Right
    }

    struct Position
    {
        public int X;
        public int Y;
    }

    static void Main()
    {
        Console.Title = "Snake Game";
        Console.WindowHeight = screenHeight;
        Console.WindowWidth = screenWidth;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        InitializeGame();

        while (!isGameOver)
        {
            if (Console.KeyAvailable)
                HandleKeyPress(Console.ReadKey(true).Key);

            MoveSnake();
            CheckCollision();
            DrawGame();

            Thread.Sleep(100);
        }

        Console.Clear();
        Console.WriteLine("Game Over! Your Score: " + score);
    }

    static void InitializeGame()
    {
        isGameOver = false;
        score = 0;

        // Initialize snake
        snake.Clear();
        snake.Add(new Position { X = 3, Y = 2 });
        snake.Add(new Position { X = 2, Y = 2 });
        snake.Add(new Position { X = 1, Y = 2 });

        // Initialize food
        GenerateFood();
    }

    static void GenerateFood()
    {
        Random random = new Random();
        food = new Position { X = random.Next(1, screenWidth - 1), Y = random.Next(1, screenHeight - 1) };
    }

    static void DrawGame()
    {
        Console.Clear();

        // Draw snake
        foreach (Position segment in snake)
        {
            Console.SetCursorPosition(segment.X, segment.Y);
            Console.Write(snakeSymbol);
        }

        // Draw food
        Console.SetCursorPosition(food.X, food.Y);
        Console.Write(foodSymbol);

        // Draw score
        Console.SetCursorPosition(screenWidth / 2 - 5, 0);
        Console.Write("Score: " + score);
    }

    static void MoveSnake()
    {
        Position head = snake.First();

        Position newHead = new Position();

        switch (direction)
        {
            case Direction.Up:
                newHead = new Position { X = head.X, Y = head.Y - 1 };
                break;
            case Direction.Down:
                newHead = new Position { X = head.X, Y = head.Y + 1 };
                break;
            case Direction.Left:
                newHead = new Position { X = head.X - 1, Y = head.Y };
                break;
            case Direction.Right:
                newHead = new Position { X = head.X + 1, Y = head.Y };
                break;
        }

        snake.Insert(0, newHead);

        // Check if the snake eats the food
        if (newHead.X == food.X && newHead.Y == food.Y)
        {
            score++;
            GenerateFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }

    static void CheckCollision()
    {
        // Check if the snake hits the wall
        if (snake.First().X <= 0 || snake.First().X >= screenWidth - 1 ||
            snake.First().Y <= 0 || snake.First().Y >= screenHeight - 1)
        {
            isGameOver = true;
        }

        // Check if the snake collides with itself
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake.First().X == snake[i].X && snake.First().Y == snake[i].Y)
            {
                isGameOver = true;
                break;
            }
        }
    }

    static void HandleKeyPress(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (direction != Direction.Down)
                    direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                if (direction != Direction.Up)
                    direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                if (direction != Direction.Right)
                    direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                if (direction != Direction.Left)
                    direction = Direction.Right;
                break;
        }
    }
}
