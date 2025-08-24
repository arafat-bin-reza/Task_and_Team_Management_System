using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Users
{
    public class GetAllUsersQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IList<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }
    }

}
