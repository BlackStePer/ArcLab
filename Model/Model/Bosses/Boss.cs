using Model.BossesAttacks;
using System;

namespace Model.Bosses
{
    /// <summary>
    /// Абстрактный класс, описывающий общую структуру и поведение босса.
    /// </summary>
    public abstract class Boss
    {
        public int HP { get; protected set; }
        public int MaxHP { get; protected set; }
        public string Name { get; protected set; }
        protected BossAttack[] _bossAttacks;

        /// <summary> Событие, вызываемое при запуске боссом любой атаки.</summary>
        public event Action<BossAttack> AttackLaunched;

        /// <summary> Событие, вызываемое в момент гибели босса.</summary>
        public event Action BossDied;

        /// <summary> Событие, вызываемое при получении боссом урона.</summary>
        public event Action DamageTaken;

        /// <summary>
        /// Запускает следующую по очереди атаку босса.
        /// </summary>
        /// <returns>Объект выполненной атаки BossAttack.</returns>
        public abstract BossAttack LaunchAttack();

        /// <summary>
        /// Наносит урон боссу и уменьшает его текущее здоровье.
        /// </summary>
        /// <param name="damage">Количество получаемого урона.</param>
        public abstract void TakeDamage(int damage);

        /// <summary> Вызов события AttackLaunched из классов-наследников. </summary>
        protected void OnAttackLaunched(BossAttack attack)
        {
            AttackLaunched?.Invoke(attack);
        }

        /// <summary> Вызов события DamageTaked из классов-наследников. </summary>
        protected void OnDamageTaked()
        {
            DamageTaken?.Invoke();
        }

        /// <summary> Вызов события BossDied из классов-наследников. </summary>
        protected void OnBossDied()
        {
            BossDied?.Invoke();
        }
    }
}
