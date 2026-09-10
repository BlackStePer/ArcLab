using System;
using System.Collections.Generic;
using System.Text;

namespace Model.BossesAttacks
{
    /// <summary>
    /// Перечисление типов атак босса.
    /// </summary>
    public enum AttackType
    {
        RockRain,    
        Shockwave,    
        LaserPlates   
    }

    /// <summary>
    /// Базовый класс для реализации различных стратегий атак босса.
    /// </summary>
    public abstract class BossAttack
    {
        public int Damage { get; protected set; }
        public string Name { get; protected set; }
        public AttackType Type { get; protected set; }
    }
}
