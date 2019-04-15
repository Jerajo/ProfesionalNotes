using System;
using System.IO;
using Audit.Core;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;

namespace PN.Services
{
    public class DataProvider : AuditDataProvider
    {
        public override object InsertEvent(AuditEvent auditEvent)
        {
            var fileName = GetFullPath($"Log{Guid.NewGuid()}.json");
            File.WriteAllText(fileName, auditEvent.ToJson());
            return fileName;
        }

        public override void ReplaceEvent(object eventId, AuditEvent auditEvent)
        {
            var fileName = GetFullPath(eventId.ToString());
            File.WriteAllText(fileName, auditEvent.ToJson());
        }

        public override T GetEvent<T>(object eventId)
        {
            var fileName = GetFullPath(eventId.ToString());
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }

        // async implementation:
        public override async Task<object> InsertEventAsync(AuditEvent auditEvent)
        {
            var fileName = GetFullPath($"Log{Guid.NewGuid()}.json");
            await Task.Run(() =>
            {
                File.WriteAllText(fileName, auditEvent.ToJson());
            });
            return fileName;
        }

        public override async Task ReplaceEventAsync(object eventId, AuditEvent auditEvent)
        {
            var fileName = GetFullPath(eventId.ToString());
            await Task.Run(() =>
            {
                File.WriteAllText(fileName, auditEvent.ToJson());
            });
        }

        public override async Task<T> GetEventAsync<T>(object eventId)
        {
            var fileName = GetFullPath(eventId.ToString());
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName)));
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/Audits/", fileName);
        }
    }
}