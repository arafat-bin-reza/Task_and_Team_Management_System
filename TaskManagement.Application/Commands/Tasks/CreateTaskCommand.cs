using AutoMapper;
using MediatR;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.Tasks
{
    public class CreateTaskCommand
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
