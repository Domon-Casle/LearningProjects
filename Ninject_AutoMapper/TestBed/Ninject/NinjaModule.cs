using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBed.AutoMapper;
using TestBed.Classes;
using TestBed.Interfaces;
using Ninject;

namespace TestBed.Ninject
{
    public class NinjaModule : NinjectModule
    {
        // Build a binding for IMapper then construct it and use the profile build for shinobi -> monk (This also is put into singleton so does not get built multiple times
        public override void Load()
        {
            Bind<IMapper>()
                .ToMethod(context =>
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<MonkProfile>();
                        cfg.ConstructServicesUsing(t => Kernel.Get(t));
                    });

                    Console.WriteLine("Build the Automapper class");
                    return config.CreateMapper();
                })
                .InSingletonScope();
        }
    }
}
