using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responce
{
    public class LoginResponse
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public int Id { get; set; }
        public string Post { get; set; }


        public LoginResponse(string name, string token, int id, string post)
        {
            Name = name;
            Token = token;
            Id = id;
            Post = post;
        }
    }
}
