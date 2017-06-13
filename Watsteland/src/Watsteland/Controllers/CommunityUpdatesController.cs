using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Watsteland.Data;
using Watsteland.Models;

namespace Watsteland.Controllers
{
    public class CommunityUpdatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunityUpdatesController(ApplicationDbContext context)
        {
            _context = context;    
        }
    }
}
