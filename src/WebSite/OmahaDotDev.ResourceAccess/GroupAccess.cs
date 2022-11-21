﻿using Hero4Hire.Framework;
using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Model.Accessors.Group;
using OmahaDotDev.Model.Common;
using OmahaDotDev.Model.Common.Exceptions;
using OmahaDotDev.ResourceAccess.Database;
using OmahaDotDev.ResourceAccess.Database.Model;

namespace OmahaDotDev.ResourceAccess
{
    internal class GroupAccess : AccessorBase<AmbientContext>, IGroupAdminResourceAccess
    {
        private readonly SiteDbContext _dbContext;
        public GroupAccess(SiteDbContext dbContext)

        {
            _dbContext = dbContext;
        }

        public async Task<GroupResponse> CreateGroup(CreateGroupRequest request, CancellationToken cancellationToken)
        {
            var userIsSiteAdmin = await _dbContext.IsUserSiteAdmin(AmbientContext);
            if (!userIsSiteAdmin)
            {
                throw new ForbiddenException(AmbientContext, "Create Group");
            }

            var dbRecord = new GroupRecord(request.Name)
            {
                DomainNames = request.DomainNames.Select(dn => new GroupDomainNameRecord(dn)).ToList()
            };

            await _dbContext.Groups.AddAsync(dbRecord, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return ToGroupResponse(dbRecord);
        }

        public async Task<GroupResponse> FindGroupByDomainName(string domainName, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Groups
                .Where(p => p.DomainNames.Any(dn => dn.DomainName == domainName))
                .Include(p => p.DomainNames)
                .AsNoTracking()
                .FirstAsync(cancellationToken);

            return ToGroupResponse(result);
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroups(GetAllGroupsRequest request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Groups
                .Where(p => request.IncludeInactive || p.IsActive)
                .Include(p => p.DomainNames)
                .AsNoTracking()
                .Select(g => ToGroupResponse(g))
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<GroupResponse> GetGroup(int id, CancellationToken cancellationToken)
        {
            var record = await _dbContext.Groups.Include(g => g.DomainNames).FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            if (record == null)
            {
                throw new NotFoundException("Group", id);
            }

            return ToGroupResponse(record);
        }

        public async Task SetGroupStatus(SetGroupStatusRequest request, CancellationToken cancellationToken)
        {
            var record = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);

            if (record == null)
            {
                throw new NotFoundException("Group", request.GroupId);
            }

            record.IsActive = request.IsActive;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<GroupResponse> UpdateGroup(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            var record = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

            if (record == null)
            {
                throw new NotFoundException("Group", request.Id);
            }

            record.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ToGroupResponse(record);
        }

        private GroupResponse ToGroupResponse(GroupRecord record)
        {
            return new GroupResponse(record.Name, record.DomainNames.Select(dn => dn.DomainName))
            {
                Id = record.Id
            };
        }


    }
}
