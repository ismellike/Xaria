namespace Xaria.Enemies
{
    /// <summary>
    /// The inheritable boss enemy class.
    /// </summary>
    /// <seealso cref="Xaria.Enemy" />
    abstract class Boss : Enemy
    {
        /// <summary>
        /// 
        /// </summary>
        public new enum Type
        {
            /// <summary>
            /// The boss1
            /// </summary>
            Boss1 = 1,
            /// <summary>
            /// The boss2
            /// </summary>
            Boss2 = 2,
            /// <summary>
            /// The boss3
            /// </summary>
            Boss3 = 3,
            /// <summary>
            /// The boss4
            /// </summary>
            Boss4 = 4,
        }

        /// <summary>
        /// Gets or sets the type of the boss.
        /// </summary>
        /// <value>
        /// The type of the boss.
        /// </value>
        protected Type BossType { get; set; }
    }
}