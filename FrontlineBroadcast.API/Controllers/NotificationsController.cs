using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrontlineBroadcast.API.Data;
using FrontlineBroadcast.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontlineBroadcast.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(ApplicationDbContext context, ILogger<NotificationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            _logger.LogInformation("Retrieved all notifications");
            return await _context.Notifications.ToListAsync();
        }

        // GET: api/Notifications/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetActiveNotifications()
        {
            return await _context.Notifications
                .Where(n => n.ExpiresAt == null || n.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Created notification with ID {Id}", notification.Id);
            return CreatedAtAction(nameof(GetNotification), new { id = notification.Id }, notification);
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated notification with ID {Id}", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted notification with ID {Id}", id);
            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}