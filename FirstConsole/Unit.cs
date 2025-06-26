using System.Collections.Generic;
namespace FirstConsole
{
    public class Unit
    {
        private int damage;
        private int maxHealth;
        private int currentHealth;
        private bool isAlive = true;
        private int shieldCount = 0;
        private bool lastDamageFromWeapon = false;
        private readonly List<string> abilityDescriptions = new List<string>();
        private readonly Dictionary<AttackType, List<float>> damageHistory = new Dictionary<AttackType, List<float>>();
        public IReadOnlyDictionary<AttackType, List<float>> DamageHistory => damageHistory;

        public string Name { get; set; }
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
                else                
                {
                    isAlive = true;
                    if (currentHealth > maxHealth)
                        currentHealth = maxHealth;
                }
            }
        }
        public bool IsAlive => isAlive;
        public int ShieldCount
        {
            get => shieldCount;
            set => shieldCount = value;
        }

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
