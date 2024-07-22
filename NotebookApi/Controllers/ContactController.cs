using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using NotebookContext.Model;

namespace Notebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly NotebookContext.NotebookContext _context;

        public ContactController(NotebookContext.NotebookContext context)
        {
            _context = context;
        }

        // GET: Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return await _context.Contact.ToListAsync();
        }

        // GET: Contact/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            if (id == null)
            {
                return null;
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return null;
            }

            return contact;
        }

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<Contact>> Create([FromBody] Contact contact)
        {
            try
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return Ok(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: Contact/Put/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<ActionResult<Contact>> Put([Bind("Id,FirstName,LastName,MiddleName,Phone,Address,Description")] Contact contact)
        {
            try
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
                return Ok(contact);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // POST: Contact/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> Delete(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok(contact);
            }

            return BadRequest();
            
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
