using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAngular.Models;

namespace WebApplicationAngular.Controller
{
    [Route("api/Employee"), Produces("application/json")]
    [ApiController]
   
    public class EmployeeController : ControllerBase
    {
        public EmployeeContext _ctx;
        public EmployeeController(EmployeeContext context)
        {
            _ctx = context;
        }

        // GET: api/Employee/GetEmployee 
        //[Route("api/Employee/GetEmployee")]
        //[Route("api/Employee")]
        [HttpGet, Route("GetEmployee")]
        public async Task<object> GetEmployee()
        {
            List<EmployeeModel> employee = null;
            object result = null;
            try
            {
                using (_ctx)
                {
                    employee = await _ctx.Employees.ToListAsync();
                    result = new
                    {
                        employee
                    };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return employee;
        }

        // GET: api/Employee/GetEmployee
        //[HttpGet]
        //public IEnumerable<Employee> GetEmployee()
        //{
        //    return _ctx.Employees;
        //}


        // GET api/Employee/GetByID/5  

        //[HttpGet, Route("GetByID/{id}")]
        //public async Task<Employee> GetByID(int id)
        //{
        //    Employee employee = null;
        //    try
        //    {
        //        using (_ctx)
        //        {
        //            employee = await _ctx.Employees.FirstOrDefaultAsync(x => x.Id == id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return employee;
        //}

        // GET: api/Employee/5
        [HttpGet]
        [Route("GetEmployee/{Id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _ctx.Employees.SingleOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        //PUT: api/Employee
        [HttpPut]
        public async Task<object> Update([FromBody]EmployeeModel model)
        {

            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.Id > 0)
                        {
                            var entityUpdate = _ctx.Employees.FirstOrDefault(x => x.Id == model.Id);
                            if (entityUpdate != null)
                            {
                                entityUpdate.LastName = model.LastName;
                                entityUpdate.FirstName = model.FirstName;
                                entityUpdate.Address = model.Address;
                                entityUpdate.Qualification = model.Qualification;
                                entityUpdate.ContectNumber = model.ContectNumber;
                                await _ctx.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            var EmployeeModel = new EmployeeModel
                            {
                                LastName = model.LastName,
                                FirstName = model.FirstName,
                                Address = model.Address,
                                Qualification = model.Qualification,
                                ContectNumber = model.ContectNumber
                            };

                            _ctx.Employees.Add(EmployeeModel);
                            await _ctx.SaveChangesAsync();
                        }

                        _ctxTransaction.Commit();
                        message = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback();
                        e.ToString();
                        message = "Saved Error";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;

        }

        //POST api/Employee/Save
       [HttpPost]
        [Route("Save")]
        //public async Task<object> Save([FromBody]Employee model)
         public async Task<object> Save([FromBody]EmployeeModel model)
        {
            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.Id > 0)
                        {
                            var entityUpdate = _ctx.Employees.FirstOrDefault(x => x.Id == model.Id);
                            if (entityUpdate != null)
                            {
                                entityUpdate.LastName = model.LastName;
                                entityUpdate.FirstName = model.FirstName;
                                entityUpdate.Address = model.Address;
                                entityUpdate.Qualification = model.Qualification;
                                entityUpdate.ContectNumber = model.ContectNumber;
                                await _ctx.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            var EmployeeModel = new EmployeeModel
                            {
                                LastName = model.LastName,
                                FirstName = model.FirstName,
                                Address = model.Address,
                                Qualification = model.Qualification,
                                ContectNumber = model.ContectNumber
                            };

                            _ctx.Employees.Add(EmployeeModel);
                            await _ctx.SaveChangesAsync();
                        }

                        _ctxTransaction.Commit();
                        message = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback();
                        e.ToString();
                        message = "Saved Error";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

        //POST api/Employee
        //[HttpPost]
        //[Route("Save")]
        //public async Task<IActionResult> Save([FromBody] Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _ctx.Employees.Add(employee);
        //    await _ctx.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        //}

        // DELETE api/Employee/DeleteByID/5  
        [HttpDelete]
        [Route("DeleteByID/{Id}")]
        public async Task<object> DeleteByID(int id)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var idToRemove = _ctx.Employees.SingleOrDefault(x => x.Id == id);
                        if (idToRemove != null)
                        {
                            _ctx.Employees.Remove(idToRemove);
                            await _ctx.SaveChangesAsync();
                        }
                        _ctxTransaction.Commit();
                        message = "Deleted Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        message = "Error on Deleting!!";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }


        //[HttpDelete]
        //[Route("DeleteByID/{Id}")]
        //public async Task<IActionResult> DeleteByID([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var employee = await _ctx.Employees.SingleOrDefaultAsync(x => x.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _ctx.Employees.Remove(employee);
        //    await _ctx.SaveChangesAsync();

        //    return Ok(employee);
        //}




    }

   

   
}

