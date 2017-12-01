using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Enums;

namespace TestBed.Interfaces
{
    public interface INinja
    {
        IWeapon Weapon { get; }
        int HP { get; }
        string Name { get; }
        void AttackTarget(INinja ninja);
        void DefendAttack(IWeapon weapon);
        bool Hide();
        Moves DoMove();
    }
}
