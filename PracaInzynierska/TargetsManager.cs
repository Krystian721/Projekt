namespace PracaInzynierska
{
    #region Usings
    using System;
    using System.ComponentModel;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    #endregion
    class TargetsManager
    {
        #region Private Properties
        /// <summary>
        /// Current selected mode for creating targets
        /// </summary>
        private Mode selectedMode;

        /// <summary>
        /// Private property for generating random numbers
        /// </summary>
        private Random random;

        /// <summary>
        /// Screen height in pixels
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// Screen width in pixels
        /// </summary>
        private int screenWidth;
        #endregion

        #region Public Properties
        /// <summary>
        /// Enum for creating targets modes
        /// </summary>
        public enum Mode { Novice, Professional };

        /// <summary>
        /// Public property for set or get current creating targets mode
        /// </summary>
        public Mode SelectedMode
        {
            get
            {
                return this.selectedMode;
            }
            set
            {
                selectedMode = value;
            }
        }

        /// <summary>
        /// Public property for set or get variable generating random numbers
        /// </summary>
        public Random Random
        {
            get
            {
                return random;
            }
            set
            {
                random = value;
            }
        }

        /// <summary>
        /// Public property for get or set screen height variable
        /// </summary>
        public int ScreenHeight
        {
            get
            {
                return screenHeight;
            }
            set
            {
                screenHeight = value;
            }
        }

        /// <summary>
        /// Public property for get or set screen width variable
        /// </summary>
        public int ScreenWidth
        {
            get
            {
                return screenWidth;
            }
            set
            {
                screenWidth = value;
            }
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Constructor
        /// </summary>
        private TargetsManager()
        {
            Random = new Random();
            ScreenHeight = 0;
            ScreenWidth = 0;
        }
        #endregion

        #region Public Functions
        #endregion

        #region Singleton
        /// <summary>
        /// Instance of TargetsManager class
        /// </summary>
        private static TargetsManager instance;

        /// <summary>
        /// Public property to get instance of TargetsManager class
        /// </summary>
        public static TargetsManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TargetsManager();
                return instance;
            }
        }
        #endregion
    }

    class MovingObject
    {
        #region Private Properties
        /// <summary>
        /// Effects for spriteBatch
        /// </summary>
        private SpriteEffects effects;
        /// <summary>
        /// Target moving direction
        /// </summary>
        private int movingDirection;

        /// <summary>
        /// Time object is alive
        /// </summary>
        private int timeAlive;

        /// <summary>
        /// Source rectangle
        /// </summary>
        private Rectangle sourceRectangle;

        /// <summary>
        /// Destination rectangle
        /// </summary>
        private Rectangle destinationRectangle;

        /// <summary>
        /// Target height in pixels
        /// </summary>
        private int targetHeight;

        /// <summary>
        /// Target width in pixels
        /// </summary>
        private int targetWidth;

        /// <summary>
        /// Determines if object is moving
        /// </summary>
        private bool isMoving;

        /// <summary>
        /// Current frame of animation
        /// </summary>
        private int currentFrame;

        /// <summary>
        /// Number of frames in animation
        /// </summary>
        private int frameCount;

        /// <summary>
        /// Determines if user hits the target
        /// </summary>
        private bool isAlive;

        /// <summary>
        /// Worker for constantly update object
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// Timer for change animation frames
        /// </summary>
        private float timer;

        /// <summary>
        /// Time between animation frames change
        /// </summary>
        private float interval;

        /// <summary>
        /// Sprite width in pixels
        /// </summary>
        private int spriteWidth;

        /// <summary>
        /// Sprite height in pixels
        /// </summary>
        private int spriteHeight;
        #endregion

        #region Public Properties
        /// <summary>
        /// Public property for get or set effects variable
        /// </summary>
        public SpriteEffects Effects
        {
            get
            {
                return effects;
            }
            set
            {
                effects = value;
            }
        }
        /// <summary>
        /// Public property to get or set movingDirection variable
        /// </summary>
        public int MovingDirection
        {
            get
            {
                return movingDirection;
            }
            set
            {
                movingDirection = value;
            }
        }

        /// <summary>
        /// Public property for get or set timeAlive variable
        /// </summary>
        public int TimeAlive
        {
            get
            {
                return timeAlive;
            }
            set
            {
                timeAlive = value;
            }
        }
        
        /// <summary>
        /// Public property for set or get last created target
        /// </summary>
        public Rectangle SourceRectangle
        {
            get
            {
                return sourceRectangle;
            }
            set
            {
                sourceRectangle = value;
            }
        }

        /// <summary>
        /// Public property to get or set destinationRectangle
        /// </summary>
        public Rectangle DestinationRectangle
        {
            get
            {
                return destinationRectangle;
            }
            set
            {
                destinationRectangle = value;
            }
        }

        /// <summary>
        /// Public property for get or set target height variable
        /// </summary>
        public int TargetHeight
        {
            get
            {
                return targetHeight;
            }
            set
            {
                targetHeight = value;
            }
        }

        /// <summary>
        /// Public property for get or set target width variable
        /// </summary>
        public int TargetWidth
        {
            get
            {
                return targetWidth;
            }
            set
            {
                targetWidth = value;
            }
        }

        /// <summary>
        /// Public property for get or set isMoving variable
        /// </summary>
        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                isMoving = value;
            }
        }

        /// <summary>
        /// Public property for get or set current animation frame
        /// </summary>
        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }

        /// <summary>
        /// Public property for get or set frame count
        /// </summary>
        public int FrameCount
        {
            get
            {
                return frameCount;
            }
            set
            {
                frameCount = value;
            }
        }

        /// <summary>
        /// Public property for get or set target status
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }
        
        /// <summary>
        /// Public property for get or set worker
        /// </summary>
        public BackgroundWorker Worker
        {
            get
            {
                return worker;
            }
            set
            {
                worker = value;
            }
        }

        /// <summary>
        /// Public property for get or set timer
        /// </summary>
        public float Timer
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
            }
        }

        /// <summary>
        /// Public property for get or set interval
        /// </summary>
        public float Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }

        /// <summary>
        /// Public property for get or set spriteWidth variable
        /// </summary>
        public int SpriteWidth
        {
            get
            {
                return spriteWidth;
            }
            set
            {
                spriteWidth = value;
            }
        }

        /// <summary>
        /// Public property for get or set spriteHeight variable
        /// </summary>
        public int SpriteHeight
        {
            get
            {
                return spriteHeight;
            }
            set
            {
                spriteHeight = value;
            }
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Update target
        /// </summary>
        /// <param name="deltaTime"></param>
        private void Update(float deltaTime)
        {
            timer += deltaTime;

            if (Timer > Interval)
            {
                if (IsMoving)
                {
                    CurrentFrame++;
                    if (CurrentFrame > FrameCount - 6)
                        CurrentFrame = 0;
                    Timer = 0f;

                    if (TimeAlive == 0)
                    {
                        MovingDirection = TargetsManager.Instance.Random.Next(2);
                        TimeAlive = 10;
                    }

                    if (DestinationRectangle.X >= TargetsManager.Instance.ScreenWidth)
                    {
                        MovingDirection = 0;
                        TimeAlive = 10;
                    }

                    if (DestinationRectangle.X <= 0)
                    {
                        MovingDirection = 1;
                        TimeAlive = 10;
                    }

                    if (MovingDirection == 0)
                    {
                        destinationRectangle.X -= 15;
                        destinationRectangle.Y -= 10;
                        Effects = SpriteEffects.FlipHorizontally;
                    }
                    else
                    {
                        destinationRectangle.X += 15;
                        destinationRectangle.Y -= 10;
                        Effects = SpriteEffects.None;
                    }

                    TimeAlive--;
                }
                else
                {

                }

                SourceRectangle = new Rectangle(CurrentFrame * SpriteWidth, 0, SpriteWidth, SpriteHeight);
            }
        }

        /// <summary>
        /// Worker main task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {  
        }

        /// <summary>
        /// Function to change GUI from worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        /// <summary>
        /// Function called after completing worker job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Constructor
        /// </summary>
        public MovingObject()
        {
            DestinationRectangle = new Rectangle(TargetsManager.Instance.Random.Next(0, TargetsManager.Instance.ScreenWidth), 
                TargetsManager.Instance.ScreenHeight, TargetWidth, TargetHeight);
            IsMoving = true;
            CurrentFrame = 0;
            FrameCount = 8;
            IsAlive = true;
            Worker = new BackgroundWorker();
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Timer = 0f;
            Interval = 1000f / 20f;
            SpriteWidth = 34;
            SpriteHeight = 31;
        }

        /// <summary>
        /// Start the worker
        /// </summary>
        public void StartWorkerAsync()
        {
            Worker.RunWorkerAsync();
        }
        #endregion
    }
}
