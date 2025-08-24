using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Users
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
    }

}
