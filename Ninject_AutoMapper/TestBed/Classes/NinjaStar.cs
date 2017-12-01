using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Interfaces;

namespace TestBed.Classes
{
    public class NinjaStar : IWeapon
    {
        private int Damage;
        private int Defence;

        public NinjaStar()
        {
            Damage = 2;
            Defence = 0;
        }

        int IWeapon.Damage { get { return Damage; } }

        int IWeapon.Defence { get { return Defence; } }

        public int DefendAttack(IWeapon weapon)
        {
            return weapon.Damage;
        }
    }
}
