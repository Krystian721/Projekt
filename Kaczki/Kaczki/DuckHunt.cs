namespace Kaczki
{
    #region Usings
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    #endregion
    public class DuckHunt : Game
    {
        #region Fields
        private SpriteBatch sprite;
        private GraphicsDeviceManager graphics;
        private int preferredBackBufferWidth;
        private int preferredBackBufferHeight;
        private Texture2D screen;
        private Rectangle screenRectangle;
        private Texture2D duck;
        private Duck target;
        private Vector2 scorePosition;
        private int scorePositionX;
        private int scorePositionY;
        private float deltaTime;
        private Vector2 availablePointsPosition;
        private int availablePointsPositionX;
        private int availablePointsPositionY;
        private int score;
        private int availablePoints;
        private SpriteFont font;
        private Vector2 endGamePosition;
        private int endGamePositionX;
        private int endGamePositionY;
        private Vector2 finalScorePosition;
        private int finalScorePositionX;
        private int finalScorePositionY;
        private Vector2 finalNumericScorePosition;
        private int finalNumericScorePositionX;
        private int finalNumericScorePositionY;
        private Color scoreFontColor;
        private Color availablePointsFontColor;
        private Color duckSpriteColor;
        private float duckSpriteRotation;
        private Vector2 duckSpriteOrigin;
        private float duckSpriteLayerDepth;
        private Color endGameFontColor;
        private Color finalScoreFontColor;
        private Color finalNumericScoreFontColor;
        private Point laserPosition;
        private bool isShooting;
        #endregion

        #region Public Properties
        public SpriteBatch Sprite { get => sprite; set => sprite = value; }
        public GraphicsDeviceManager Graphics { get => graphics; set => graphics = value; }
        public int PreferredBackBufferWidth { get => preferredBackBufferWidth; set => preferredBackBufferWidth = value; }
        public int PreferredBackBufferHeight { get => preferredBackBufferHeight; set => preferredBackBufferHeight = value; }
        public Texture2D Screen { get => screen; set => screen = value; }
        public Rectangle ScreenRectangle { get => screenRectangle; set => screenRectangle = value; }
        public Texture2D Duck { get => duck; set => duck = value; }
        public Duck Target { get => target; set => target = value; }
        public Vector2 ScorePosition { get => scorePosition; set => scorePosition = value; }
        public int ScorePositionX { get => scorePositionX; set => scorePositionX = value; }
        public int ScorePositionY { get => scorePositionY; set => scorePositionY = value; }
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }
        public Vector2 AvailablePointsPosition { get => availablePointsPosition; set => availablePointsPosition = value; }
        public int AvailablePointsPositionX { get => availablePointsPositionX; set => availablePointsPositionX = value; }
        public int AvailablePointsPositionY { get => availablePointsPositionY; set => availablePointsPositionY = value; }
        public int Score { get => score; set => score = value; }
        public int AvailablePoints { get => availablePoints; set => availablePoints = value; }
        public SpriteFont Font { get => font; set => font = value; }
        public Vector2 EndGamePosition { get => endGamePosition; set => endGamePosition = value; }
        public Vector2 FinalScorePosition { get => finalScorePosition; set => finalScorePosition = value; }
        public Vector2 FinalNumericScorePosition { get => finalNumericScorePosition; set => finalNumericScorePosition = value; }
        public int EndGamePositionX { get => endGamePositionX; set => endGamePositionX = value; }
        public int EndGamePositionY { get => endGamePositionY; set => endGamePositionY = value; }
        public int FinalScorePositionX { get => finalScorePositionX; set => finalScorePositionX = value; }
        public int FinalScorePositionY { get => finalScorePositionY; set => finalScorePositionY = value; }
        public int FinalNumericScorePositionX { get => finalNumericScorePositionX; set => finalNumericScorePositionX = value; }
        public int FinalNumericScorePositionY { get => finalNumericScorePositionY; set => finalNumericScorePositionY = value; }
        public Color ScoreFontColor { get => scoreFontColor; set => scoreFontColor = value; }
        public Color AvailablePointsFontColor { get => availablePointsFontColor; set => availablePointsFontColor = value; }
        public Color DuckSpriteColor { get => duckSpriteColor; set => duckSpriteColor = value; }
        public float DuckSpriteRotation { get => duckSpriteRotation; set => duckSpriteRotation = value; }
        public Vector2 DuckSpriteOrigin { get => duckSpriteOrigin; set => duckSpriteOrigin = value; }
        public float DuckSpriteLayerDepth { get => duckSpriteLayerDepth; set => duckSpriteLayerDepth = value; }
        public Color EndGameFontColor { get => endGameFontColor; set => endGameFontColor = value; }
        public Color FinalScoreFontColor { get => finalScoreFontColor; set => finalScoreFontColor = value; }
        public Color FinalNumericScoreFontColor { get => finalNumericScoreFontColor; set => finalNumericScoreFontColor = value; }
        public Point LaserPosition { get => laserPosition; set => laserPosition = value; }
        public bool IsShooting { get => isShooting; set => isShooting = value; }
        #endregion

        #region Constructors and Deconstructors
        public DuckHunt()
        {
            PreferredBackBufferWidth = 1100;
            PreferredBackBufferHeight = 700;
            ScorePositionX = 825;
            ScorePositionY = 623;
            AvailablePointsPositionX = 825;
            AvailablePointsPositionY = 555;
            EndGamePositionX = 400;
            EndGamePositionY = 150;
            FinalScorePositionX = 475;
            FinalScorePositionY = 200;
            FinalNumericScorePositionX = 490;
            FinalNumericScorePositionY = 250;
            ScoreFontColor = new Color(64, 191, 255);
            AvailablePointsFontColor = new Color(64, 191, 255);
            DuckSpriteColor = Color.White;
            DuckSpriteRotation = 0;
            DuckSpriteOrigin = Vector2.Zero;
            DuckSpriteLayerDepth = 0;
            EndGameFontColor = Color.Black;
            FinalScoreFontColor = Color.Black;
            FinalNumericScoreFontColor = Color.Black;

            IsShooting = false;
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = PreferredBackBufferWidth;
            Graphics.PreferredBackBufferHeight = PreferredBackBufferHeight;
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Public Methods and Operators
        #endregion

        #region Methods
        protected override void Initialize()
        {
            ScreenRectangle = new Rectangle(0, 0, PreferredBackBufferWidth, PreferredBackBufferHeight);
            Target = new Duck();
            ScorePosition = new Vector2(ScorePositionX, ScorePositionY);
            AvailablePointsPosition = new Vector2(AvailablePointsPositionX, AvailablePointsPositionY);
            EndGamePosition = new Vector2(EndGamePositionX, EndGamePositionY);
            FinalScorePosition = new Vector2(FinalScorePositionX, FinalScorePositionY);
            FinalNumericScorePosition = new Vector2(FinalNumericScorePositionX, FinalNumericScorePositionY);
            LaserPosition = new Point(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Screen = Content.Load<Texture2D>("screen");
            Duck = Content.Load<Texture2D>("duck");
            Font = Content.Load<SpriteFont>("Quartz");
            Sprite = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {
            Sprite.Begin();
            Sprite.Draw(Screen, ScreenRectangle, Color.White);
            Sprite.DrawString(Font, Score + "", ScorePosition, ScoreFontColor);
            Sprite.DrawString(Font, AvailablePoints + "", AvailablePointsPosition, AvailablePointsFontColor);
            Sprite.Draw(Duck, Target.DuckRectangle, Target.SourceRectangle, DuckSpriteColor, DuckSpriteRotation, DuckSpriteOrigin, Target.Effects, DuckSpriteLayerDepth);

            if (Target.GameEnd)
            {
                Sprite.DrawString(Font, "KONIEC GRY!", EndGamePosition, EndGameFontColor);
                Sprite.DrawString(Font, "WYNIK:", FinalScorePosition, FinalScoreFontColor);
                Sprite.DrawString(Font, Score + "", FinalNumericScorePosition, FinalNumericScoreFontColor);
            }

            Sprite.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (!Target.GameEnd)
            {
                Target.Update(DeltaTime);

                if (Target.IsFlying)
                {
                    if (Target.Shot == 3)
                        AvailablePoints = (Target.DuckRectangle.Y + Target.FrameHeight) * (Target.Shot);
                    else
                        AvailablePoints = (Target.DuckRectangle.Y + Target.FrameHeight) * (Target.Shot + 1);
                }

                if (AvailablePoints <= 0)
                {
                    AvailablePoints = 0;
                }

                if (isShooting && Target.Shot != 0)
                {
                    isShooting = false;
                    Target.Shot--;

                    if ((LaserPosition.X >= Target.DuckRectangle.X && LaserPosition.X <= Target.DuckRectangle.X + 65) &&
                        (LaserPosition.Y >= Target.DuckRectangle.Y && LaserPosition.Y <= Target.DuckRectangle.Y + 71) && (Target.IsFlying))
                    {
                        Target.IsFlying = false;
                        Score += AvailablePoints;
                        Target.IsDead[Target.Lives] = true;
                        Target.Lives++;
                    }
                }
            }

            base.Update(gameTime);
        }
        #endregion
    }
}

