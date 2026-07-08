using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fahrtenbuch.Application.dtos;

namespace Fahrtenbuch.Application.querys;

public record GetCarsQueryResult(IEnumerable<CarDto> Result);

