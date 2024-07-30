using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class ImageUrlRepository : GenericRepository<ImageUrl>, IImageUrlRepository
    {
        public ImageUrlRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<ImageUrl>> GetImageUrlsBySchoolId(int schoolId)
        {
            return await _context.ImageUrls
                .Include(ed => ed.Violation)
                    .ThenInclude(e => e.User)
                .Where(ed => ed.Violation.User.SchoolId == schoolId)
                .ToListAsync();
        }

        // get all image urls by violation id and delete them
        public async Task DeleteImagesByViolationId(int violationId)
        {
            var imageUrls = await _context.ImageUrls
                .Where(ed => ed.ViolationId == violationId)
                .ToListAsync();

            _context.ImageUrls.RemoveRange(imageUrls);
            await _context.SaveChangesAsync();
        }
    }
}
