using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstConsole
{
    public class Interfaces
    {
        public interface IDamageable
        {
            void TakeDamage(int damage, IUnit origin, bool isWeaponDamage);
            bool IsAlive { get; }
            int CurrentHealth { get; }
            string Name { get; }
        }

        public interface IUpdatable
        {
            void Update();
        }

        public interface IHasAbilities
        {
            int AbilityCount { get; }
            string GetAbilityDescription(int index);
            void AddAbilityDescriptions(string description);
        }

        public interface ICriticalStrike
        {
            bool TryCriticalStrike();
        }

        public interface IWeaponEffect
        {
            int ApplyEffect(int baseDamage, float armor);
        }

        public interface IUnit : IDamageable, IHasAbilities
        {
            int Damage { get; }
            float Armor { get; set; }
            bool LastDamageFromWeapon { get; }
            bool InShield { get; }
            int ShieldCount { get; set; }
        }

    }
}
