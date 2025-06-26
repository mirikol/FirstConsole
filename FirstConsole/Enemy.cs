using System;
using System.Collections.Generic;

namespace FirstConsole
{
    internal class Enemy
    {
        
        private Input input;        
        private int secondAbilityModifier;
        private int thirdAbilityValue;

        private Unit PlayerUnit { get; }
        private Unit EnemyUnit { get; }
        private int AbilitiesCount { get; }

        private Weapon enemyWeapon;

        public Enemy(Unit playerUnit, Unit enemyUnit, Input input)
        {
            PlayerUnit = playerUnit;
            EnemyUnit = enemyUnit;
            this.input = input;
            AbilitiesCount = 3;
            secondAbilityModifier = 3;
            thirdAbilityValue = 100;

            enemyWeapon = new Weapon {
                Damage = enemyUnit.Damage,
                Effect = EffectType.Fire
            };
        }
        
        public void Turn()
        {
            switch (input.GetAICommandNumber(AbilitiesCount))
            {
                case 1:
                    PlayerUnit.TakeDamage(enemyWeapon, EnemyUnit);
                    break;
                case 2:
                    int modifiedDamage = PlayerUnit.Damage * secondAbilityModifier;
                    EnemyUnit.TakeDamage(EnemyUnit.Damage, EnemyUnit);
                    PlayerUnit.TakeDamage(modifiedDamage, EnemyUnit);
                    break;
                case 3:
                    if (EnemyUnit.LastDamageFromWeapon)
                    {
                        EnemyUnit.TakeDamage(thirdAbilityValue, EnemyUnit);
                    }
                    else
                    {
                        EnemyUnit.TakeDamage(-thirdAbilityValue, EnemyUnit);
                    }
                    break;
            }
        }
    }
}
