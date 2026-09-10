using Model;
using Model.Bosses;
using Model.BossesAttacks;
using System;

namespace ConsoleView
{

    internal class Program
    {
        private static void Main(string[] args)
        {

            Console.WriteLine("=== БИТВА С ДРЕВНИМ МОНОЛИТОМ НАЧАЛАСЬ ===");

            Player player = new Player();
            Boss boss = new MonolithBoss();

            GameLogic logic = new GameLogic(player, boss);

            boss.AttackLaunched += (attack) => Console.WriteLine($"\n{boss.Name} применяет: {attack.Name}! ({attack.Name})");

            boss.DamageTaken += () =>  Console.WriteLine($"Удар по боссу! У {boss.Name} осталось {boss.HP}/{boss.MaxHP} HP.");

            boss.BossDied += () => Console.WriteLine($"ПОБЕДА! {boss.Name} полностью разрушен!");

            player.AttackLaunched += () => Console.WriteLine($"\nВы наносите ответный удар!");

            player.DamageTaken += () => Console.WriteLine($"Вы получили урон! Ваше здоровье: {player.HP}/{player.MaxHP} HP");

            player.PlayerDied += () =>  Console.WriteLine($"Вы погибли на арене... Игра окончена.");

            while (!logic.IsBattleOver())
            {
                Console.WriteLine("\n---------------------------------------------");
                Console.WriteLine($"Вы: {player.HP}/{player.MaxHP} HP | {boss.Name}: {boss.HP}/{boss.MaxHP} HP");
                Console.WriteLine("Ваш ход: 1 - Атаковать | 2 - Лечение (+25 HP)");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    logic.PerformPlayerAttack();
                }
                else if (choice == "2")
                {
                    player.Heal(25);
                    Console.WriteLine($"Вы выпили зелье. Ваше здоровье: {player.HP}/{player.MaxHP} HP");
                }
                else
                {
                    Console.WriteLine("Неверный ввод! Вы замешкались и пропустили ход.");
                }
                if (logic.IsBattleOver()) break;

                var puzzle = logic.GenerateMathPuzzle();
                BossAttack currentAttack = logic.PerformBossAttack();

                if (currentAttack != null)
                {
                    Console.WriteLine($"БЫСТРО РЕШИТЕ ПРИМЕР ДЛЯ УКЛОНЕНИЯ: {puzzle.Question}");
                    Console.Write("Ваш ответ: ");

                    int.TryParse(Console.ReadLine(), out int playerAnswer);

                    if (playerAnswer == puzzle.Answer)
                    {
                        Console.WriteLine("Вы успели уклониться! Урон отменен.");
                        player.Heal(currentAttack.Damage);
                    }
                    else
                    {
                        Console.WriteLine($"Неверно! Правильный ответ: {puzzle.Answer}. Вы попали под удар!");
                    }
                }
            }

            Console.WriteLine("\n=============================================");
            if (player.HP > 0)
            {
                int gold = logic.CalculateVictoryLoot();
                Console.WriteLine($"Награда за победу: {gold} золотых монет!");
            }
            else
            {
                Console.WriteLine("Попробуйте снова.");
            }
            Console.WriteLine("=============================================");
            Console.ReadKey();
        }
    }
}