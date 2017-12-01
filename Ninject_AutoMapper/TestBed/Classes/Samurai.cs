using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Enums;
using TestBed.Interfaces;

namespace TestBed.Classes
{
    public class Samurai : INinja
    {
        public IWeapon Weapon;
        public int HP;
        public string Name;

        IWeapon INinja.Weapon { get { return Weapon; } }

        int INinja.HP { get { return HP; } }

        string INinja.Name { get { return Name; } }

        public void AttackTarget(INinja ninja)
        {
            ninja.DefendAttack(Weapon);
        }

        public void DefendAttack(IWeapon weapon)
        {
            HP -= Weapon.DefendAttack(weapon);
        }

        public Moves DoMove()
        {
            throw new NotImplementedException();
        }

        public bool Hide()
        {
            throw new NotImplementedException();
        }
    }
}
