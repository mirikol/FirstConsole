using System;

namespace FirstConsole
{
    public class Program
    {        
        static void Main(string[] args)
        {
            Input input = new Input();
            ConsoleWriter consoleWriter = new ConsoleWriter();
            Unit playerUnit = new Unit(50, 1000) { Armor = 15f, Name = "Игрок" };
            Unit enemyUnit = new Unit(55, 2000) { Armor = 25f, Name = "Противник" };


            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();
            playerUnit.Name = name;

            Player player = new Player(name, playerUnit, enemyUnit, input, consoleWriter);
            Enemy enemy = new Enemy(playerUnit, enemyUnit, input, consoleWriter);

            Console.Clear();
            Console.WriteLine($"Добро пожаловать в игру, {name}.\n");

            while (playerUnit.IsAlive && enemyUnit.IsAlive)
            {
                player.Turn();
                if (!enemyUnit.IsAlive)
                    break;
                enemy.Turn();
                if (!playerUnit.IsAlive)
                    break;

                consoleWriter.WriteDamagesFromTo(enemyUnit.DamageHistory, playerUnit.Name, enemyUnit.Name);
                consoleWriter.WriteDamagesFromTo(playerUnit.DamageHistory, enemyUnit.Name, playerUnit.Name);

                EndTurn();
            }

            Console.WriteLine("\nИгра окончена!");
            Console.WriteLine(playerUnit.IsAlive ? "Вы победили!" : "Противник победил!");

            void EndTurn()
            {
                Console.WriteLine("\nДля продолжения нажмите любую клавишу.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}