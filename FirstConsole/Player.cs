using System;

namespace FirstConsole
{
    internal class Player
    {        
        private Input input;
        private ConsoleWriter writer;
        private readonly Weapon playerWeapon;
        private readonly Weapon fireballWeapon;
        private string Name { get; }
        private Unit PlayerUnit { get; }
        private Unit EnemyUnit { get; }

        public int FireballDamage => fireballWeapon.Damage;
        public Player(string name, Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer)
        {
            Name = name;
            PlayerUnit = playerUnit;
            EnemyUnit = enemyUnit;
            this.input = input;
            this.writer = writer;

            fireballWeapon = new Weapon {Damage = 200};

            playerWeapon = new Weapon {
                Damage = playerUnit.Damage,
                Effect = EffectType.Ice
            };

            PlayerUnit.AddAbilityDescriptions($"Ударить оружием (урон {PlayerUnit.Damage} + лёд)");
            PlayerUnit.AddAbilityDescriptions($"Щит: следующая атака противника не наносит урона");
            PlayerUnit.AddAbilityDescriptions($"Огненный шар: наносит урон в размере {FireballDamage} единиц");
        }

        public void Turn()
        {
            writer.WriteUnitHealth("Ваше здоровье", PlayerUnit);
            writer.WriteUnitHealth("Здоровье противника", EnemyUnit);
            Console.WriteLine();

            writer.WriteAllAbilities($"{Name}! Выберите действие: ", PlayerUnit);


            switch (input.PlayerCommandNumber)
            {
                case 1:
                    EnemyUnit.TakeDamage(playerWeapon, PlayerUnit);
                    break;
                case 2:
                    PlayerUnit.ShieldCount =1;
                    break;
                case 3:                   
                    EnemyUnit.TakeDamage(fireballWeapon, PlayerUnit);
                    break;
            }
        }
    }
}
