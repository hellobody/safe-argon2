﻿using System;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SafeArgon2
{
    public static class PasswordHasher
    {
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);

            var _salt = new byte[] {
                0xf7, 0x19, 0x2b, 0xa7, 0xff, 0xb8, 0xca, 0xdc, 0x67, 0x51, 0xed, 0xa0, 0x08, 0x1d, 0x9d, 0x95,
                0x0b, 0x10, 0xe4, 0x32, 0x23, 0xef, 0x30, 0x07, 0x39, 0xc6, 0xbc, 0xad, 0x36, 0xda, 0x08, 0xeb,
                0x03, 0x3b, 0xab, 0x98, 0x32, 0x06, 0x7d, 0x39, 0x6f, 0x81, 0x72, 0x24, 0xff, 0x58, 0x41, 0xe6,
                0x33, 0x5d, 0xf7, 0xe7, 0x56, 0xf7, 0xaf, 0x32, 0xfa, 0xd8, 0x72, 0x78, 0xac, 0x63, 0xda, 0xd1
            };

            var _secret = new byte[] {
                0xb4, 0xe6, 0x04, 0x41, 0xf6, 0x2d, 0xc4, 0x1a, 0xa0, 0x36, 0x9e, 0x2a, 0xa0, 0xbd, 0x1c, 0xce,
                0x93, 0x1c, 0x8d, 0xb7, 0xb7, 0xaf, 0x11, 0x20, 0xba, 0x5e, 0x99, 0xfc, 0xff, 0xd6, 0xb1, 0x04,
                0x00, 0x55, 0x5b, 0xb0, 0x35, 0x80, 0x43, 0x2e, 0xbf, 0xc7, 0x10, 0x06, 0xe3, 0x04, 0x68, 0xe8,
                0x10, 0xa7, 0x95, 0xb5, 0xd1, 0x02, 0x84, 0x49, 0x4c, 0x22, 0x34, 0x05, 0x90, 0x48, 0x90, 0x4a
            };

            var _ad = new byte[] { 0x4b, 0x53, 0x7c, 0xa5, 0xe0, 0x2b, 0xe4, 0x06, 0xce, 0x9e, 0x9e, 0xa3, 0x27, 0x9c, 0x6e, 0x26 };

            Argon2id hashAlgo = new Argon2id(bytes);

            hashAlgo.AssociatedData = _ad;
            hashAlgo.DegreeOfParallelism = 16;
            hashAlgo.Iterations = 15;
            hashAlgo.KnownSecret = _secret;
            hashAlgo.MemorySize = 4096;
            hashAlgo.Salt = _salt;

            var hash = hashAlgo.GetBytes(512);

            string hashString = BitConverter.ToString(hash);

            return hashString;
        }
    }
}
