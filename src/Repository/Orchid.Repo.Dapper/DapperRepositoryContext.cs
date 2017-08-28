using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Utilities;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace Orchid.Repo.Dapper
{
    public class DapperRepositoryContext : RepositoryContext
    {
        #region | Fields |

        private readonly object _syncObj = new object();

        private IDbTransaction _transaction;

        #endregion

        #region | Properties |

        public IDbConnection Connection { get; private set; }

        #endregion

        #region | Ctor |

        public DapperRepositoryContext([NotNull]IDbConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            Connection = connection;
            Connection.Open();
            _transaction = Connection.BeginTransaction();
        }

        #endregion

        #region | Members of IRepositoryContext |

        public override void RegisterNew<T>(T value)
        {
            Connection.Insert(value, _transaction);
            IsCommited = false;
        }

        public override void RegisterModified<T>(T value)
        {
            Connection.Update(value, _transaction);
            IsCommited = false;
        }

        public override void RegisterDeleted<T>(T value)
        {
            Connection.Delete(value, _transaction);
            IsCommited = false;
        }

        public override void Commit()
        {
            if (IsCommited) return;
            try
            {
                lock (_syncObj)
                {
                    _transaction.Commit();
                    IsCommited = true;
                }
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                throw e;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = Connection.BeginTransaction();
            }
        }

        public override void Rollback()
        {
            IsCommited = false;
            _transaction.Rollback();
        }

        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposing) return;

            Connection?.Dispose();
            _transaction?.Dispose();

            this.Connection = null;
            this._transaction = null;
        }

        #endregion
    }
}
