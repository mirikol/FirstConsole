using System;

namespace FirstConsole
{
    public enum AttackType
    {
        Damage,
        Self,
        Heal
    }
    public class Program
    {        
        static void Main(string[] args)
        {
            Input input = new Input();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Unit playerUnit = new Unit(50, 1000) { Armor = 15f };
            Unit enemyUnit = new Unit(55, 2000) { Armor = 25f };


            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            Player player = new Player(name, playerUnit, enemyUnit, input, consoleWriter);
            Enemy enemy = new Enemy(playerUnit, enemyUnit, input);

            Console.Clear();
            Console.WriteLine($"Добро пожаловать в игру, {name}.\n");

            while (playerUnit.IsAlive && enemyUnit.IsAlive)
            {
                player.Turn();
                enemy.Turn();

                consoleWriter.WriteDamagesFromTo(enemyUnit.DamageHistory, name, "Enemy");
                consoleWriter.WriteDamagesFromTo(playerUnit.DamageHistory, "Enemy", name);

                EndTurn();
            }

            Console.WriteLine("\nИгра окончена!");
            Console.WriteLine(playerUnit.IsAlive
                ? "Вы победили!"
                : "Противник победил!");

            void EndTurn()
            {
                Console.WriteLine("\nДля продолжения нажмите любую клавишу.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}