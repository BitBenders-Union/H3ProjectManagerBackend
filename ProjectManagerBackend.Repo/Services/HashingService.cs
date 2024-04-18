using Konscious.Security.Cryptography;
using ProjectManagerBackend.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerBackend.Repo.Services
{
    public class HashingService : IHashingService
    {
        public byte[] GenerateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return salt;
        }

        public byte[] PasswordHashing(string password, byte[] salt)
        {
            Argon2id argon2 = new (Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                Iterations = 2,
                MemorySize = 1024
            };

            byte[] hash = argon2.GetBytes(128 / 8);

            return hash;
        }
    }
}
