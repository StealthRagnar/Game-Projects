using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Snake_Game_2
{
    public partial class MainWindow : Window
    {

        private readonly Dictionary<GridValue, ImageSource> gridValtoImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.Body },
            { GridValue.Food, Images.Food }
        };

        private readonly Dictionary<Direction, int> dirToRotation = new()
        {
            { Direction.Up, 0 },
            { Direction.Right, 90},
            { Direction.Down, 180 },
            { Direction.Left, 270 }
        };


        private readonly int rows = 20, cols = 20;
        private readonly Image[,] gridImages;
        private GameState gamestate;
        private bool gameRunning;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gamestate = new GameState(rows, cols);
        }

        private async Task RunGame()
        {
            Draw();
            Overlay.Visibility = Visibility.Hidden;
            await GameLoop();
            await ShowGameover();
            gamestate = new GameState(rows, cols);
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!gameRunning)
            {
                gameRunning = true;
                await RunGame();
                gameRunning = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamestate.Gameover)
            {
                return;
            }

            if (e.Key == Key.Up)
            {
                gamestate.ChangeDirection(Direction.Up);
                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                gamestate.ChangeDirection(Direction.Down);
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                gamestate.ChangeDirection(Direction.Left);
                e.Handled = true;
            }
            else if (e.Key == Key.Right)
            {
                gamestate.ChangeDirection(Direction.Right);
                e.Handled = true;
            }
        }



        private async Task GameLoop()
        {
            while (!gamestate.Gameover)
            {
                await Task.Delay(100);
                gamestate.Move();
                Draw();
            }
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }
            return images;
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE {gamestate.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridval = gamestate.Grid[r, c];
                    gridImages[r, c].Source = gridValtoImage[gridval];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }

        private void DrawSnakeHead()
        {
            Position headPos = gamestate.Headposition();
            Image image = gridImages[headPos.Row, headPos.Column];
            image.Source = Images.Head;

            int rotation = dirToRotation[gamestate.Dir];
            image.RenderTransform = new RotateTransform(rotation);
        }

        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gamestate.SnakePositions());
            for(int i = 0; i < positions.Count; i++)
            {
                Position pos = positions[i];
                ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
                gridImages[pos.Row, pos.Column].Source = source;
                await Task.Delay(40);
            }
        }

        private async Task ShowGameover()
        {
            await DrawDeadSnake();
            await Task.Delay(1000);
            Overlay.Visibility = Visibility.Visible;
            OverlayText.Text = "Game Over, Restart";
        }


    }
}