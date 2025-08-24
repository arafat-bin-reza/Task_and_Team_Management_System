using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Users
{
    public class DeleteUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if (user == null) throw new Exception("User not found");

            await _userRepository.DeleteAsync(user, cancellationToken);
        }
    }

}
