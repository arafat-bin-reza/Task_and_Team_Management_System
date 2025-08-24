using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Teams
{
    public class GetTeamByIdQuery
    {
        public int TeamId { get; set; }
    }

}
