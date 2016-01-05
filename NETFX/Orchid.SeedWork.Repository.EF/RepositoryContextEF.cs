using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository.EF
{
    public class RepositoryContextEF : RepositoryContextBase
    {
        #region | Properties |

        public DbContext Context { get; private set; }

        #endregion

        #region | Ctor |

        public RepositoryContextEF(DbContext context)
        {
            if (context == null)
            {
                // TODO: log & warning
            }

            Context = context;
        }

        #endregion

        #region | Members of IRepositoryContext |

        public override void RegisterNew<T>(T value)
        {
            var autoDetect = Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;

                Context.Set<T>().Add(value);

                IsCommited = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = autoDetect;
            }
        }

        public override void RegisterModified<T>(T value)
        {
            var autoDetect = Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;

                var entry = Context.Entry<T>(value);

                if (entry.State == EntityState.Detached)
                {
                    var entity = Context.Set<T>().ToList().FirstOrDefault(_ => _.Equals(value));
                    if (entity != null)
                    {
                        Context.Entry<T>(entity).CurrentValues.SetValues(value);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }

                IsCommited = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = autoDetect;
            }
        }

        public override void RegisterDeleted<T>(T value)
        {
            var autoDetect = Context.Configuration.AutoDetectChangesEnabled;
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;

                Context.Entry<T>(value).State = EntityState.Deleted;

                IsCommited = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = autoDetect;
            }
        }

        public override void Commit()
        {
            if (IsCommited) return;
            try
            {
                Context.SaveChanges();
                IsCommited = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (Context == null) return;

            Context.Dispose();

            this.Context = null;
        }

        #endregion
    }
}
