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
    public class OrderController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public OrderController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }




        [HttpPost("add")]
        public async Task<IActionResult> AddOrder(OrderModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {

                    model.is_okey = false;
                    model.order_date = DateTime.Now;
                    string sql = @"INSERT INTO order (company_id,order_date,is_okey) VALUES(@company_id,@order_date,false) RETURNING id";

                    int order_id = connection.Query(sql).FirstOrDefault();





                    return Ok();
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
