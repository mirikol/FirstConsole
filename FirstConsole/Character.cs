using System;

namespace FirstConsole
{
    public abstract class Character
    {
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
            target.TakeDamage(damage, source);
        }

        protected virtual void ApplyWeaponDamage(Weapon weapon, Unit source, Unit target)
        {
            target.TakeDamage(weapon, source);
        }

        protected bool IsAlive => MyUnit.IsAlive;
    }
}
