using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Orchid.Repo.Etcd
{
    public class EtcdRepositoryOptions
    {
        public string[] Urls { get; set; }
        public bool IgnoreCertificateError { get; set; }
        public X509Certificate X509Certificate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseProxy { get; set; }
    }
}
