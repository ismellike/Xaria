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
            Texture2D startTexture = Content.Load<Texture2D>("start");
            Buttons.Add(new Button(startTexture, new Vector2((Game1.screenSize.X - startTexture.Width) / 2f, (Game1.screenSize.Y - startTexture.Height) / 2f), StartGame));
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        void StartGame()
        {
            Game1.state = GameState.Playing;
            Game1.player.Health = Player.STARTING_HEALTH;
            Game1.level = new Level(1);
        }
    }
}