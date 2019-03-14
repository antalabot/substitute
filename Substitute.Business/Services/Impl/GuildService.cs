using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Enum;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Data.Entities;
using Substitute.Domain.DataStore;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services.Impl
{
    public class GuildService : IGuildService
    {
        #region Private constants
        private const string CLASS_NAME = "GUILD";
        #endregion

        #region Private readonly variables
        private readonly IDiscordBotRestService _botService;
        private readonly ICache _cache;
        private readonly IContext _context;
        
        private readonly TimeSpan _getGuildRolesExpiration = TimeSpan.FromMinutes(5);
        #endregion

        public GuildService(IDiscordBotRestService botService, ICache cache, IContext context)
        {
            _botService = botService;
            _cache = cache;
            _context = context;
        }

        public async Task<IEnumerable<RoleDigestModel>> GetRoles(RoleFilterModel model)
        {
            IEnumerable<RoleDigestModel> list = await _cache.GetOrCreateAsync($"{CLASS_NAME}|GetRoles|{model.GuildId}",
                                                                              async entity =>
                                                                              {
                                                                                  entity.SlidingExpiration = _getGuildRolesExpiration;

                                                                                  IEnumerable<RoleModel> guild = await _botService.GetGuildRoles(model.GuildId);
                                                                                  IEnumerable<GuildRole> dbRoles = _context.Get<GuildRole>().Where(r => r.GuildId == model.GuildId);
                                                                                  return guild.GroupJoin(dbRoles, k => k.Id, k => k.Id, (rest, db) => new RoleDigestModel
                                                                                  {
                                                                                      AccessLevel = db.SingleOrDefault()?.AccessLevel ?? EAccessLevel.User,
                                                                                      Id = rest.Id,
                                                                                      Name = rest.Name
                                                                                  });
                                                                              });
            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                list = list.Where(r => r.Name.ToLower().Contains(model.Name.ToLower()));
            }

            if (model.AccessLevel != null)
            {
                list = list.Where(r => r.AccessLevel == model.AccessLevel.Value);
            }

            Func<RoleDigestModel, object> sortFieldSelector = r => r.Id;
            switch (model.SortBy)
            {
                case ERoleSort.AccessLevel:
                    sortFieldSelector = r => r.AccessLevel;
                    break;
                case ERoleSort.Id:
                    sortFieldSelector = r => r.Id;
                    break;
                case ERoleSort.Name:
                    sortFieldSelector = r => r.Name;
                    break;
            }

            switch (model.SortDirection)
            {
                case ESortDirection.Ascending:
                    list = list.OrderBy(sortFieldSelector);
                    break;
                case ESortDirection.Descending:
                    list = list.OrderByDescending(sortFieldSelector);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return model.PerPage == 0 ? list : list.Skip(model.PerPage * (model.Page - 1)).Take(model.PerPage);
        }
        
        public async Task SetRoleAccessLevel(RoleModel role)
        {
            if (role.AccessLevel == EAccessLevel.User)
            {
                if ((await _context.GetByIdAsync<GuildRole>(role.Id)) != null)
                {
                    await _context.RemoveByIdAsync<GuildRole>(role.Id);
                }
            }
            else
            {
                GuildRole model = await _context.GetByIdAsync<GuildRole>(role.Id);
                if (model == null)
                {
                    model = new GuildRole
                    {
                        GuildId = role.GuildId,
                        Id = role.Id,
                        Name = role.Name
                    };
                    await _context.AddAsync(model);
                }
                model.AccessLevel = role.AccessLevel;
            }
            _context.SaveChanges();
        }
    }
}
