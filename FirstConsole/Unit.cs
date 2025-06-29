using System.Collections.Generic;
namespace FirstConsole
{
    public class Unit
    {
        private int _damage;
        private int maxHealth;
        private int currentHealth;
        private bool isAlive = true;
        private int shieldCount = 0;
        private bool lastDamageFromWeapon = false;
        private readonly List<string> abilityDescriptions = new List<string>();
        private readonly Dictionary<AttackType, List<float>> damageHistory = new Dictionary<AttackType, List<float>>();
        public IReadOnlyDictionary<AttackType, List<float>> DamageHistory => damageHistory;

        public string Name { get; set; }
        public int Damage
        {
            get => _damage;
            protected set => _damage = value;
        }
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
            _damage = damage;
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;

            damageHistory[AttackType.Damage] = new List<float>();
            damageHistory[AttackType.Self] = new List<float>();
            damageHistory[AttackType.Heal] = new List<float>();
        }
                
        public virtual void TakeDamage(int damage, Unit origin, bool isWeaponDamage)
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

        public void TakeDamage(Weapon weapon, Unit origin)
        {
            TakeDamage(
                DamageCalculator.CalculateAttackWithSpecialEffect(
                weapon.Effect,
                weapon.Damage,
                Armor
            ),
            origin,
            true
        );
        }

        public string GetAbilityDescription(int abilityNumber) => abilityDescriptions[abilityNumber];

        public void AddAbilityDescriptions(string description) => abilityDescriptions.Add(description);
        
    }

    public class Warrior : Unit
    {
        public int Rage { get; private set; }
        public const int MaxRage = 2;
        private int _rageBuffTurns;
        private readonly int _baseDamage;

        public Warrior(int damage, int maxHealth) : base(damage, maxHealth)
        {
            _baseDamage = damage;
        }

        public void AddRage()
        {
            if (Rage < MaxRage)
            {
                Rage++;
            }
        }

        public void ActivateRageBuff()
        {
            if (Rage < MaxRage)
                return;

            Damage = (int)(_baseDamage * 3.5f);
            _rageBuffTurns = 3;
            Rage = 0;
        }

        public void UpdateRageBuff()
        {
            if (_rageBuffTurns > 0)
            {
                _rageBuffTurns--;
                if (_rageBuffTurns == 0)
                {
                    Damage = _baseDamage;
                }
            }
        }
    }
    public class Mage : Unit
    {
        public Mage(int damage, int maxHealth) : base(damage, maxHealth) { }
    }

    public class Assassin : Unit
    {
        private readonly Random _random = new();

        public Assassin(int damage, int maxHealth) : base(damage, maxHealth) { }

        public bool TryCriticalStrike() => _random.Next(100) < 20;
    }
}
