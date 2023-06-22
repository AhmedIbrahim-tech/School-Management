﻿namespace Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<GenericBaseResponse<int>>
    {
        public int Id { get; set; }
        public DeleteStudentCommand(int id)
        {
            this.Id = id;
        }
    }
}
