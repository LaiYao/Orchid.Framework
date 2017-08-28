using Orchid.Repo.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using OMP.Domain.Entities;

namespace OMP.Domain.Repositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        IEnumerable<Service> GetServicesViaNamespace(string namespaceName);

        IEnumerable<Service> GetServicesViaLabels(string labels);

        Service GetServiceViaName(string serviceName);

        IEnumerable<Service> GetServicesViaGroup(string groupName);
    }
}
