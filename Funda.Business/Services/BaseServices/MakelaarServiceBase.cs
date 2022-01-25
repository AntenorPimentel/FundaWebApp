using AutoMapper;
using Funda.Business.Models;
using Funda.Business.Profiles;
using Funda.Business.Validators;
using Funda.Data.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Funda.Business.Services.BaseServices
{
    public class MakelaarServiceBase
    {
        protected readonly ILogger _logger = Log.Logger;
        protected IMapper _mapper;

        public MakelaarServiceBase()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MakelaarServiceProfile());
            }).CreateMapper();
        }

        protected static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        protected static bool IsValidMakelaarsPersistence(MakelaarsPersistence makelaarsPersistence)
        {
            var validator = new MakelaarsPersistenceValidator().Validate(makelaarsPersistence);
            return validator.IsValid ? validator.IsValid : throw new ArgumentException(validator.ToString(", "));
        }
    }
}