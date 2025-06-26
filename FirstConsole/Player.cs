using System;

namespace FirstConsole
{
    public class Player : Character
    {
        private readonly Weapon _playerWeapon;
        private readonly Weapon _fireballWeapon;
        public string Name { get; }
        public int FireballDamage => _fireballWeapon.Damage;
        public Player(string name, Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer)
            : base(playerUnit, enemyUnit, input, writer)
        {
            Name = name;

            _fireballWeapon = new Weapon {Damage = 200};

            _playerWeapon = new Weapon
            {
                Damage = playerUnit.Damage,
                Effect = EffectType.Ice
            };

            MyUnit.AddAbilityDescriptions($"Ударить оружием (урон {MyUnit.Damage} + лёд)");
            MyUnit.AddAbilityDescriptions($"Щит: следующая атака противника не наносит урона");
            MyUnit.AddAbilityDescriptions($"Огненный шар: наносит урон в размере {FireballDamage} единиц");
        }

        public override void Turn()
        {
            Writer.WriteUnitHealth("Ваше здоровье", MyUnit);
            Writer.WriteUnitHealth("Здоровье противника", OpponentUnit);
            Console.WriteLine();

            Writer.WriteAllAbilities($"{Name}! Выберите действие: ", MyUnit);


            switch (InputHandler.PlayerCommandNumber)
            {
                case 1:
                    ApplyWeaponDamage(_playerWeapon, MyUnit, OpponentUnit);
                    break;
                case 2:
                    MyUnit.ShieldCount = 1;
                    Writer.WriteShieldActivation(Name);
                    break;
                case 3:                   
                    ApplyWeaponDamage(_fireballWeapon, MyUnit, OpponentUnit);
                    break;
            }
        }
    }
}
