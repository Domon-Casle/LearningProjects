using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TestBed.Classes;

namespace TestBed.AutoMapper
{
    public class MonkProfile : Profile
    {
        // Build profile to map shinobi to samurai
        public MonkProfile()
        {
            CreateMap<Shinobi, Samurai>();
        }
    }
}
