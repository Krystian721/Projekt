namespace Kaczki
{
    #region Usings
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    #endregion

    public class Duck
    {
        #region Fields
        private Rectangle duckRectangle;
        private Boolean isFlying;
        private Boolean[] isDead;
        private int lives;
        private Boolean gameEnd;    
        private float timer;
        private float interval;
        private int currentFrame;
        private int frameCount;
        private int spriteWidth;
        private int spriteHeight;
        private Rectangle sourceRectangle;
        private SpriteEffects effects;
        private int flyTime;
        private int direction;
        private int fallTime;
        private Random rand;
        private int shot;
        private int minimumStartingX;
        private int maximumStartingX;
        private int startingY;
        private int frameWidth;
        private int frameHeight;
        private int screenWidth;
        private int optimalFlyTimer;
        private int verticalSpeed;
        private int horizontalSpeed;
        private int livesLimit;
        private int timeToGround;
        private int fallSpeed;
        private int shotLimit;
        #endregion

        #region Public Properties
        public Rectangle DuckRectangle { get => duckRectangle; set => duckRectangle = value; }
        public Rectangle SourceRectangle { get => sourceRectangle; set => sourceRectangle = value; }
        public SpriteEffects Effects { get => effects; set => effects = value; }
        public int SpriteWidth { get => spriteWidth; set => spriteWidth = value; }
        public int SpriteHeight { get => spriteHeight; set => spriteHeight = value; }
        public int FrameWidth { get => frameWidth; set => frameWidth = value; }
        public int FrameHeight { get => frameHeight; set => frameHeight = value; }
        public int ScreenWidth { get => screenWidth; set => screenWidth = value; }

        public int CurrentFrame { get => currentFrame; set => currentFrame = value; }
        public int FrameCount { get => frameCount; set => frameCount = value; }
        public bool IsFlying { get => isFlying; set => isFlying = value; }
        public int FlyTime { get => flyTime; set => flyTime = value; }
        public int Direction { get => direction; set => direction = value; }
        public int FallTime { get => fallTime; set => fallTime = value; }
        public int OptimalFlyTimer { get => optimalFlyTimer; set => optimalFlyTimer = value; }
        public int VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
        public int HorizontalSpeed { get => horizontalSpeed; set => horizontalSpeed = value; }
        public int TimeToGround { get => timeToGround; set => timeToGround = value; }
        public int FallSpeed { get => fallSpeed; set => fallSpeed = value; }

        public bool[] IsDead { get => isDead; set => isDead = value; }
        public int Lives { get => lives; set => lives = value; }
        public bool GameEnd { get => gameEnd; set => gameEnd = value; }
        public float Timer { get => timer; set => timer = value; }
        public float Interval { get => interval; set => interval = value; }
        public int Shot { get => shot; set => shot = value; }
        public int LivesLimit { get => livesLimit; set => livesLimit = value; }
        public int ShotLimit { get => shotLimit; set => shotLimit = value; }

        public Random Rand { get => rand; set => rand = value; }    
        public int MinimumStartingX { get => minimumStartingX; set => minimumStartingX = value; }
        public int MaximumStartingX { get => maximumStartingX; set => maximumStartingX = value; }
        public int StartingY { get => startingY; set => startingY = value; }
        #endregion

        #region Constructors and Deconstructors
        public Duck()
        {
            MinimumStartingX = 200;
            MaximumStartingX = 800;
            StartingY = 450;
            FrameWidth = 74;
            FrameHeight = 70;
            ScreenWidth = 1100;
            OptimalFlyTimer = 10;
            VerticalSpeed = 10;
            HorizontalSpeed = 15;
            LivesLimit = 20;
            TimeToGround = 50;
            FallSpeed = 5;
            ShotLimit = 3;

            IsDead = new Boolean[LivesLimit];
            Rand = new Random();
            DuckRectangle = new Rectangle(Rand.Next(MinimumStartingX, MaximumStartingX), StartingY, FrameWidth, FrameHeight);
            IsFlying = true;
            Timer = 0f;
            Interval = 1000f / 20f;
            CurrentFrame = 0;
            FrameCount = 8;
            SpriteWidth = 34;
            SpriteHeight = 31;
            Shot = ShotLimit;
        }
        #endregion

        #region Public Methods and Operators
        public void Update(float deltaTime)
        {
            Timer += deltaTime;
            if (Timer > Interval)
            {
                if (IsFlying)
                {
                    CurrentFrame++;
                    if (CurrentFrame > FrameCount - 6)
                        CurrentFrame = 0;
                    Timer = 0f;

                    if (FlyTime == 0)
                    {
                        Direction = Rand.Next(2);
                        FlyTime = OptimalFlyTimer;
                    }
                    if (DuckRectangle.X >= (ScreenWidth - FrameWidth))
                    {
                        Direction = 0;
                        FlyTime = OptimalFlyTimer;
                    }
                    if (DuckRectangle.X <= 0)
                    {
                        Direction = 1;
                        FlyTime = OptimalFlyTimer;
                    }
                    if (Direction == 0)
                    {
                        duckRectangle.X -= HorizontalSpeed;
                        duckRectangle.Y -= VerticalSpeed;
                        Effects = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        duckRectangle.X += HorizontalSpeed;
                        duckRectangle.Y -= VerticalSpeed;
                        Effects = SpriteEffects.None;
                    }
                    FlyTime--;
                    if (DuckRectangle.Y <= (-1 - FrameHeight))
                    {
                        duckRectangle.X = Rand.Next(MinimumStartingX, MaximumStartingX);
                        duckRectangle.Y = StartingY;
                        Lives++;
                        if (Lives == LivesLimit)
                        {
                            GameEnd = true;
                            IsFlying = false;
                        }
                        if (Shot != ShotLimit && !GameEnd)
                        {
                            Shot = ShotLimit;
                        }
                    }
                }
                else
                {
                    if (FallTime <= TimeToGround)
                    {
                        CurrentFrame = 6;
                    }
                    else if (DuckRectangle.Y <= StartingY)
                    {
                        CurrentFrame = 7;
                        duckRectangle.Y += FallSpeed;
                    }
                    else
                    {
                        IsFlying = true;
                        duckRectangle.X = Rand.Next(MinimumStartingX, MaximumStartingX);
                        duckRectangle.Y = StartingY;
                        FallTime = 0;
                        if (Lives == LivesLimit)
                        {
                            GameEnd = true;
                            IsFlying = false;
                        }
                        if (!GameEnd)
                        {
                            Shot = ShotLimit;
                        }
                    }

                    FallTime++;
                }
                SourceRectangle = new Rectangle(CurrentFrame * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            }
        }
        #endregion
    }
}
