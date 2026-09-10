using Model.Bosses;
using Model.BossesAttacks;
using System;

namespace Model
{
    /// <summary>
    /// Класс управляющий игровой сессией и взаимодействием сущностей.
    /// </summary>
    public class GameLogic
    {
        private readonly Random _random = new Random();

        public Player CurrentPlayer { get; private set; }
        public Boss CurrentBoss { get; private set; }

        public GameLogic(Player player, Boss boss)
        {
            CurrentPlayer = player;
            CurrentBoss = boss;
        }

        /// <summary> Вызывает механику атаки игрока по привязанному боссу. </summary>
        public void PerformPlayerAttack()
        {
            int damage = CurrentPlayer.LaunchAttack();
            CurrentBoss.TakeDamage(damage);
        }

        /// <summary> Вызывает механику атаки босса по привязанному игроку. </summary>
        public BossAttack PerformBossAttack()
        {
            BossAttack attack = CurrentBoss.LaunchAttack();

            CurrentPlayer.TakeDamage(attack.Damage);

            return attack;
        }

        /// <summary> Проверяет, завершился ли бой гибелью одного из участников.</summary>
        /// <returns>Окончен ли бой.</returns>
        public bool IsBattleOver()
        {
            return CurrentPlayer.HP <= 0 || CurrentBoss.HP <= 0;
        }

        /// <summary>
        /// Генерирует случайный математический пример для механики уклонения.
        /// </summary>
        /// <returns>Кортеж, содержащий строку с примером и целочисленный правильный ответ.</returns>
        public (string Question, int Answer) GenerateMathPuzzle()
        {
            int a = _random.Next(10, 50);
            int b = _random.Next(10, 50);

            if (_random.Next(2) == 0)
            {
                return ($"{a} + {b}", a + b);
            }
            else
            {
                return ($"{a} - {b}", a - b);
            }
        }

        /// <summary>
        /// Рассчитывает финальную денежную награду за победу над боссом.
        /// </summary>
        /// <returns>Количество заработанного золота.</returns>
        public int CalculateVictoryLoot()
        {
            int baseGold = CurrentBoss.MaxHP / 10;
            int bonus = _random.Next(20, 101);
            return baseGold + bonus;
        }
    }
}
