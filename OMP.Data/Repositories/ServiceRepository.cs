using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OMP.Domain.Repositories;
using Orchid.Repo.Etcd;
using OMP.Domain.Entities;
using Orchid.Core.Utilities;

namespace OMP.Data.Repositories
{
    public class ServiceRepository : EtcdRepository<Service>, IServiceRepository
    {
        const string ETCD_MAPPING_PATH = "/omp/services";

        public ServiceRepository(EtcdRepositoryOptions options) : base(ETCD_MAPPING_PATH, options)
        {
        }

        public IEnumerable<Service> GetServicesViaGroup(string groupName)
        {
            Check.NotEmpty(groupName, nameof(groupName));
            return Find(_ => _.GroupName == groupName);
        }

        public IEnumerable<Service> GetServicesViaLabels(string labels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Service> GetServicesViaNamespace(string namespaceName)
        {
            Check.NotEmpty(namespaceName, nameof(namespaceName));
            return Find(_ => _.NamespaceName == namespaceName);
        }

        public Service GetServiceViaName(string serviceName)
        {
            Check.NotEmpty(serviceName, nameof(serviceName));
            return Find(_ => _.Id == serviceName).SingleOrDefault();
        }

        public Service GetServiceViaContractName(string serviceName)
        {
            Check.NotEmpty(serviceName, nameof(serviceName));
            return Find(_ => _.Id == serviceName).SingleOrDefault();
        }
    }
}