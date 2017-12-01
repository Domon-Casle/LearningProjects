using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Interfaces;

namespace TestBed.Classes
{
    public class Sword : IWeapon
    {
        private int Damage;
        private int Defence;

        public Sword()
        {
            Damage = 4;
            Defence = 2;
        }

        int IWeapon.Damage
        {
            get
            {
                return Damage;
            }
        }

        int IWeapon.Defence
        {
            get
            {
                return Defence;
            }
        }

        public int DefendAttack(IWeapon weapon)
        {
            var damageTaken = (weapon.Damage - Defence);

            if (damageTaken < 0)
                damageTaken = 0;

            return damageTaken;
        }
    }
}
