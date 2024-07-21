using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using NotebookContext.Model;
using Microsoft.AspNetCore.Authorization;
using Notebook.Data.Interfaces;

namespace Notebook.Controllers
{
    public class ContactController : Controller
    {

        private readonly IContactData _contactData;

        public ContactController(IContactData contactData)
        {
            _contactData = contactData;
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            return View(await _contactData.GetContactsAsync());
        }

        // GET: Contact/Details/5
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactData.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,MiddleName,Phone,Address,Description")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactData.CreateContactAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contact/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactData.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,MiddleName,Phone,Address,Description")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactData.UpdateContactAsync(contact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contact/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactData.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _contactData.GetContactAsync(id);
            if (contact != null)
            {
                await _contactData.DeleteContactAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _contactData.ContactExists(id);
        }
    }
}
