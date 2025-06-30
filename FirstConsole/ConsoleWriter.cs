using System;
using System.Collections.Generic;
using static FirstConsole.Interfaces;

namespace FirstConsole
{
    public class ConsoleWriter
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

        public void WriteDamagesFromTo(IReadOnlyDictionary<AttackType, List<float>> damages, string damagerName, string defenderName)
        {
            foreach (var damage in damages)
            {
                WriteDamagesWithType(damage.Key, damagerName, defenderName, damage.Value);
            }
        }

        public void WriteUnitHealth(string owner, IDamageable damageable)
        {
            if (damageable is Unit unit)
            {
                float healthPercentage = (float)unit.CurrentHealth / unit.MaxHealth * 100;
                Console.WriteLine($"{owner}: ");
                Console.ForegroundColor = healthPercentage > 10 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(unit.CurrentHealth);
                Console.ResetColor();
            }
        }

        public void WriteAllAbilities(string message, IHasAbilities abilityOwner)
        {
            Console.WriteLine($"{message}");
            for (int i = 0; i < abilityOwner.AbilityCount; i++)
            {
                Console.WriteLine($"{i+1}. {abilityOwner.GetAbilityDescription(i)}");
            }
            Console.WriteLine();
        }

        public void WriteShieldActivation(string owner)
        {
            Console.WriteLine($"Игрок {owner} активировал щит! Следующая атака будет заблокирована.");
            Console.WriteLine();
        }

        public void WriteShieldBlock(string attacker, string defender)
        {
            string safeAttacker = string.IsNullOrEmpty(attacker) ? "Неизвестный" : attacker;
            string safeDefender = string.IsNullOrEmpty(defender) ? "Неизвестный" : defender;

            Console.WriteLine($"Щит игрока {safeDefender} блокировал атаку {safeAttacker}!");
            Console.WriteLine();
        }

        public void WriteCriticalStrike(string attacker)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Критический удар {attacker}!");
            Console.ResetColor();
        }

        public void WriteRageStatus(int rage, int maxRage)
        {
            Console.Write($"Ярость: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{rage} / {maxRage}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
