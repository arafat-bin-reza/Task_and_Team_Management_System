using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Users
{
    public class GetUserByEmailQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetByEmailAsync(query.Email, cancellationToken);
        }
    }

}
