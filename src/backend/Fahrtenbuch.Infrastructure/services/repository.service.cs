using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fahrtenbuch.Infrastructure.persistence.repositories;

namespace Fahrtenbuch.Infrastructure.services;

public class RepositoryService(IServiceProvider serviceProvider)
{
    private readonly Lazy<CarRepository> _lazyCarRepository = new(() => new(serviceProvider));
    private readonly Lazy<MileageRepository> _lazyMileageRepository = new(() => new(serviceProvider));
    public CarRepository CarRepository => _lazyCarRepository.Value;
    public MileageRepository MileageRepository => _lazyMileageRepository.Value;
}