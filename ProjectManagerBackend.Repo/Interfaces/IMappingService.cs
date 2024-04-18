using ProjectManagerBackend.Repo.DTOs;
using ProjectManagerBackend.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    //public interface IMappingService<TSource, TDestination>
    public interface IMappingService
    {
        public UserDetail AddUser (UserDetailDTO userDetailDTO);
        //TDestination Map(TSource source);
        public UserDetail UserToken(TokenDTO tokenDTO);

        public TMapped? Map<T, TMapped>(T source);
    }
}
