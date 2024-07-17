using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infractructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public void Initialize() =>
            Addressables.InitializeAsync();

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (TryGetCached(assetReference.AssetGUID, out AsyncOperationHandle cachedHandle))
                return cachedHandle.Result as T;

            return await RunWithCache(
                Addressables.LoadAssetAsync<T>(assetReference),
                cacheKey: assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (TryGetCached(address, out AsyncOperationHandle cachedHandle)) return cachedHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            return await RunWithCache(
                Addressables.LoadAssetAsync<T>(address),
                cacheKey: address);
        }

        public Task<GameObject> Instantiate(string address) =>
            Addressables.InstantiateAsync(address).Task;

        public Task<GameObject> Instantiate(string address, Vector3 at) =>
            Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;

        public Task<GameObject> Instantiate(string address, Transform parent) =>
            Addressables.InstantiateAsync(address, parent).Task;

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles)
                Addressables.Release(handle);

            _handles.Clear();
        }

        private bool TryGetCached(string address, out AsyncOperationHandle cachedHandle)
        {
            if (_handles.TryGetValue(address, out List<AsyncOperationHandle> handles))
            {
                foreach (var handle in handles)
                {
                    if (handle.IsDone)
                    {
                        cachedHandle = handle;
                        return true;
                    }
                }
            }

            cachedHandle = default;
            return false;
        }

        private async Task<T> RunWithCache<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }
    }
}