namespace Xaria
{
    /// <summary>
    /// The weapon class 
    /// </summary>
    class Weapon
    {
        /// <summary>
        /// Gets the type of the projectile.
        /// </summary>
        /// <value>
        /// The type of the projectile.
        /// </value>
        public Projectile.Type ProjectileType { get; private set; }
        /// <summary>
        /// Gets the ammo.
        /// </summary>
        /// <value>
        /// The ammo.
        /// </value>
        public int Ammo { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Weapon"/> is infinite.
        /// </summary>
        /// <value>
        ///   <c>true</c> if infinite; otherwise, <c>false</c>.
        /// </value>
        public bool Infinite { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Weapon"/> is immovable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if immovable; otherwise, <c>false</c>.
        /// </value>
        public bool Immovable { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        /// <param name="projectileType">Type of the projectile.</param>
        /// <param name="ammo">The ammo.</param>
        /// <param name="infinite">if set to <c>true</c> [infinite].</param>
        /// <param name="immovable">if set to <c>true</c> [immovable].</param>
        public Weapon(Projectile.Type projectileType, int ammo, bool infinite = false, bool immovable = false)
        {
            ProjectileType = projectileType;
            Ammo = ammo;
            Infinite = infinite;
            Immovable = immovable;
        }

        /// <summary>
        /// Increases the ammo.
        /// </summary>
        /// <param name="ammo">The ammo.</param>
        public void IncreaseAmmo(int ammo)
        {
            Ammo += ammo;
        }

        /// <summary>
        /// Decreases the ammo.
        /// </summary>
        public void DecreaseAmmo()
        {
            Ammo--;
        }
    }
}