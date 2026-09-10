using System;
<<<<<<< HEAD

=======
>>>>>>> unityView

namespace Model
{
    /// <summary>
    /// Класс харрактеристик игрока
    /// </summary>
    public class Player
    {
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int Damage { get; private set; }

        /// <summary> Событие, вызываемое при совершении атаки игроком. </summary>
        public event Action AttackLaunched;
        
        /// <summary> Событие, вызываемое при смерти игрока. </summary>
        public event Action PlayerDied;

        /// <summary> Событие, вызываемое при получении игроком урона. </summary>
        public event Action DamageTaken;

        public Player()
        {
            HP = 100;
            MaxHP = 100;
            Damage = 10;
        }

        /// <summary>
        /// Совершает атаку и возвращает значение наносимого урона.
        /// </summary>
        /// <returns>Значение базового урона игрока.</returns>
        public int LaunchAttack()
        {
            AttackLaunched?.Invoke();
            return Damage;
        }

        /// <summary>
        /// Наносит урон игроку и уменьшает его текущее здоровье.
        /// </summary>
        /// <param name="damage">Количество получаемого урона.</param>
        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);

            if(HP <= 0)
            {
                PlayerDied?.Invoke();
            }
            else
            {
                DamageTaken?.Invoke();
            }
        }

        /// <summary>
        /// Восстанавливает здоровье игрока на указанную величину, не превышая максимум.
        /// </summary>
        /// <param name="amount">Количество восстанавливаемого здоровья.</param>
        public void Heal(int amount)
        {
            if (HP <= 0) return;
            HP = Math.Min(MaxHP, HP + amount);
        }
    }
}
