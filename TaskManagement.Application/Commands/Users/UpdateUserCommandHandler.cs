using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Users
{
    public class UpdateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if (user == null) throw new Exception("User not found");

            if (!string.IsNullOrEmpty(command.Name)) user.FullName = command.Name;
            if (!string.IsNullOrEmpty(command.Email)) user.Email = command.Email;

            await _userRepository.UpdateAsync(user, cancellationToken);
        }
    }

}
