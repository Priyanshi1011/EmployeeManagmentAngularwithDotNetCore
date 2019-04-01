using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAngular.Migrations;
using WebApplicationAngular.Models;

namespace WebApplicationAngular.Controller
{
    [Route("api/Department"), Produces("application/json")]
    [ApiController]
   
    public class DepartmentsController : ControllerBase
    {
        private readonly EmployeeContext _ctx;

        public DepartmentsController(EmployeeContext context)
        {
            _ctx = context;
        }

        //GET: api/Departments
        //[HttpGet, Route("GetDepartment")]
        //public IEnumerable<DepartmentModel> GetDepartment()
        //{
        //    return _ctx.Department;
        //}

        [HttpGet, Route("GetDepartment")]
        public async Task<object> GetDepartment()
        {
            List<DepartmentModel> department = null;
            object result = null;
            try
            {
                using (_ctx)
                {
                    department = await _ctx.Department.ToListAsync();
                    result = new
                    {
                        department
                    };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return department;
        }
        // GET: api/Departments/5
        [HttpGet]
        [Route("GetDepartment/{Id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _ctx.Department.SingleOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDepartment([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var department = await _ctx.Department.FindAsync(id);

        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(department);
        //}

        // PUT: api/Departments/5
        [HttpPut]
        public async Task<object> Update([FromBody]DepartmentModel model)
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
                            var entityUpdate = _ctx.Department.FirstOrDefault(x => x.Id == model.Id);
                            if (entityUpdate != null)
                            {
                                entityUpdate.DepartmentName = model.DepartmentName;
                              
                                await _ctx.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            var department = new DepartmentModel
                            {
                                DepartmentName = model.DepartmentName,
                              
                            };

                            _ctx.Department.Add(department);
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
            //return Ok(model);
            return result;

        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDepartment([FromRoute] int id, [FromBody] Department department)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != department.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _ctx.Entry(department).State = EntityState.Modified;

        //    try
        //    {
        //        await _ctx.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Departments
        [HttpPost]
        [Route("Save")]
        //public async Task<object> Save([FromBody]Employee model)
        public async Task<object> Save([FromBody]DepartmentModel model)
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
                        DepartmentModel department = new DepartmentModel();
                        if (model.Id > 0)
                        {
                            var entityUpdate = _ctx.Department.FirstOrDefault(x => x.Id == model.Id);
                            if (entityUpdate != null)
                            {
                                entityUpdate.DepartmentName = model.DepartmentName;
                               
                                await _ctx.SaveChangesAsync();
                              
                            }
                        }
                        else
                        {

                            department.DepartmentName = model.DepartmentName;

                          

                            _ctx.Department.Add(department);
                           
                            await _ctx.SaveChangesAsync();
                           
                        }

                        _ctxTransaction.Commit();
                        //return Ok(model);
                        //await GetDepartment();
                        //message = "Saved Successfully";
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
            //return Ok(model);
            return result;
        }
        //[HttpPost]
        //public async Task<IActionResult> Save([FromBody] Department department)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _ctx.Department.Add(department);
        //    await _ctx.SaveChangesAsync();

        //    return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        //}

        // DELETE: api/Departments/5
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
                        var idToRemove = _ctx.Department.SingleOrDefault(x => x.Id == id);
                        if (idToRemove != null)
                        {
                            _ctx.Department.Remove(idToRemove);
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


        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        var department = await _ctx.Department.FindAsync(id);
        //        if (department == null)
        //        {
        //            return NotFound();
        //        }

        //        _ctx.Department.Remove(department);
        //        await _ctx.SaveChangesAsync();

        //        return Ok(department);
        //    }

        //    private bool DepartmentExists(int id)
        //    {
        //        return _ctx.Department.Any(e => e.Id == id);
        //    }
        }
    }