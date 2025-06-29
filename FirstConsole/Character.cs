using System;

namespace FirstConsole
{
    public abstract class Character
    {
        public string Name { get; set; }

        protected readonly Unit MyUnit;
        protected readonly Unit OpponentUnit;
        protected readonly Input InputHandler;
        protected readonly ConsoleWriter Writer;

        protected Character(Unit myUnit, Unit opponentUnit, Input input, ConsoleWriter writer)
        {
            MyUnit = myUnit;
            OpponentUnit = opponentUnit;
            InputHandler = input;
            Writer = writer;
        }
        public abstract void Turn();

        protected virtual void ApplyDamage(int damage, Unit source, Unit target)
        {
            target.TakeDamage(damage, source, false);
        }

        protected virtual void ApplyWeaponDamage(Weapon weapon, Unit source, Unit target)
        {
            int finalDamage = weapon.Damage;

            if (source == MyUnit)
            {
                switch (MyUnit)
                {
                    case Assassin assassin when assassin.TryCriticalStrike():
                        finalDamage *= 2;
                        Writer.WriteCriticalStrike(Name);
                        break;
                    case Mage:
                        weapon = new Weapon()
                        {
                            Damage = weapon.Damage,
                            Effect = EffectType.Fire
                        };
                        break;
                }
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

        protected bool IsAlive => MyUnit.IsAlive;

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
