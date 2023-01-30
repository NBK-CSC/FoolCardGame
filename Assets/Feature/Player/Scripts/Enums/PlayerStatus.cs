namespace FoolCardGame.Player.Enums
{
    /// <summary>
    /// Статусы игроков в раунде
    /// </summary>
    public enum PlayerStatus
    {
        /// <summary>
        /// Подкидывающий отключенный
        /// </summary>
        ThrowerDisabled,
        
        /// <summary>
        /// Подкидывающий включенный
        /// </summary>
        ThrowerEnabled,
        
        /// <summary>
        /// Подкидывающий включенный
        /// </summary>
        ThrowerActive,
        
        /// <summary>
        /// Подкидывающий ожидающий
        /// </summary>
        ThrowerPassive,
        
        /// <summary>
        /// Защитник включенный
        /// </summary>
        DefenderActive,
        
        /// <summary>
        /// Защитник ожидающий
        /// </summary>
        DefenderPassive
    }
}