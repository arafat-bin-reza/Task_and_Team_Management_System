using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Queries.Tasks
{
    public class GetTaskByIdQuery
    {
        public int TaskId { get; set; }
    }
}
