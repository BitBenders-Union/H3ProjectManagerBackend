using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Interfaces
{
    public interface IHashingService
    {
        public byte[] PasswordHashing(string password, byte[] salt);
        public byte[] GenerateSalt();
    }
}
