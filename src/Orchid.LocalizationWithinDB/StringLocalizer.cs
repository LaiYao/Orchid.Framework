using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Orchid.Core.Utilities;
using Orchid.LocalizationWithinDB.Entities;
using Orchid.LocalizationWithinDB.Repositories;
using Orchid.Repo.Abstractions;

namespace Orchid.LocalizationWithinDB
{
    public class StringLocalizer : IStringLocalizer
    {
        #region | Fields |

        private readonly IRepositoryFactory<IRepositoryContext> _repoFactory;

        #endregion

        public StringLocalizer(IRepositoryFactory<IRepositoryContext> repoFactory)
        {
            Check.NotNull(repoFactory, nameof(repoFactory));

            _repoFactory = repoFactory;
        }

        #region | Members of IStringLocalizer |

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var repoCulture = _repoFactory.Create<Culture>();
            var repoResource = _repoFactory.Create<Resource>();

            var culture = repoCulture.Find(_ => _.Name == CultureInfo.CurrentCulture.Name).FirstOrDefault();

            if (culture == null) return null;

            return repoResource
                .Find(_ => _.Culture.Id == culture.Id)
                .Select(_ => new LocalizedString(_.Key, _.Value, true));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new StringLocalizer(_repoFactory);
        }

        #endregion

        #region | Helper |

        private string GetString(string key)
        {
            var repoCulture = _repoFactory.Create<Culture>();
            var repoResource = _repoFactory.Create<Resource>();

            var culture = repoCulture.Find(_ => _.Name == CultureInfo.CurrentCulture.Name).FirstOrDefault();

            if (culture == null) return null;

            return repoResource
                .Find(_ => _.Culture.Id == culture.Id && _.Key == key)
                .FirstOrDefault()?.Value;
        }

        #endregion
    }

    public class StringLocalizer<T> : StringLocalizer, IStringLocalizer<T>
    {
        public StringLocalizer(IRepositoryFactory<IRepositoryContext> repoFactory) : base(repoFactory)
        {
        }
    }
}
