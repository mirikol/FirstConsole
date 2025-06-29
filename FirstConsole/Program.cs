using System;

namespace FirstConsole
{
    public class Program
    {        
        static void Main(string[] args)
        {
            Input input = new Input();
            ConsoleWriter consoleWriter = new ConsoleWriter();

            Console.Write("Введите ваше имя: ");
            string name = Console.ReadLine();

            Console.WriteLine("\nВыберите класс:");
            Console.WriteLine("1. Воин (атака + ярость)");
            Console.WriteLine("2. Маг (стихийный урон)");
            Console.WriteLine("3. Ассасин (шанс крита)");

            Unit playerUnit = Console.ReadLine()
                switch
            {
                "1" => new Warrior(50, 1000) { Armor = 15f, Name = name },
                "2" => new Mage(45, 900) { Armor = 10f, Name = name },
                "3" => new Assassin(60, 800) { Armor = 12f, Name = name },
                _ => new Unit(50, 1000) { Armor = 15f, Name = name }
            };

            Unit enemyUnit = new Unit(55, 2000) { Armor = 25f, Name = "Противник" };

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