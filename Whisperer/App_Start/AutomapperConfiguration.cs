using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Whisperer.Models;

namespace Whisperer.App_Start
{
    public class Mapper
    {
        public static IMapper Map;
        public static void Initialize()
        {
            Map = new MapperConfiguration(c =>
            {
                c.CreateMap<ApiUser, User>()
                    .ForMember(d => d.UserId, m => m.MapFrom(s => s.id))
                    .ForMember(d => d.Username, m => m.MapFrom(s => s.name))
                    .ForAllOtherMembers(d=> d.Ignore())
                    ;
                c.CreateMap<User, ApiUser>()
                    .ForMember(d => d.id, m => m.MapFrom(s => s.UserId))
                    .ForMember(d => d.name, m => m.MapFrom(s => s.Username))
                    .ForAllOtherMembers(d => d.Ignore())
                    ;

                c.CreateMap<ApiChannel, Channel>()
                    .ForMember(d => d.ChannelId, m => m.MapFrom(s => s.id))
                    .ForMember(d => d.ChannelName, m => m.MapFrom(s => s.name))
                    .ForAllOtherMembers(d=> d.Ignore())
                    ;
                c.CreateMap<Channel, ApiChannel>()
                    .ForMember(d => d.id, m => m.MapFrom(s => s.ChannelId))
                    .ForMember(d => d.name, m => m.MapFrom(s => s.ChannelName))
                    .ForAllOtherMembers(d => d.Ignore())
                    ;
            }).CreateMapper();
        }
    }
}