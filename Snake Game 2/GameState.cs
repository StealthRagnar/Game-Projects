using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game_2
{
    public class GameState
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool Gameover { get; private set; }

        private readonly LinkedList<Position> snakepositions = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = Rows / 2;
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakepositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c <= Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
            {
                return;
            }

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;
        }

        public Position Headposition()
        {
            return snakepositions.First.Value;

        }

        public Position Tailposition()
        {
            return snakepositions.Last.Value;

        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakepositions;
        }

        private void Addhead(Position pos)
        {
            snakepositions.AddFirst(pos);
            Grid[pos.Row, pos.Column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakepositions.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakepositions.RemoveLast();
        }

        public void ChangeDirection(Direction dir)
        {
            Dir = dir;
        }

        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns;
        }

        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            if (newHeadPos == Tailposition())
            {
                return GridValue.Empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Column];
        }

        public void Move()
        {
            Position newHeadpos = Headposition().Translate(Dir);
            GridValue hit = WillHit(newHeadpos);

            if(hit == GridValue.Outside || hit == GridValue.Snake)
            {
                Gameover = true;
            }

            else if (hit == GridValue.Empty ) {
                RemoveTail();
                Addhead(newHeadpos);
            
            
            }

            else if (hit == GridValue.Food)
            {
                Addhead(newHeadpos);
                Score++;
                AddFood();
            }
           
        }

    

    }
}
