using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Data.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        TaskDbContext _context;


        public TasksRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void CreateTask(Task task)
        {

            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public Task GetTask(Guid taskId)
        {
            return _context.Tasks.Include(x => x.TeacherID).SingleOrDefault(x => x.Id == taskId);
        }

        public IQueryable<Task> GetTasksList(Guid teacherId)
        {
            var list = _context.Tasks.Where(x => x.TeacherID == teacherId);
            return list;
        }
    }
}
