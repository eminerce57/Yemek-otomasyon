using Dapper;
using CrudProject.Common;
using CrudProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace CrudProject.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyOrderController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public CompanyOrderController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetCompanyUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM ""order""  where is_okey = false";
                    var List = connection.Query<dynamic>(sql).ToList();

                    return Ok(List);
                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHelper.ExceptionResponse(ex.Message));
                }
                finally
                {
                    connection.Close();
                }
            }
        }



    }
}
