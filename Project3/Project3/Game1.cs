using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Xaria.Screens;
using Microsoft.Xna.Framework.Content;
using Android.Hardware;

namespace Xaria
{
    /// <summary>
    /// GameState controls the game's state
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// The start state
        /// </summary>
        Start,
        /// <summary>
        /// The playing state
        /// </summary>
        Playing,
        /// <summary>
        /// The end game state
        /// </summary>
        End,
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SensorManager sensorManager;
        private object accelerometer;
        private object magnetometer;
        private TouchCollection touchCollection;
        #region drawing
        /// <summary>
        /// The graphics device manager
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// The sprite batch for drawings
        /// </summary>
        SpriteBatch spriteBatch;
        RenderTarget2D renderTarget;
        #endregion
        #region screens
        /// <summary>
        /// The start screen
        /// </summary>
        Start startScreen;
        /// <summary>
        /// The end screen
        /// </summary>
        End endScreen;
        /// <summary>
        /// The background
        /// </summary>
        Background background;
        #endregion
        #region variables
        /// <summary>
        /// The screen size
        /// </summary>
        internal static Vector2 screenSize;
        /// <summary>
        /// The texture dictionary
        /// </summary>
        internal static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();
        /// <summary>
        /// The state
        /// </summary>
        internal static GameState state = GameState.Start;
        /// <summary>
        /// The font
        /// </summary>
        internal static SpriteFont font;
        //game variables
        /// <summary>
        /// The player
        /// </summary>
        internal Player player;
        /// <summary>
        /// The level
        /// </summary>
        internal Level level;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Game1"/> class.
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenSize = new Vector2() { X = 1024, Y = 2048 };
            renderTarget = new RenderTarget2D(GraphicsDevice, (int)screenSize.X, (int)screenSize.Y);
            sensorManager = (SensorManager)Activity.GetSystemService(Android.Content.Context.SensorService);
            accelerometer = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            magnetometer = sensorManager.GetDefaultSensor(SensorType.MagneticField);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new Start(Content);
           // startScreen = new Endless(Content);
            endScreen = new End(Content);

            textureDictionary.Add("ship", Content.Load<Texture2D>("player_ship"));
            textureDictionary.Add("laser", Content.Load<Texture2D>("Projectiles/laser"));
            textureDictionary.Add("basic", Content.Load<Texture2D>("Enemies/basic"));
            textureDictionary.Add("star", Content.Load<Texture2D>("star"));
            textureDictionary.Add("Boss1", Content.Load<Texture2D>("Enemies/Boss1"));
            textureDictionary.Add("beam", Content.Load<Texture2D>("Projectiles/beam"));
            textureDictionary.Add("shield", Content.Load<Texture2D>("Drops/shield"));
            font = Content.Load<SpriteFont>("font");

            player = new Player();
            level = new Level(1);
            background = new Background(2017);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //check for input
            background.Update(gameTime);
            //get input
            float[] R = new float[9];
            SensorManager.GetRotationMatrix(R, null, null, null);
            float[] orientation = new float[9];
            SensorManager.GetOrientation(R, orientation);
            float roll = (float)Java.Lang.Math.ToDegrees(orientation[2]);

            touchCollection = TouchPanel.GetState();

            switch (state)
                {
                    case GameState.Start:
                        startScreen.Update(touchCollection);
                        break;
                    case GameState.Playing:
                        level.Update(ref player, gameTime, touchCollection, roll);
                        break;
                    case GameState.End:
                        endScreen.Update(touchCollection);
                    break;
                }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin(); //do something with this to rescale
            // TODO: Add your drawing code here
            background.Draw(ref spriteBatch);
            switch(state)
            {
                case GameState.Start:
                    startScreen.Draw(ref spriteBatch);
                    break;
                case GameState.Playing:
                    player.Draw(ref spriteBatch);
                    level.Draw(ref spriteBatch);
                    break;
                case GameState.End:
                    endScreen.Draw(ref spriteBatch);
                    break;
            }

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
