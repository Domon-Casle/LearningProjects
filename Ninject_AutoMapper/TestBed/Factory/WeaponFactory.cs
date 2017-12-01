using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Classes;
using TestBed.Interfaces;

namespace TestBed.Factory
{
    public class WeaponFactory
    {
        private Random random;

        public WeaponFactory()
        {
            random = new Random();
        }

        public IWeapon GetWeapon() // Called by ninject to generate an IWeapon
        {
            int randomWeapon = random.Next(0, 2);

            switch (randomWeapon)
            {
                case 0:
                    Console.WriteLine("Generate a Sword");
                    return new Sword();
                    break;
                case 1:
                    Console.WriteLine("Generate a NinjaStar");
                    return new NinjaStar();
                    break;
            }

            return null;
        } 
    }
}
