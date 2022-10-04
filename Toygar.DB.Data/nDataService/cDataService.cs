using Bootstrapper.Boundary.nCore.nObjectLifeTime;
using Bootstrapper.Core.nAttributes;
using Bootstrapper.Core.nApplication;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

namespace Toygar.DB.Data.nDataService
{
    public class cDataService<TBaseEntity>: cBaseDataService<cDataServiceContext, TBaseEntity>
        where TBaseEntity : cBaseEntity
    {
        public cDataService()
            : base(new cDataServiceContext(cApp.App))
        {
        }

        public override void InvokeTransactionalAction(Func<bool> _ServiceMethod)
        {
            try
            {
                List<MethodBase> __Methods = App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

                if (__Methods.Where(__Item => __Item.DeclaringType.Name == this.GetType().Name).ToList().Count < 2)
                {
                    if (_ServiceMethod())
                    {
                        Database.DefaultConnection.Commit();
                    }
                    else
                    {
                        Database.DefaultConnection.Rollback();
                    }
                }
                else
                {
                    throw new Exception("ic ice aclilan transation mevcut..!");
                }
            }
            catch (Exception ex)
            {
                App.Loggers.SqlLogger.LogError(ex);
                Database.DefaultConnection.Rollback();
                throw;
            }
        }

    }
}
