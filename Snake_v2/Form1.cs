using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Snake.Properties;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private List<Circle> border = new List<Circle>();
        private Circle food = new Circle();
        private Circle Badfood = new Circle();
        private Circle freezyApple = new Circle();
        private Circle speedUp = new Circle();

        public Form1()
        {
            InitializeComponent();
            //Set game speed and start timer
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start New game
            StartGame();
        }

        internal void StartGame()
        {
            lblGameOver.Visible = false;

            //Set settings to default
            new Settings();

            //Create new player object
            Snake.Clear();
            Circle head = new Circle {X = 10, Y = 5};
            Snake.Add(head);


            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
            GenerateFreezyFood();
            GenerateSpeedFood();
            Generateborder();

        }

        //Place random food object
        private void GenerateFood()
        {

            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(1, maxXPos-2), Y = random.Next(1, maxYPos-2) };
            Badfood = new Circle { X = random.Next(1, maxXPos / 2), Y = random.Next(1, maxYPos / 2) };

            /*//уникнення спавну їжі на border
            for (int i = 0; i < border.Count; i++)
            {
                if (food.X == border[i].X && food.Y == border[i].Y)
                {
                    A:
                    food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
                    if (food.X == border[i].X && food.Y == border[i].Y)
                        goto A;
                }
            }*/

            //уникнення спавну їжі на тілі змійки
            for (int i = 0; i < Snake.Count; i++)
            {
                if (food.X == Snake[i].X && food.Y == Snake[i].Y)
                {
                    A:
                    food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
                    if (food.X == Snake[i].X && food.Y == Snake[i].Y)
                        goto A;
                }
            }
        }

        //Adds elements of circles to a list "border" to describe the borders of the game
        private void Generateborder()
        {
            
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            for (int i = 0; i < maxXPos; i++)
            {
                if (i == 0 || i == maxXPos - 1)
                {
                    for (int j = 0; j < maxYPos; j++)
                    {
                        if (j == maxYPos / 2 || j == (maxYPos / 2)-1 || j == (maxYPos / 2) + 1) continue;
                        border.Add(new Circle { X = i, Y = j });
                    }
                }
                if (i > 0 && i < maxXPos-1)
                {
                    if (i == maxXPos / 2 || i == (maxXPos / 2) - 1 || i == (maxXPos / 2) + 1) continue;
                    for (int j = 0; j < 1; j++)
                    {
                        border.Add(new Circle { X = i, Y = j });
                    }
                    for (int j = maxYPos - 1; j < maxYPos; j++)
                    {
                        border.Add(new Circle { X = i, Y = j });
                    }
                }
            }
        }
        private void GenerateFreezyFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            freezyApple = new Circle { X = random.Next(1, maxXPos), Y = random.Next(1, maxYPos) };
        }
        private void GenerateSpeedFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            speedUp = new Circle { X = random.Next(2, maxXPos), Y = random.Next(2, maxYPos) };
        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                MovePlayer();
            }

            pbCanvas.Invalidate();

        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                //Set colour of snake
                
                //Draw snake
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush snakeColour;
                    if (i == 0)
                        snakeColour = Brushes.Black;     //Draw head
                    else
                        snakeColour = Brushes.Green;    //Rest of body

                    //Draw snake
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                      Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));


                    //Draw Food
                    canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width,
                             food.Y * Settings.Height, Settings.Width, Settings.Height));

                    //Draw badFood
                    canvas.FillEllipse(Brushes.Black,
                        new Rectangle(Badfood.X * Settings.Width,
                             Badfood.Y * Settings.Height, Settings.Width, Settings.Height));

                    //Draw AppleFreezy
                    canvas.FillEllipse(Brushes.Aqua,
                    new Rectangle(freezyApple.X * Settings.Width,
                         freezyApple.Y * Settings.Height, Settings.Width, Settings.Height));

                    //Draw speedApple
                    canvas.FillEllipse(Brushes.Gold,
                    new Rectangle(speedUp.X * Settings.Width,
                         speedUp.Y * Settings.Height, Settings.Width, Settings.Height));

                    //Draw borders
                    for (int k = 0; k < border.Count; k++)
                    {
                            canvas.FillEllipse(Brushes.Blue,
                                new Rectangle(border[k].X * Settings.Width,
                                    border[k].Y * Settings.Height, Settings.Width, Settings.Height));
                    }
                }
            }
            else
            {
                string gameOver = "Game over \nYour final score is: " + 
                    Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }


        private void MovePlayer()
        {
            for (int i = Snake.Count - 1 ; i >= 0; i--)
            {
                //Move head
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }


                    //Get maximum X and Y Pos
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;


                    //Allow snake to go through the screen.
                    if (Snake[i].X < 0) Snake[i].X = maxXPos;
                    else if (Snake[i].X >= maxXPos) Snake[i].X = 0;
                    else if (Snake[i].Y < 0) Snake[i].Y = maxYPos;
                    else if (Snake[i].Y >= maxYPos) Snake[i].Y = 0;


                    //Detect collission with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X &&
                           Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    //Detect collision with food piece
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                    if (Snake[0].X == Badfood.X && Snake[0].Y == Badfood.Y)
                    {
                        Die();
                    }
                    if (Snake[0].X == freezyApple.X && Snake[0].Y == freezyApple.Y)
                    {
                        if (gameTimer.Interval == 750 / Settings.Speed)
                        {
                            gameTimer.Interval = 1000 / Settings.Speed;
                            EatAppleFreezy();
                        }
                        else
                            EatAppleFreezy();
                    }
                    if (Snake[0].X == speedUp.X && Snake[0].Y == speedUp.Y)
                    {
                        if (gameTimer.Interval != 1000 / Settings.Speed)
                        {
                            gameTimer.Interval = 1000 / Settings.Speed;
                            EatSpeedUp();
                        }
                        else
                            EatSpeedUp();
                    }

                    //Detect collision with borders
                    for (int k = 0; k < border.Count; k++)
                    {
                        if (Snake[0].X == border[k].X && Snake[0].Y == border[k].Y)
                        {
                            Die();
                        }
                    }

                }
                else
                {
                    //Move body
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void Eat()
        {
            //Add circle to body
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            //Update Score
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }
        public void EatAppleFreezy()
        {
            //Add circle to body
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            //Update Score
            gameTimer.Interval = 2000 / Settings.Speed;
            Settings.Score += Settings.Points + 10;
            lblScore.Text = Settings.Score.ToString();
            GenerateFreezyFood();
        }

        public void EatSpeedUp()
        {
            //Add circle to body
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            //Update Score
            gameTimer.Interval = 750 / Settings.Speed;
            Settings.Score += Settings.Points + 15;
            lblScore.Text = Settings.Score.ToString();
            GenerateSpeedFood();
        }

        private void Die()
        {
            Settings.GameOver = true;
            Record();
        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }
        internal void Record()
        {
            if(Settings.Score > Properties.Settings.Default.Score1)
            {
                Properties.Settings.Default.Score1 = Settings.Score;
                Form4 ui = new Form4();
                ui.ShowDialog();
                //Properties.Settings.Default.Name1 = ui.;
                //Properties.Settings.Default.Save();
            }
            
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {

        }
    }
}
