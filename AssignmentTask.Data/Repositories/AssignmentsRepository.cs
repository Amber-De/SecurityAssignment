﻿using AssignmentTask.Data.Context;
using AssignmentTask.Domain.Interfaces;
using AssignmentTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssignmentTask.Data.Repositories
{
    public class AssignmentsRepository : IAssignmentsRepository
    {
        TaskDbContext _context;
        public AssignmentsRepository(TaskDbContext context)
        {
            _context = context;
        }

        public Guid AddAssignment(Assignment assignment)
        {
            assignment.Id = Guid.NewGuid();
            _context.Assignments.Add(assignment);
            _context.SaveChanges();

            return assignment.Id;
        }
    }
}
