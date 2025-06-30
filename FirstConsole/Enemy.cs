using System;
using System.Collections.Generic;

namespace FirstConsole
{
    public class Enemy : Character
    {
        private readonly int _secondAbilityModifier = 3;
        private readonly int _thirdAbilityValue = 100;
        private readonly Weapon _enemyWeapon;
        private const int AbilitiesCount = 3;

        public Enemy(Unit playerUnit, Unit enemyUnit, Input input, ConsoleWriter writer)
            : base(enemyUnit, playerUnit, input, writer)
        {
            _enemyWeapon = new Weapon { Damage = enemyUnit.Damage, Effect = EffectType.Fire };
            InitializeAbilities();
        }

        private void InitializeAbilities()
        {
            MyUnit.AddAbilityDescriptions($"Атака: наносит {MyUnit.Damage} урона (+огонь)");
            MyUnit.AddAbilityDescriptions($"Ярость: наносит себе {MyUnit.Damage} и врагу {MyUnit.Damage * _secondAbilityModifier})");
            MyUnit.AddAbilityDescriptions($"Контратака: если последний урон от оружия, наносит себе {_thirdAbilityValue}, иначе лечит на {_thirdAbilityValue}");
        }
        
        public override void Turn()
        {
            switch (inputHandler.GetAICommandNumber(AbilitiesCount))
            {
                case 1:
                   ApplyWeaponDamage(_enemyWeapon, MyUnit, OpponentUnit);
                    break;
                case 2:
                    int modifiedDamage = MyUnit.Damage * _secondAbilityModifier;
                    ApplyDamage(MyUnit.Damage, MyUnit, MyUnit);
                    ApplyDamage(modifiedDamage, MyUnit, OpponentUnit);
                    break;
                case 3:
                    if (MyUnit.LastDamageFromWeapon)
                    {
                        ApplyDamage(_thirdAbilityValue, MyUnit, MyUnit);
                    }
                    else
                    {
                        ApplyDamage(-_thirdAbilityValue, MyUnit, MyUnit);
                    }
                    break;
            }
        }
    }
}