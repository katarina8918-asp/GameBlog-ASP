﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBlog.Application.UseCases.DTO
{
    public class UseCaseLogsDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UseCaseName { get; set; }
        public string Data { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public bool IsAuthorized { get; set; }
    }
}
