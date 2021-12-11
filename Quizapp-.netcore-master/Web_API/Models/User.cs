using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace Web_API.Models
{
    public partial class User
    {
        //public User(string name, string email, string role)
        //{
        //    Name = name;
        //    Email = email;
        //    Role = role;
        //}
        public int Id { get; set; }
        public int? UserTypeId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
      
        public virtual UserType UserType { get; set; }

        public static string EncryptPassword(string Password, string KeyString)
        {
            byte[] input = Encoding.UTF8.GetBytes(Password);
            byte[] key = Encoding.UTF8.GetBytes(KeyString.PadRight(8, ' ').Substring(0, 8));
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                {
                    cs.Write(input, 0, input.Length);
                    cs.FlushFinalBlock();

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptPassword(string CryptedPassword, string KeyString)
        {
            byte[] Input = Convert.FromBase64String(CryptedPassword);
            byte[] key = Encoding.UTF8.GetBytes(KeyString.PadRight(8, ' ').Substring(0, 8));
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                try
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(Input, 0, Input.Length);
                        cs.FlushFinalBlock();

                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
                catch (CryptographicException ex)
                {
                    throw new Exception("Cryptographic error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("General error: " + ex.Message);
                }
            }
        }

        //public ICollection<AssignedUserTaskUnit> AssignedUserTaskUnits { get; set; }

    }
}
