namespace FirstConsole
{
    public static class DamageCalculator
    {
        public static int CalculateAttackWithSpecialEffect(EffectType effectType, float damage, float armor)
        {
            float finalDamage = damage;
            float modifiedArmor = armor;

            switch (effectType)
            {
                case EffectType.Ice:
                    finalDamage += 5;
                    modifiedArmor = 0;
                    break;
                case EffectType.Fire:
                    finalDamage += 10;
                    break;
            }

            if (modifiedArmor > 0)
            {
                float damageReduction = finalDamage * (modifiedArmor / 100f);
                finalDamage -= damageReduction;
            }
            return (int)MathF.Round(finalDamage);
        }
    }
}