using static FirstConsole.Interfaces;

namespace FirstConsole
{
    public class Weapon : IWeaponEffect
    {
        public int Damage { get; init; }
        public EffectType Effect { get; init; } = EffectType.None;

        public int ApplyEffect(int baseDamage, float armor)
        {
            return DamageCalculator.CalculateAttackWithSpecialEffect(Effect, baseDamage, armor);
        }
    }

}
