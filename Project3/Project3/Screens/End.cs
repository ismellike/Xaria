using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Screens
{
    /// <summary>
    /// The end screen class
    /// </summary>
    /// <seealso cref="Xaria.Screen" />
    class End : Screen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="End" /> class.
        /// </summary>
        /// <param name="Content">The content.</param>
        public End(ContentManager Content)
        {
            Texture2D endTexture = Content.Load<Texture2D>("end");
            Buttons.Add(new Button(endTexture, new Vector2((Game1.screenSize.X - endTexture.Width) / 2f, Game1.screenSize.Y-300), GoToStart));
        }

        /// <summary>
        /// Draws the buttons, and tells you your progress.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        internal override void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (Button button in Buttons)
                button.Draw(ref spriteBatch);
            spriteBatch.DrawString(Game1.font, "You made it to level " + Game1.level.Difficulty + ".", new Vector2(Game1.screenSize.X/2f - 200, Game1.screenSize.Y - 500), Color.Yellow, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Goes to start game state.
        /// </summary>
        void GoToStart()
        {
            Game1.state = GameState.Start;
        }
    }
}