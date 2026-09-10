using Model.BossesAttacks;
using System;

namespace Model.Bosses
{
    /// <summary>
    /// Реализация босса "Монолит" (Пирамида с глазом).
    /// </summary>
    public class MonolithBoss : Boss
    {
        private int _currentBossAttackIndex = 0;
        public MonolithBoss()
        {
            HP = 1000;
            MaxHP = 1000;
            Name = "Босс Монолит";
            _bossAttacks = new BossAttack[] { new ShockwaveAttack(), new RockRainAttack(), new LaserPlatesAttack() };
        }

        /// <summary> Переключает индекс на следующую атаку в массиве по кругу. </summary>
        private void ChangeAttackIndex()
        {
            if (_currentBossAttackIndex + 1 == _bossAttacks.Length)
                _currentBossAttackIndex = 0;
            else
                _currentBossAttackIndex++;
        }

        public override BossAttack LaunchAttack()
        {
            BossAttack attack = _bossAttacks[_currentBossAttackIndex];

            ChangeAttackIndex();

            OnAttackLaunched(attack);

            return attack;
        }

        public override void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
            if (HP > 0)
                OnDamageTaked();
            else
                OnBossDied();
        }
    }
}
