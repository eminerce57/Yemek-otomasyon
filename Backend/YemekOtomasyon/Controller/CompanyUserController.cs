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
    public class CompanyUserController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public CompanyUserController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyUser(int id)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM company_users where is_active = true AND company_id=@company_id";

                    var List = connection.Query<dynamic>(sql,new { company_id = id}).ToList();

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


        [HttpPost("add")]
        public async Task<IActionResult> AddCompanyUser(CompanyUsersModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    model.password = Helper.ComputeMD5Hash(model.password);
                    model.is_active = true;
                    model.created_at = DateTime.Now;
                    string sql = @"INSERT INTO company_users (company_id,name,surname,username,password,token,created_at,is_admin,is_active) VALUES(@company_id,@name,@surname,@username,@password,@token,@created_at,@is_admin,@is_active)";
                    connection.Execute(sql, model);


                    return Ok("Kayıt Başarıyla Eklendi");
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



        [HttpPost("update")]
        public async Task<IActionResult> UpdateCompanyUser(CompanyUsersModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"UPDATE company_users 
                           SET company_id=@company_id, name=@name, surname=@surname, username=@username, is_active=@is_active 
                           WHERE id=@id";
                    connection.Execute(sql, model);

                    return Ok("Kayıt Başarıyla Güncellendi");
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

    

    [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCompanyUser(int id)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {

                    string sql = @"UPDATE company_users SET is_active=false WHERE id=@id";
                    connection.Execute(sql, new {id=id});


                    return Ok("Kayıt Başarıyla Güncelllendi");
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
