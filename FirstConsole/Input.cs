using System;

namespace FirstConsole
{
    public class Input
    {
        private Random random = new Random();

        public int PlayerCommandNumber
        {
            get
            {
                Console.Write("Введите номер: ");
                int commandNumber;
                while (!int.TryParse(Console.ReadLine(), out commandNumber))
                {
                    Console.WriteLine("Команда не распознана. Попробуйте ещё раз.\n");
                    Console.Write("Введите номер: ");
                }
                return commandNumber;
            }
        }

        public int GetAICommandNumber(int maxNumbers) => random.Next(1, maxNumbers + 1);
    }
}