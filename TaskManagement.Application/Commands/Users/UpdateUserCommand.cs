using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Users
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

}
