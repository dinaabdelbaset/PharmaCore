using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaCore.ActivationManager
{
    internal sealed class DeviceMonitorRepository
    {
        private readonly string _connectionString;
        public DeviceMonitorRepository(string cs){ _connectionString = cs; }
        public Task<IReadOnlyList<BranchAuthDbLocal.DeviceInfo>> GetDevicesAsync(Guid orgId, Guid branchId) => BranchAuthDbLocal.GetDevicesAsync(_connectionString, orgId, branchId);
        public Task SetActiveAsync(Guid deviceId, bool active) => BranchAuthDbLocal.SetDeviceActiveAsync(_connectionString, deviceId, active);
        public Task HeartbeatAsync(Guid deviceId, int seconds) => BranchAuthDbLocal.UpdateHeartbeatAsync(_connectionString, deviceId, seconds);
        public Task EnsureSchemaAsync() => BranchAuthDbLocal.EnsureMonitoringSchemaAsync(_connectionString);
    }
}