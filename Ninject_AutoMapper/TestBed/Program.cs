using AutoMapper;
using Ninject;
using Ninject.Parameters;
using System;
using TestBed.Classes;
using TestBed.Ninject;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            // Map what you want ninject to do.
            IKernel kernel = new StandardKernel(new WeaponModule(), new NinjaModule());

            // Basic mapper useage
            Mapper.Initialize(cfg =>
            {
                //Create maps so mapper knows what can go to what
                cfg.CreateMap<Shinobi, Samurai>();
                cfg.CreateMap<Shinobi, Monk>();
            });

            // Display how a shinobi 'could' be built
            Shinobi ninja1 = new Shinobi(new Sword(), "Sam");
            Shinobi ninja2 = new Shinobi(new NinjaStar(), "Unknown");

            // Use ninject to create a shinobi using what it knows how to build.
            // Also displays how to pass arguements to the constructor for Shinobi.
            Shinobi ninja3 = kernel.Get<Shinobi>(new[] { new ConstructorArgument("name", "Unknown2") });

            // Use auto mapper to copy a shinobi into a samurai/monk class
            Samurai ninja4 = Mapper.Map<Samurai>(ninja3);
            Monk ninja5 = Mapper.Map<Monk>(ninja3);

            // Build the testbed which requires both the kernal from ninject AND automapper 
            TestBed temp = kernel.Get<TestBed>();
            TestBed temp2 = kernel.Get<TestBed>();

            Console.WriteLine("Hit any key...");
            Console.ReadKey();
        }
    }
}
