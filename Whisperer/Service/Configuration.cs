﻿using Whisperer.DependencyResolution;
using ConfigurationModel = Whisperer.Models.Configuration;

namespace Whisperer.Service
{
    public class Configuration
    {
        private IReadOnlyService<ConfigurationModel> _service;

        public Configuration()
        {
            _service = Ioc.Container.GetInstance<IReadOnlyService<ConfigurationModel>>();
        }

        public string GetIncomingToken()
        {
            return _service.Get(1).IncomingToken;
        }

        public string GetPayloadUrl()
        {
            return _service.Get(1).PayloadUrl;
        }

        public short GetDailyMeetingTime()
        {
            return _service.Get(1).DailyMeetingTime;
        }

        public short GetOfficeHoursBegin()
        {
            return _service.Get(1).OfficeHoursBegin;
        }

        public short GetOfficeHoursEnd()
        {
            return _service.Get(1).OfficeHoursEnd;
        }
    }
}