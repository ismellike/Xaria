using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Text;
using Xaria.Enemies;

namespace Xaria
{
    /// <summary>
    /// The testing class
    /// </summary>
    class Test
    {
        /// <summary>
        /// The level
        /// </summary>
        private Level level = new Level(1);
        /// <summary>
        /// The difficulty
        /// </summary>
        private int difficulty = 1;
        /// <summary>
        /// The test content
        /// </summary>
        StringBuilder testContent = new StringBuilder();
        /// <summary>
        /// The run tests
        /// </summary>
        bool runTests = true;

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="touchCollection">The touch collection.</param>
        public void Update(GameTime gameTime, TouchCollection touchCollection)
        {
            if (runTests)
            {
                RunTests();
                runTests = false;
            }
            CheckLevels(touchCollection);
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
        private void RunTests()
        {
            Player player = new Player();
            TestPlayerDamage(ref player);
            TestPlayeAmmos(ref player);
            TestEnemiesDamage();
            //testContent.AppendLine("info");
        }

        /// <summary>
        /// Tests the enemies damage.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestEnemiesDamage()
        {
            const int enemyCount = 7;
            int damage = 50;

            Enemy[] enemy = new Enemy[enemyCount] { new Basic(Vector2.Zero), new Intermediate(Vector2.Zero), new Advanced(Vector2.Zero), new Boss1(Vector2.Zero),
                new Boss2(Vector2.Zero), new Boss3(Vector2.Zero), new Boss4(Vector2.Zero)};
            for (int i = 0; i < enemyCount; i++)
            {
                int eHealth = enemy[i].GetHealth();
                enemy[i].Damage(damage);
                if(eHealth - damage == enemy[i].GetHealth())
                    testContent.AppendLine("Testing Enemy.Damage(int damage) for "+enemy[i].GetType().Name+" : PASSED");
                else
                    testContent.AppendLine("Testing Enemy.Damage(int damage) for " + enemy[i].GetType().Name + " : FAILED");
            }
        }

        /// <summary>
        /// Tests the player add ammos.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestPlayeAmmos(ref Player player)
        {
            int rocketAmmo = 0;
            int beamAmmo = 0; //infinite
            int ammoToAdd = 5;

            for (int i = 0; i < 3; i++)
            {
                switch (player.weapon.ProjectileType)
                {
                    case Projectile.Type.Laser:
                        player.Shoot();
                        if (player.Weapons[0].Ammo == 1)
                            testContent.AppendLine("Testing Player.Shoot() w/ Laser : PASSED");
                        else
                            testContent.AppendLine("Testing Player.Shoot() w/ Laser : FAILED");

                        player.IncreaseAmmo(Projectile.Type.Rocket, rocketAmmo = ammoToAdd);
                        if (rocketAmmo == player.Weapons.Find(weapon => weapon.ProjectileType == Projectile.Type.Rocket).Ammo)
                            testContent.AppendLine("Testing Player.IncreasAmmo(Projectile.Type.Rocket, "+ammoToAdd.ToString()+") : PASSED");
                        else
                            testContent.AppendLine("Testing Player.IncreasAmmo(Projectile.Type.Rocket, " + ammoToAdd.ToString() + ") : FAILED");

                        player.SwitchWeapon();
                        if (player.weapon.ProjectileType == Projectile.Type.Rocket)
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Laser : PASSED");
                        else
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Laser: FAILED");

                        break;
                    case Projectile.Type.Beam:
                        player.Shoot();
                        beamAmmo--;
                        if (beamAmmo == player.Weapons.Find(weapon => weapon.ProjectileType == Projectile.Type.Beam).Ammo)
                            testContent.AppendLine("Testing Player.Shoot() w/ Rocket : PASSED");
                        else
                            testContent.AppendLine("Testing Player.Shoot() w/ Rocket : FAILED");

                        player.SwitchWeapon();
                        if (player.weapon.ProjectileType == Projectile.Type.Laser)
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Beam : PASSED");
                        else
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Beam: FAILED");
                        break;
                    case Projectile.Type.Rocket:
                        player.Shoot();
                        rocketAmmo--;
                        if (rocketAmmo == player.Weapons.Find(weapon => weapon.ProjectileType == Projectile.Type.Rocket).Ammo)
                            testContent.AppendLine("Testing Player.Shoot() w/ Rocket : PASSED");
                        else
                            testContent.AppendLine("Testing Player.Shoot() w/ Rocket : FAILED");

                        player.IncreaseAmmo(Projectile.Type.Beam, beamAmmo = ammoToAdd);
                        if (beamAmmo == player.Weapons.Find(type => type.ProjectileType == Projectile.Type.Beam).Ammo)
                            testContent.AppendLine("Testing Player.IncreasAmmo(Projectile.Type.Beam, " + ammoToAdd.ToString() + ") : PASSED");
                        else
                            testContent.AppendLine("Testing Player.IncreasAmmo(Projectile.Type.Beam, " + ammoToAdd.ToString() + ") : FAILED");

                        player.SwitchWeapon();
                        if (player.weapon.ProjectileType == Projectile.Type.Beam)
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Rocket : PASSED");
                        else
                            testContent.AppendLine("Testing Player.SwitchWeapon() from Rocket: FAILED");
                        break;
                }
            }
        }

        /// <summary>
        /// Tests the player damage.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestPlayerDamage(ref Player player)
        {
            int pHealth = player.Health;
            int damage = 50;
            player.Damage(damage);
            if (pHealth - damage == player.Health)
                testContent.AppendLine("Testing Player.Damage(int damage) w/o shield : PASSED");
            else
                testContent.AppendLine("Testing Player.Damage(int damage) w/o shield : FAILED");

            TestAddShield(ref player);
            int shield = player.Shield;
            damage = 30;
            player.Damage(damage);
            if (shield - damage == player.Shield)
                testContent.AppendLine("Testing Player.Damage(int damage) w/ shield : PASSED");
            else
                testContent.AppendLine("Testing Player.Damage(int damage) w/ shield : FAILED");
        }

        private void TestAddShield(ref Player player)
        {
            int shield = player.Shield;
            int strength = 50;
            player.AddShield(strength);

            if (shield + strength == player.Shield)
                testContent.AppendLine("Testing Player.AddShield(int strength) : PASSED");
            else
                testContent.AppendLine("Testing Player.AddShield(int strength) : FAILED");
        }

        /// <summary>
        /// Shoulds the test.
        /// </summary>
        /// <param name="touchCollection">The touch collection.</param>
        /// <returns></returns>
        bool ShouldTest(TouchCollection touchCollection)
        {
            foreach(TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Released)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks the levels.
        /// </summary>
        /// <param name="touchCollection">The touch collection.</param>
        private void CheckLevels(TouchCollection touchCollection)
        {
            if(ShouldTest(touchCollection))
            {
                if (difficulty >= Level.FINAL_LEVEL)
                {
                    Reset();
                }
                else
                {
                    difficulty++;
                    level = new Level(difficulty);
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(ref SpriteBatch spriteBatch)
        {
            level.Draw(ref spriteBatch);
            spriteBatch.DrawString(Game1.font, testContent, new Vector2(100, 1200), Color.White);
        }

        void Reset()
        {
            difficulty = 1;
            level = new Level(difficulty);
            testContent.Clear();
            runTests = true;
            Game1.state = GameState.Start;
        }
    }
}