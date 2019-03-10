using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Screens
{
    /// <summary>
    /// The start screen class
    /// </summary>
    /// <seealso cref="Xaria.Screen" />
    class Start : Screen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Start" /> class.
        /// </summary>
        /// <param name="Content">The content.</param>
        public Start(ContentManager Content)
        {
            Texture2D startTexture = Content.Load<Texture2D>("Buttons/start");
            Texture2D testTexture = Content.Load<Texture2D>("Buttons/test");
            Buttons.Add(new Button(startTexture, new Vector2(Game1.screenSize.X/ 3f - startTexture.Width/2f, 2*Game1.screenSize.Y / 3f - startTexture.Height/2f), StartGame));
            Buttons.Add(new Button(testTexture, new Vector2(2*Game1.screenSize.X / 3f - startTexture.Width / 2f, 2 * Game1.screenSize.Y / 3f - startTexture.Height / 2f), TestGame));

        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame()
        {
            Game1.state = GameState.Playing;
        }

        /// <summary>
        /// Tests the game.
        /// </summary>
        void TestGame()
        {
            Game1.state = GameState.Testing;
        }
    }
}