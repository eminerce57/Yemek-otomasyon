﻿using Dapper;
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
    public class CompanyController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public CompanyController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }




        [HttpGet("")]
        public async Task<IActionResult> GetCompany()
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM company where is_active = true";

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


        [HttpPost("add")]
        public async Task<IActionResult> AddCompany(CompanyModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                   
                    model.is_active = true; 
                    string sql = @"INSERT INTO company (name,tax_no,is_active) VALUES(@name,@tax_no,@is_active)";
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
        public async Task<IActionResult> UpdateCompany(CompanyModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                  
                    string sql = @"UPDATE company SET name=@name, tax_no=@tax_no WHERE id=@id";
                    connection.Execute(sql, model);


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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteCompany(int id)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {

                    string sql = @"UPDATE company SET is_active=false WHERE id=@id";
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
