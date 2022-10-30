using Hero4Hire.Framework;
using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Model;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.Model.Common;
using OmahaDotDev.Model.Exceptions;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.ResourceAccess.Database.Entities;

namespace OmahaDotDev.ResourceAccess
{
    internal class GroupAccess : AccessorBase<AmbientContext>, IGroupAdminResourceAccess
    {
        private readonly SiteDbContext _dbContext;
        public GroupAccess(AmbientContext ambientContext, ServiceFactory<AmbientContext> serviceFactory, SiteDbContext dbContext)
            : base(ambientContext, serviceFactory)
        {
            _dbContext = dbContext;
        }

        public async Task<GroupResponse> CreateGroup(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var dbRecord = new GroupRecord(request.Name, request.DomainName);
            await _dbContext.Groups.AddAsync(dbRecord, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GroupResponse(dbRecord.Name, dbRecord.DomainName)
            {
                Id = dbRecord.Id
            };
        }

        public async Task DeleteGroup(DeleteGroupRequest request, CancellationToken cancellationToken)
        {
            var dbPost = await _dbContext.Groups.FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (dbPost == null)
            {
                throw new NotFoundException("Group", request.Id);
            }

            if (request.Perm)
            {
                _dbContext.Groups.Remove(dbPost);
            }
            else
            {
                dbPost.IsDeleted = true;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<SkipTakeSet<GroupResponse>> FindAllGroups(FindGroupRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Groups
                .Where(p => request.IncludeDeleted || !p.IsDeleted)
                .Where(p => string.IsNullOrWhiteSpace(request.Search) ||
                            EF.Functions.Like(p.Name, $"%{request.Search}%") ||
                            EF.Functions.Like(p.DomainName, $"%{request.Search}%"))
                .AsNoTracking()
                .Select(g => new GroupResponse(g.Name, g.DomainName)
                {
                    Id = g.Id
                })
                .AsSkipTakeSet(request.Skip, request.Take, cancellationToken);

            return result;
        }

        public async Task<GroupResponse> GetGroup(int id, CancellationToken cancellationToken)
        {
            var record = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            if (record == null)
            {
                throw new NotFoundException("Group", id);
            }

            return new GroupResponse(record.Name, record.DomainName)
            {
                Id = record.Id
            };
        }

        public async Task<GroupResponse> UpdateGroup(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            var record = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            if (record == null)
            {
                throw new NotFoundException("Group", request.Id);
            }

            record.Name = request.Name;
            record.DomainName = request.DomainName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GroupResponse(record.Name, record.DomainName)
            {
                Id = record.Id
            };
        }

    }
}
