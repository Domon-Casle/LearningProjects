using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Enums;
using TestBed.Interfaces;

namespace TestBed.Classes
{
    public class Monk : INinja
    {
        public IWeapon Weapon { get; set; }

        public int HP { get; set; }

        public string Name { get; set; }

        public void AttackTarget(INinja ninja)
        {
            throw new NotImplementedException();
        }

        public void DefendAttack(IWeapon weapon)
        {
            throw new NotImplementedException();
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
