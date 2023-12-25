﻿using Application.Interfaces;
using AutoMapper;
using Domain.CalculationProbability;
using Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context)
        {
            _context = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, User>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return _mapper.Map<List<User>>(_context.Users.ToList());
        }

        public async Task<User> Login(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            return _mapper.Map<User>(user);
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(_mapper.Map<UserEntity>(user));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserById(string? id)
        {
            UserEntity user = (from users in _context.Users
                               where users.Id.ToString() == id
                                               select users).FirstOrDefault();
            List<CalculationEntity> calculation = (from calculations in _context.Calculations
                                               where calculations.UserId.ToString() == id
                                               select calculations).ToList();

            _context.Users.Remove(user);
            _context.Calculations.RemoveRange(calculation);
            _context.SaveChanges();
        }
    }
}
