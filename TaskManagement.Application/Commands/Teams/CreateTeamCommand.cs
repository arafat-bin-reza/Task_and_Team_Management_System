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

namespace TaskManagement.Application.Commands.Teams
{
    public class CreateTeamCommand
    {
        public string Name { get; set; } = string.Empty;
    }

}
