using System;
using System.Collections.Generic;
using System.Text;

namespace TestBed.Interfaces
{
    public interface IWeapon
    {
        int Damage { get; }
        int Defence { get; }
        int DefendAttack(IWeapon weapon);
    }
}
