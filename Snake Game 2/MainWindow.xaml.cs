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

        private readonly int rows = 20, cols = 20;
        private readonly Image[,] gridImages;
        private GameState gamestate;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gamestate = new GameState(rows, cols);
        }



        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw();
            await GameLoop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamestate.Gameover)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    gamestate.ChangeDirection(Direction.Up); break;
                case Key.Down:
                    gamestate.ChangeDirection(Direction.Down); break;
                case Key.Left:
                    gamestate.ChangeDirection(Direction.Left); break;
                case Key.Right:
                    gamestate.ChangeDirection(Direction.Right); break;
            }
        }

        private async Task GameLoop()
        {
            while (!gamestate.Gameover)
            {
                await Task.Delay(80);
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
                        Source = Images.Empty
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
            ScoreText.Text = $"SCORE: {gamestate.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridval = gamestate.Grid[r, c];
                    gridImages[r, c].Source = gridValtoImage[gridval];
                }
            }
        }
    }
}