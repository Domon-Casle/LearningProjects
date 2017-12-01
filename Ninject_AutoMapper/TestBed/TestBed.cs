using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Classes;
using Ninject;
using Ninject.Parameters;

namespace TestBed
{
    public class TestBed
    {
        private IMapper _mapper;

        public TestBed(IMapper mapper, IKernel kernal)
        {
            // Save the mapper
            _mapper = mapper;

            // Use the kernal to make a shinobi like before
            Shinobi ninja1 = kernal.Get<Shinobi>(new[] { new ConstructorArgument("name", "Unknown2") });

            // copy that new shinobi into a monk using the passed in Automapper 
            Monk ninaj2 = mapper.Map<Monk>(ninja1);
        }
    }
}
