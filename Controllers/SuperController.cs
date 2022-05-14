using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Providers;
using System.Linq;
using Models;
using System;
using Microsoft.AspNetCore.JsonPatch;
namespace Controllers
{
    public class SuperController<T> : ControllerBase where T : class
    {
        protected readonly MyContext _context;

        public SuperController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{startIndex}/{pageSize}/{sortBy}/{sortDir}")]
        public virtual async Task<IActionResult> GetAll(int startIndex, int pageSize, string sortBy, string sortDir)
        {
            var list = await _context.Set<T>()
                .OrderByName<T>(sortBy, sortDir == "desc")
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync()
                ;
            int count = await _context.Set<T>().CountAsync();

            return Ok(new { list = list, count = count });
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var list = await _context.Set<T>()/*.OrderByName<T>("Id")*/.ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);

            return Ok(model);
        }

        // [HttpGet("{propertyName}/{id}")]
        // public virtual async Task<IActionResult> GetByForeignkey(string propertyName, int id)
        // {
        //      int i = typeof(T).FullName.LastIndexOf('.');
        //     string tableName = typeof(T).FullName.Substring(i + 1) + "s";


        //     var list = await _context.Set<T>().FromSqlInterpolated($"SELECT * From {tableName} Where {propertyName} = {id}").ToListAsync();

        //     return Ok(list);
        // }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T model)
        {
            await _context.Set<T>().AddAsync(model);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddRange(List<T> models)
        {
            

            await _context.Set<T>().AddRangeAsync(models);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(models);
        }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] T model)
        {
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<T> patchDoc)
        {
            var model = await _context.Set<T>().FindAsync(id);

            patchDoc.ApplyTo(model);

            _context.Entry(model).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        // [Authorize(Roles = "ADMIN, SHOP, APPROVING_SHOP")]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var model = await _context.Set<T>().FindAsync(id);
            if (model == null)
            {
                return Ok(false);
            }

            _context.Set<T>().Remove(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(true);
        }

        // [HttpGet("{id}/{lang}")]
        // public virtual async Task<ActionResult> ChangeState(int id, string lang)
        // {
        //     T model = await _context.Set<T>().FindAsync(id);

        //     if (model == null)
        //     {
        //         return BadRequest("This thing doent existe");
        //     }

        //     // Type type = typeof(T);

        //     PropertyInfo prop = model.GetType().GetProperty("IsActive");

        //     // if (prop == null)
        //     // {
        //     //     prop = model.GetType().GetProperty("UserStatus");
        //     // }

        //     bool isTrue = (bool)prop.GetValue(model, null);

        //     prop.SetValue(model, !isTrue);


        //     try
        //     {
        //         await _context.SaveChangesAsync();

        //         // send email
        //     }
        //     catch (DbUpdateConcurrencyException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }

        //     return Ok();
        // }

        [HttpGet]
        public async Task<ActionResult<int>> Count()
        {
            return await _context.Set<T>().CountAsync();
        }

        [HttpGet("{column}/{name}")]
        public virtual async Task<ActionResult<IEnumerable<T>>> Autocomplete([FromRoute] string column,[FromRoute] string name)
        {
            int i = typeof(T).FullName.LastIndexOf('.');
            string tableName = typeof(T).FullName.Substring(i + 1) + "s";

            return await _context.Set<T>()
                .FromSqlRaw(String.Format(@"SELECT * FROM {0} where {1} LIKE '%{2}%'", tableName, column, name))
                .Take(10)
                .ToListAsync();
        }

        [HttpPost]
        public virtual async Task<IActionResult> UpdateRange(List<T> models)
        {
            if (models.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            _context.Set<T>().UpdateRange(models);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteRange(List<T> models)
        {
            if (models.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            _context.Set<T>().RemoveRange(models);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteRangeByIds(List<int> ids)
        {
            if (ids.Count == 0)
            {
                return Ok(new { message = "count = 0" });
            }

            // var l =  ids.Select(model => (int)model.GetType().GetProperty("Id").GetValue(model, null)).ToList();
            var l =  ids.Select(id => _context.Set<T>().Find(id)).ToList();

            // string sql = "DELETE FROM Cats where Id = @Id";
            // await _context.Database.ExecuteSqlRawAsync("sql, new SqlParameter("@Id", id")); 

            _context.Set<T>().RemoveRange(l);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        // T GetInstance<T>() where T : new()
        // {
        //     T instance = new T();
        //     return instance;
        // }
    }
}