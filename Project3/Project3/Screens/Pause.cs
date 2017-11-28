using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Android.OS;
using System.IO;

namespace Xaria.Screens
{
    class Pause : Screen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Start" /> class.
        /// </summary>
        /// <param name="Content">The content.</param>
        public Pause(ContentManager Content)
        {
            Texture2D startTexture = Content.Load<Texture2D>("Buttons/pause");
            Buttons.Add(new Button(startTexture, new Vector2(100, 0), PauseGame));
        }
        void PauseGame()
        {

        }
    }
}