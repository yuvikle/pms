using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PositionManagement.Application.Commands;
using PositionManagement.Domain.Entities;

namespace PositionManagement.Application.Mapper
{
    public class TradeProfile : Profile
    {
        public TradeProfile()
        {
            CreateMap<CreateTradeCommand, Trade>()
                .ForMember(dest => dest.Action,
                       opt => opt.MapFrom(src => src.Action.ToUpper()))
                .ForMember(dest => dest.Type,
                       opt => opt.MapFrom(src => src.Type.ToUpper())); ;
        }
    }
}
