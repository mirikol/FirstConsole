using System;

namespace FirstConsole
{
    public class Player : Character
    {
        private readonly Weapon _fireballWeapon;
        public int FireballDamage => _fireballWeapon.Damage;

        public Player(string name, Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer)
            : base(playerUnit, enemyUnit, input, writer)
        {
            Name = name;
            _fireballWeapon = new Weapon {Damage = 200};
            InitializeAbilities();            
        }

        private void InitializeAbilities()
        {
            string classAbility = MyUnit switch
            {
                Warrior => $"Удар мечом (урон {MyUnit.Damage})",
                Mage => $"Ледяная стрела (урон {MyUnit.Damage} + огонь)",
                Assassin => $"Скрытый удар (урон {MyUnit.Damage} + шанс крита)",
                _ => $"Удар оружием (урон {MyUnit.Damage})"
            };

            MyUnit.AddAbilityDescriptions(classAbility);
            MyUnit.AddAbilityDescriptions($"Щит: следующая атака противника не наносит урона");
            MyUnit.AddAbilityDescriptions($"Огненный шар: наносит урон в размере {_fireballWeapon.Damage} единиц");
        }

        public override void Turn()
        {
            var warrior = MyUnit as Warrior;

            warrior?.UpdateRageBuff();

            Writer.WriteUnitHealth("Ваше здоровье", MyUnit);
            Writer.WriteUnitHealth("Здоровье противника", OpponentUnit);
            Console.WriteLine();

            Writer.WriteAllAbilities($"{Name}! Выберите действие: ", MyUnit);


            switch (InputHandler.PlayerCommandNumber)
            {
                case 1:
                    if (warrior != null)
                    {
                        warrior.AddRage();
                        if (warrior.Rage == Warrior.MaxRage)
                        {
                            Writer.WriteRageStatus(warrior.Rage, Warrior.MaxRage);
                            warrior.ActivateRageBuff();
                        }
                    }
                    ApplyMainWeaponDamage();

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
