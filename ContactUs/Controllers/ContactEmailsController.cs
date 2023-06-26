using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactUs.Data;
using ContactUs.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;
using RestSharp.Authenticators;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ContactUs.Controllers
{
    public class ContactEmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactEmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(ContactEmail body)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            //mail.From.Add(new MailboxAddress("Excited Admin", "furman63@ethereal.email"));
            mail.From.Add(new MailboxAddress("Excited Admin", "carlie.kihn@ethereal.email"));

            //mail.To.Add(new MailboxAddress("Excited User", "furman63@ethereal.email"));
            mail.To.Add(new MailboxAddress("Excited User", body.BusinessEmail));

            mail.Subject = body.Name + " Via Contact us";
            var emailbody = "Al Salam Alikum, We recived message from: " + body.Name + ", Feedback type: " + body.FeedBackType + ", Sender is: " + body.YouAre + ", Company is: " + body.Company + ", Phone number: " + body.PhoneNo.ToString() + ", Message text: " + body.Message + ". The message end. Best Regards.";

                
            mail.Body = new TextPart("plain")
            {
                Text = emailbody,
            };

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls) ;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("carlie.kihn@ethereal.email", "ZPgecNS12QK1A1Pq5x");

                client.Send(mail);
                client.Disconnect(true);
            }
            _context.Add(body);
            _context.SaveChanges();
            return Ok("Your message has arrived to us");
        }



        

        // GET: ContactEmails
        public async Task<IActionResult> Index()
        {
              return View(await _context.ContactEmails.ToListAsync());
        }

        // GET: ContactEmails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactEmails == null)
            {
                return NotFound();
            }

            var contactEmail = await _context.ContactEmails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEmail == null)
            {
                return NotFound();
            }

            return View(contactEmail);
        }

        // GET: ContactEmails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactEmails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeedBackType,YouAre,CreatedDate,Name,Company,BusinessEmail,PhoneNo,Message,TermsCondistion")] ContactEmail contactEmail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactEmail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactEmail);
        }

        // GET: ContactEmails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactEmails == null)
            {
                return NotFound();
            }

            var contactEmail = await _context.ContactEmails.FindAsync(id);
            if (contactEmail == null)
            {
                return NotFound();
            }
            return View(contactEmail);
        }

        // POST: ContactEmails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FeedBackType,YouAre,CreatedDate,Name,Company,BusinessEmail,PhoneNo,Message,TermsCondistion")] ContactEmail contactEmail)
        {
            if (id != contactEmail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactEmail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactEmailExists(contactEmail.Id))
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
            return View(contactEmail);
        }

        // GET: ContactEmails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactEmails == null)
            {
                return NotFound();
            }

            var contactEmail = await _context.ContactEmails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactEmail == null)
            {
                return NotFound();
            }

            return View(contactEmail);
        }

        // POST: ContactEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactEmails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ContactEmails'  is null.");
            }
            var contactEmail = await _context.ContactEmails.FindAsync(id);
            if (contactEmail != null)
            {
                _context.ContactEmails.Remove(contactEmail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactEmailExists(int id)
        {
          return _context.ContactEmails.Any(e => e.Id == id);
        }
    }
}
