using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using AutoMapper;
using Vidly2.Dtos;
using Vidly2.Models;

namespace Vidly2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.MovieId, opt =>
  opt.Ignore());
            Mapper.CreateMap<Genre, GenreDto>();
        }
    }
}