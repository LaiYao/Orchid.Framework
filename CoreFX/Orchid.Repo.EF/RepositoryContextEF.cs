using System;
using Microsoft.Data.Entity;
using Orchid.Core.Utilities;

namespace Orchid.Repo.EF
{
    public class RepositoryContextEF : RepositoryContext
    {
        #region | Fields |

        private readonly object _syncObj = new object();

        #endregion

        #region | Properties |

        public DbContext Context { get; private set; }

        #endregion

        #region | Ctor |

        public RepositoryContextEF([NotNull]DbContext context)
        {
            Check.NotNull(context, nameof(context));

            Context = context;
        }

        #endregion

        #region | Members of IRepositoryContext |

        public override void RegisterNew<T>(T value)
        {
            Context.Set<T>().Add(value);
            IsCommited = false;
        }

        public override void RegisterModified<T>(T value)
        {
            Context.Set<T>().Update(value);
            IsCommited = false;
        }

        public override void RegisterDeleted<T>(T value)
        {
            Context.Set<T>().Remove(value);
            IsCommited = false;
        }

        public override void Commit()
        {
            if (IsCommited) return;
            try
            {
                lock (_syncObj)
                {
                    Context.SaveChanges();
                    Context.Database.CommitTransaction();
                }
                IsCommited = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override void Rollback()
        {
            IsCommited = false;
            Context.Database.RollbackTransaction();
        }

        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Context == null) return;

            Context.Dispose();

            this.Context = null;
        }

        #endregion
    }
}
