using System;
using System.Linq;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;

namespace PN.Controllers.Helpers
{
    public static class Extensions
    {
        public static EntityKey GetEntityKey(this DbContext context, object entity)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            var setName = getObjectSetName(objectContext, entity.GetType());
            return objectContext.CreateEntityKey(setName, entity);
        }

        private static string getObjectSetName(ObjectContext oc, Type entityType)
        {
            var createObjectSetMethodInfo = typeof(ObjectContext)
                .GetMethods()
                .Single(i => i.Name == "CreateObjectSet" && !i.GetParameters().Any());

            var objectSetType = Assembly.GetAssembly(typeof(ObjectContext))
                .GetTypes()
                .Single(t => t.Name == "ObjectSet`1");

            var objectSet = createObjectSetMethodInfo.MakeGenericMethod(entityType).Invoke(oc, null);

            var pi = objectSetType.MakeGenericType(entityType).GetProperty("EntitySet");
            var entitySet = pi.GetValue(objectSet) as EntitySet;
            return entitySet.Name;
        }
    }
}