using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Controllers
{
    public class PublishingChannelsController : Controller
    {
        private readonly IPublishingChannelsMethods _publishingChannelMethods;
        private readonly IMapper _mapper;

        public PublishingChannelsController(IPublishingChannelsMethods countriesMethods, IMapper mapper)
        {
            _publishingChannelMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.PublishingChannel Create(string name)
        {
            var dbPublishingChannel = _publishingChannelMethods.Create(new Data.DataSets.PublishingChannel()
            {
                Name = name
            });

            if (dbPublishingChannel != null)
            {
                return _mapper.Map<DataContract.PublishingChannel>(dbPublishingChannel);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbPublishingChannel = _publishingChannelMethods.GetByID(id);

            if (dbPublishingChannel != null)
            {
                return _publishingChannelMethods.Delete(dbPublishingChannel);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            var dbPublishingChannel = _publishingChannelMethods.GetByID(id);

            if (dbPublishingChannel != null)
            {
                dbPublishingChannel.Name = name;
                return _publishingChannelMethods.Edit(dbPublishingChannel);
            }

            return false;
        }

        public List<DataContract.PublishingChannel> GetAll()
        {
            var dbPublishingChannels = _publishingChannelMethods.Filter().ToList();

            if (dbPublishingChannels != null)
            {
                return dbPublishingChannels.Select(k => _mapper.Map<DataContract.PublishingChannel>(k)).ToList();
            }

            return new List<DataContract.PublishingChannel>();
        }
    }
}