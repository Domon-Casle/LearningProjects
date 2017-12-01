using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Classes;
using TestBed.Factory;
using TestBed.Interfaces;

namespace TestBed.Ninject
{
    public class WeaponModule : NinjectModule
    {
        public override void Load()
        {
            WeaponFactory weaponFactory = new WeaponFactory();

            //Bind<IWeapon>().To<Sword>();
            Bind<IWeapon>().ToMethod(x =>
            {
                return weaponFactory.GetWeapon();
            });
        }
    }
}
