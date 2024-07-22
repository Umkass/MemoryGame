using Infractructure.UIServices.ViewService;
using UnityEngine;

namespace UI.Views
{
    public abstract class ViewBase : MonoBehaviour
    {
        private IViewService _viewService;

        public virtual void Construct(IViewService viewService) =>
            _viewService = viewService;

        private void Awake() =>
            OnAwake();

        private void OnDestroy() =>
            Cleanup();

        protected virtual void OnAwake()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}