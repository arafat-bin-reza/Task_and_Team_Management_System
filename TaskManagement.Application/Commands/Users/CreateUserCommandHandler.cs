using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Users
{
    public class CreateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = command.Name,
                Email = command.Email
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return user.Id;
        }
    }

}
