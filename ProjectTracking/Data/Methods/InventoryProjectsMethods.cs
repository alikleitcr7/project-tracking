using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{


    public class InventoryProjectsMethods : IInventoryProjectsMethods
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;

        public InventoryProjectsMethods(IMapper mapper, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
        }

        public InventoryProject Add(InventoryProject inventory)
        {
            try
            {
                inventory.DateAdded = DateTime.Now;

                if (!inventory.NeedsUpdate.HasValue)
                {
                    inventory.NeedsUpdate = true;
                }

                var record_pending = db.InventoryProject.Add(_mapper.Map<DataSets.InventoryProject>(inventory));

                if (db.SaveChanges() > 0)
                {
                    return Get(record_pending.Entity.ID);

                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            var record = db.InventoryProject.FirstOrDefault(k => k.ID == id);

            if (record != null)
            {
                db.InventoryProject.Remove(record);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public InventoryProject Update(InventoryProject inventory)
        {
            var record = db.InventoryProject
                           .Include(k => k.PublishingChannels)
                           .Include(k => k.InventorySubProjects)
                           .FirstOrDefault(k => k.ID == inventory.ID);

            if (record != null)
            {
                record.Title = inventory.Title;
                record.Description = inventory.Description;
                record.DatePublished = inventory.DatePublished;
                record.NeedsUpdate = inventory.NeedsUpdate;

                record.InventoryTypeId = inventory.InventoryTypeId;
                record.InventoryStatusId = inventory.InventoryStatusId;
                record.CountryId = inventory.CountryId;
                record.UpdateFrequencyId = inventory.UpdateFrequencyId;
                //record.PublishingChannelId = inventory.PublishingChannelId;

                if (record.PublishingChannels == null)
                {
                    record.PublishingChannels = new List<DataSets.InventoryProjectPublishingChannel>();
                }

                record.PublishingChannels.Clear();


                if (record.InventorySubProjects == null)
                {
                    record.InventorySubProjects = new List<DataSets.InventoryProjectSubProjects>();
                }

                record.InventorySubProjects.Clear();


                record.PersonResponsibleId = inventory.PersonResponsibleId;
                record.SecondaryPersonResponsibleId = inventory.SecondaryPersonResponsibleId;


                record.DateModified = DateTime.Now;



                if (db.SaveChanges() > 0)
                {
                    record.PublishingChannels = inventory.PublishingChannels == null ? new List<DataSets.InventoryProjectPublishingChannel>() :
                                            inventory.PublishingChannels.Select(k => new DataSets.InventoryProjectPublishingChannel()
                                            {
                                                InventoryProjectId = inventory.ID,
                                                PublishingChannelId = k.PublishingChannelId
                                            }).ToList();

                    record.InventorySubProjects = inventory.InventorySubProjects == null ? new List<DataSets.InventoryProjectSubProjects>() :
                                           inventory.InventorySubProjects.Select(k => new DataSets.InventoryProjectSubProjects()
                                           {
                                               InventoryProjectId = inventory.ID,
                                               InventorySubProjectId = k.InventorySubProjectId
                                           }).ToList();
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                    }


                    return Get(record.ID);
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public InventoryProject Get(int id)
        {
            var record = db.InventoryProject
                .Include(k => k.PublishingChannels)
                .ThenInclude(c => c.PublishingChannel)
                .Include(k => k.InventorySubProjects)
                .ThenInclude(c => c.InventorySubProject)
                .Include(k => k.InventoryType)
                .Include(k => k.InventoryStatus)
                .Include(k => k.Country)
                .Include(k => k.UpdateFrequency)
                .Include(k => k.PersonResponsible)
                .Include(k => k.SecondaryPersonResponsible)
                .FirstOrDefault(k => k.ID == id);

            if (record != null)
            {
                return _mapper.Map<InventoryProject>(record);
            }

            return null;
        }

        public List<InventoryProject> GetAll(int page, int countPerPage, out int totalCount)
        {

            totalCount = db.InventoryProject.Count();

            var records = db.InventoryProject
                .OrderByDescending(k => k.ID)
                .Include(k => k.PublishingChannels)
                .ThenInclude(c => c.PublishingChannel)
                .Include(k => k.InventorySubProjects)
                .ThenInclude(c => c.InventorySubProject)
                .Include(k => k.InventoryType)
                .Include(k => k.InventoryStatus)
                .Include(k => k.Country)
                .Include(k => k.UpdateFrequency)
                .Include(k => k.PersonResponsible)
                .Include(k => k.SecondaryPersonResponsible)

                .ToList();

            if (records != null)
            {
                return records.Skip(page * countPerPage)
                              .Take(countPerPage)
                              .Select(k => _mapper.Map<InventoryProject>(k))
                              .ToList();
            }

            return new List<InventoryProject>();
        }

        public List<InventoryProject> Search(InventoryProjectFilter filter, out int totalCount)
        {
            totalCount = 0;

            Func<object, bool> hasVal = delegate (object obj)
             {
                 return obj == null ? false : !string.IsNullOrEmpty(obj.ToString());
             };

            Func<DateTime?, DateTime?, bool> datesAreBetween = delegate (DateTime? fromDate, DateTime? toDate)
             {
                 return fromDate.HasValue && toDate.HasValue;
             };

            IQueryable<DataSets.InventoryProject> records = db.InventoryProject.Include(k => k.InventoryType)
                .Include(k => k.InventoryStatus)
                .Include(k => k.Country)
                .Include(k => k.UpdateFrequency)
                .Include(k => k.PublishingChannels)
                .ThenInclude(c => c.PublishingChannel)
                .Include(k => k.InventorySubProjects)
                .ThenInclude(c => c.InventorySubProject)
                .Include(k => k.PersonResponsible)
                .Include(k => k.SecondaryPersonResponsible)
                .OrderByDescending(k => k.ID);

            if (hasVal(filter.title))
            {
                records = records.Where(k => k.Title != null && k.Title.Contains(filter.title));
            }

            #region DateModified

            if (datesAreBetween(filter.dateModifiedFrom, filter.dateModifiedTo))
            {
                records = records.Where(k =>

                    (k.DateModified.HasValue && k.DateModified.Value >= filter.dateModifiedFrom.Value) &&
                    (k.DateModified.Value <= filter.dateModifiedTo.Value)
                );
            }
            else if (filter.dateModifiedFrom.HasValue)
            {
                records = records.Where(k =>
                    (k.DateModified.HasValue && k.DateModified.Value >= filter.dateModifiedFrom.Value)
                );
            }
            else if (filter.dateModifiedTo.HasValue)
            {
                records = records.Where(k =>
                    (k.DateModified.HasValue && k.DateModified.Value <= filter.dateModifiedTo.Value)
                );
            }

            #endregion

            #region DatePublished

            if (datesAreBetween(filter.datePublishedFrom, filter.datePublishedTo))
            {
                records = records.Where(k =>

                    (k.DatePublished.HasValue && k.DatePublished.Value >= filter.datePublishedFrom.Value) &&
                    (k.DatePublished.Value <= filter.datePublishedTo.Value)
                );
            }
            else if (filter.datePublishedFrom.HasValue)
            {
                records = records.Where(k =>
                    (k.DatePublished.HasValue && k.DatePublished.Value >= filter.datePublishedFrom.Value)
                );
            }
            else if (filter.datePublishedTo.HasValue)
            {
                records = records.Where(k =>
                    (k.DatePublished.HasValue && k.DatePublished.Value <= filter.datePublishedTo.Value)
                );
            }

            #endregion

            if (filter.inventoryTypeId.HasValue)
            {
                records = records.Where(k => k.InventoryTypeId.HasValue && k.InventoryTypeId.Value == filter.inventoryTypeId.Value);
            }

            if (filter.inventoryStatusId.HasValue)
            {
                records = records.Where(k => k.InventoryStatusId.HasValue && k.InventoryStatusId.Value == filter.inventoryStatusId.Value);
            }

            if (filter.countryId.HasValue)
            {
                records = records.Where(k => k.CountryId.HasValue && k.CountryId.Value == filter.countryId.Value);
            }

            if (filter.updateFrequencyId.HasValue)
            {
                records = records.Where(k => k.UpdateFrequencyId.HasValue && k.UpdateFrequencyId.Value == filter.updateFrequencyId.Value);
            }

            if (filter.publishingChannelIds != null && filter.publishingChannelIds.Count > 0)
            {
                records = records.Where(k => k.PublishingChannels.Any(c => filter.publishingChannelIds.Contains(c.PublishingChannelId)));
            }

            if (filter.needsUpdate.HasValue)
            {
                records = records.Where(k => k.NeedsUpdate.HasValue && k.NeedsUpdate.Value == filter.needsUpdate.Value);
            }

            if (filter.subProjectIds != null && filter.subProjectIds.Count > 0)
            {
                records = records.Where(k => k.InventorySubProjects.Any(c => filter.subProjectIds.Contains(c.InventorySubProjectId)));
            }


            totalCount = records.Count();

            List<InventoryProject> recordsParsed = records
                .ToList()
                .Skip(filter.page * filter.countPerPage)
                .Take(filter.countPerPage)
                .Select(k => _mapper.Map<InventoryProject>(k)).ToList();

            return recordsParsed;
        }
    }
}
