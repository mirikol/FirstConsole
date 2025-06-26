using System;
using System.Collections.Generic;

namespace FirstConsole
{
    internal class ConsoleWriter
    {
        private void DamageMessage(string damager, string defender, float damage)
        {
            Console.WriteLine($"Игрок {damager} наносит игроку {defender} урон в размере ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(damage);
            Console.ResetColor();
            Console.WriteLine(" единиц.");
        }

        private void HealMessage(string healer, float value)
        {
            Console.WriteLine($"Игрок {healer} восстановил себе здоровье в размере {value} единиц.");
        }
        private void WriteDamagesWithType(AttackType attackType, string damagerName, string defenderName, List<float> damages)
        {
            foreach (var damage in damages)
            {
                switch (attackType)
                {
                    case AttackType.Damage:
                        DamageMessage(damagerName, defenderName, damage);
                        break;
                    case AttackType.Self:
                        DamageMessage(defenderName, defenderName, damage);
                        break;
                    case AttackType.Heal:
                        HealMessage(defenderName, damage);
                        break;
                }
            }

            damages.Clear();
        }

        public void WriteDamagesFromTo(Dictionary<AttackType, List<float>> damages, string damagerName, string defenderName)
        {
            foreach (var damage in damages)
            {
                WriteDamagesWithType(damage.Key, damagerName, defenderName, damage.Value);
            }
        }

        public void WriteUnitHealth(string owner, Unit unit)
        {
            float healthPercentage = (float)unit.CurrentHealth / unit.MaxHealth * 100;
            Console.WriteLine($"{owner}: ");

            Console.ForegroundColor = healthPercentage > 10 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(unit.CurrentHealth);
            Console.ResetColor();
        }

        public void WriteAllAbilities(string message, Unit unit)
        {
            Console.WriteLine($"{message}");
            for (int i = 0; i < unit.AbilityCount; i++)
            {
                Console.WriteLine($"{i+1}. {unit.GetAbilityDescription(i)}");
            }
            Console.WriteLine();
        }
    }
}
