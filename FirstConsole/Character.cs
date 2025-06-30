using System;
using static FirstConsole.Interfaces;

namespace FirstConsole
{
    public abstract class Character
    {
        public string Name { get; set; }

        protected readonly IUnit MyUnit;
        protected readonly IUnit OpponentUnit;
        protected readonly Input inputHandler;
        protected readonly ConsoleWriter Writer;

        protected Character(
            IUnit myUnit,
            IUnit opponentUnit,
            Input input,
            ConsoleWriter writer)
        {
            MyUnit = myUnit;
            OpponentUnit = opponentUnit;
            inputHandler = input;
            Writer = writer;
        }
        public abstract void Turn();

        protected virtual void ApplyDamage(int damage, IUnit source, IUnit target)
        {
            target.TakeDamage(damage, source, false);
        }

        protected virtual void ApplyWeaponDamage(Weapon weapon, IUnit source, IUnit target)
        {
            int finalDamage = weapon.Damage;

            if (source == MyUnit && source is ICriticalStrike striker && striker.TryCriticalStrike())
            {
                finalDamage *= 2;
                Writer.WriteCriticalStrike(source.Name);
            }

            if (source is Mage)
            {
                weapon = new Weapon()
                {
                    Damage = weapon.Damage,
                    Effect = EffectType.Fire
                };
            }

            target.TakeDamage(
                DamageCalculator.CalculateAttackWithSpecialEffect(
                    weapon.Effect,
                    finalDamage,
                    target.Armor
                    ),
                source,
                true
                );
        }

        protected Weapon GetCurrentWeapon(EffectType effect = EffectType.None)
        {
            return new Weapon()
            {
                Damage = MyUnit.Damage,
                Effect = effect
            };
        }

        protected virtual void ApplyMainWeaponDamage()
        {
            var effect = MyUnit is Mage ? EffectType.Fire : EffectType.None;
            var weapon = GetCurrentWeapon(effect);
            ApplyWeaponDamage(weapon, MyUnit, OpponentUnit);
        }
    }
}
