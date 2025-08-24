using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Users
{
    public class GetAllUsersQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
