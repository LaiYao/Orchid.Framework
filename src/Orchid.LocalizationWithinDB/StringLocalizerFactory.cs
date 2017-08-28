using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Orchid.Core.Utilities;
using Orchid.Repo.Abstractions;

namespace Orchid.LocalizationWithinDB
{
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        #region | Fields |

        private readonly IRepositoryFactory<IRepositoryContext> _repoFactory;

        #endregion

        public StringLocalizerFactory(IRepositoryFactory<IRepositoryContext> repoFactory)
        {
            Check.NotNull(repoFactory, nameof(repoFactory));

            _repoFactory = repoFactory;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new StringLocalizer(_repoFactory);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer(_repoFactory);
        }
    }
}
