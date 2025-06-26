using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstConsole
{
    public enum EffectType
    {
        None,
        Ice,
        Fire
    }

    public class Weapon
    {
        public int Damage { get; set; }
        public EffectType Effect { get; set; } = EffectType.None;
    }
    public static class DamageCalculator
    {
        public static int CalculateAttackWithSpecialEffect(EffectType effectType, float damage, float armor)
        {
            float finalDamage = damage;

            switch (effectType)
            {
                case EffectType.Ice:
                    finalDamage += 5;
                    armor = 0;
                    break;
                case EffectType.Fire:
                    finalDamage += 10;
                    break;
            }

            if (armor > 0)
            {
                float damageReduction = finalDamage * (armor / 100f);
                finalDamage -= damageReduction;
            }

            return (int)MathF.Round(finalDamage);
        }
    }

    public class Unit
    {
        private int damage;
        private int maxHealth;
        private int currentHealth;
        private bool isAlive = true;
        private int shieldCount = 0;
        private bool lastDamageFromWeapon = false;
        private List<string> abilityDescriptions = new List<string>();

        private Dictionary<AttackType, List<float>> damageHistory = new Dictionary<AttackType, List<float>>();

        public int Damage => damage;
        public int MaxHealth => maxHealth;
        public bool LastDamageFromWeapon => lastDamageFromWeapon;
        public float Armor { get; set; }
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                if (currentHealth <= 0)
                {
                    isAlive = false;
                    currentHealth = 0;
                }
                else if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }
        public bool IsAlive
        {
            get => isAlive;
            set => isAlive = value;
        }

        public int ShieldCount
        {
            get => shieldCount;
            set => shieldCount = value;
        }

        public Dictionary<AttackType, List<float>> DamageHistory => damageHistory;
        public bool InShield => shieldCount > 0;
        public int AbilityCount => abilityDescriptions.Count;

        public Unit(int damage, int maxHealth)
        {
            this.damage = damage;
            this.maxHealth = maxHealth;

            currentHealth = maxHealth;

            damageHistory[AttackType.Damage] = new List<float>();
            damageHistory[AttackType.Self] = new List<float>();
            damageHistory[AttackType.Heal] = new List<float>();
        }

        private void ProcessDamage(int damage, Unit origin, bool isWeaponDamage)
        {
            lastDamageFromWeapon = isWeaponDamage;

            bool isHeal = damage < 0;
            AttackType attackType = AttackType.Damage;

            if (isHeal)
                attackType = AttackType.Heal;
            else if (origin == this)
                attackType = AttackType.Self;

            if (InShield && !isHeal)
            {
                shieldCount--;
                damageHistory[attackType].Add(0);
                return;
            }

            CurrentHealth -= damage;
            damageHistory[attackType].Add(damage);
        }

        public void TakeDamage(int damage, Unit origin)
        {
            int calculatedDamage = DamageCalculator.CalculateAttackWithSpecialEffect(
                EffectType.None,
                damage,
                Armor
            );
            ProcessDamage(calculatedDamage, origin, false);
        }

        public void TakeDamage(Weapon weapon, Unit origin)
        {
            if (ShieldCount > 0)
            {
                ShieldCount--;
                damageHistory[AttackType.Damage].Add(0);
                return;
            }
            int calculatedDamage = DamageCalculator.CalculateAttackWithSpecialEffect(
                weapon.Effect,
                weapon.Damage,
                Armor
            );
            ProcessDamage(calculatedDamage, origin, true);
        }

        public string GetAbilityDescription(int abilityNumber) => abilityDescriptions[abilityNumber];

        public void AddAbilityDescriptions(string description) => abilityDescriptions.Add(description);
        
    }
}
