using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Project3.Screens;
using System.Collections.Generic;
using System.Linq;

namespace Project3
{
    public enum GameState
    {
        Start,
        Playing,
        Paused,
        End,
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        internal static Vector2 scale;
        //screens
        Start startScreen;
        End endScreen;
        Pause pauseScreen;
        //variables
        public static Vector2 screenSize;
        internal static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();
        internal static GameState state = GameState.Start;
        //game variables
        internal Player player;
        //we need a start screen
        //also need a rendertarget
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
            screenSize = new Vector2() { X = GraphicsDevice.Viewport.Width, Y = GraphicsDevice.Viewport.Height };
            scale = new Vector2() { X = GraphicsDevice.Viewport.Width / 1080f, Y = GraphicsDevice.Viewport.Height / 1799f }; //draw textures relative to a 1280 by 720 screen
            TouchPanel.DisplayHeight = GraphicsDevice.Viewport.Height;
            TouchPanel.DisplayWidth = GraphicsDevice.Viewport.Width;
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
            // TODO: use this.Content to load your game content here

            textureDictionary.Add("ship", Content.Load<Texture2D>("ship"));
            textureDictionary.Add("laser", Content.Load<Texture2D>("laser"));
            player = new Player(100);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //default code
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            //check for input
            TouchCollection touchCollection = TouchPanel.GetState();
                switch (state)
                {
                    case GameState.Start:
                        startScreen.Update(touchCollection);
                        break;
                    case GameState.Playing:
                        player.Update(touchCollection);
                        player.Shoot(gameTime);
                        break;
                    case GameState.End:
                        endScreen.Update(touchCollection);
                        break;
                    case GameState.Paused:
                        pauseScreen.Update(touchCollection);
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
            spriteBatch.Begin(); //do something with this to rescale
            // TODO: Add your drawing code here
            /*enemies.ForEach((Enemy enemy)=>{
                enemy.Draw(ref spriteBatch);
            });*/
            switch(state)
            {
                case GameState.Start:
                    startScreen.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    player.Draw(ref spriteBatch);
                    player.Projectiles.ForEach((Projectile projectile) =>
                    {
                        projectile.Draw(ref spriteBatch);
                    });
                    break;
                case GameState.End:
                    endScreen.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    pauseScreen.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
