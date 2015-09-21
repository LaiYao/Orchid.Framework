using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Framework.Internal;
using Orchid.Core.Utilities;

namespace Orchid.Repo.EF
{
    public class RepositoryContextEF : RepositoryContext
    {
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
            var autoDetect = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                Context.Set<T>().Add(value);

                IsCommited = false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = autoDetect;
            }
        }

        public override void RegisterModified<T>(T value)
        {
            var autoDetect = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                var entry = Context.Entry<T>(value);

                if (entry.State == EntityState.Detached)
                {
                    var entity = Context.Set<T>().FirstOrDefault(_ => _.Equals(value));
                    if (entity != null)
                    {
                        // TODO: EF7 hasnt implement such function, ref: https://github.com/aspnet/EntityFramework/issues/1999
                        //Context.Entry<T>(entity).SetValues(value);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }

                IsCommited = false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = autoDetect;
            }
        }

        public override void RegisterDeleted<T>(T value)
        {
            var autoDetect = Context.ChangeTracker.AutoDetectChangesEnabled;
            try
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = false;

                Context.Entry<T>(value).State = EntityState.Deleted;

                IsCommited = false;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = autoDetect;
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
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
