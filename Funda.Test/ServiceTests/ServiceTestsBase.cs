using AutoMapper;
using Funda.Business.Models;
using Funda.Business.Profiles;
using Microsoft.Extensions.Options;

namespace Funda.Test.ServiceTests
{
    public class ServiceTestsBase
    {
        protected IOptions<APIPaggingConfig> _apiPaggingConfig;
        protected readonly IMapper _mapper;

        public ServiceTestsBase()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MakelaarServiceProfile());
            }).CreateMapper();

            _apiPaggingConfig = Options.Create(new APIPaggingConfig
            {
                PageNumber = 1,
                PageSize = 25
            });
        }
    }
}