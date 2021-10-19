using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;

namespace VehicleNetBackend
{
    public class MyControllerBase : ControllerBase
    {
        [Inject]
        protected DbCtx _context { get; set; }

        protected Task<User> GetLoginUser() => Task.FromResult(_context.UserService.User);

        protected ErrorResult GetErrorResult(string error)
        {
            return new ErrorResult(error);
        }

        protected ErrorResult<T> GetErrorResult<T>(string error, T data)
        {
            return new ErrorResult<T>(error, data);
        }
    }

    public class ErrorResult<T> : ErrorResult
    {
        public ErrorResult(string error, T data) : base(new { error, data })
        {
            this.StatusCode = 450;
            this.Data = data;
        }

        public T Data { get; }
    }


    public class ErrorResult : JsonResult
    {
        public ErrorResult(string error) : this(new { error })
        {
        }

        internal ErrorResult(object value) : base(value)
        {
            this.StatusCode = 450;
        }
    }
}