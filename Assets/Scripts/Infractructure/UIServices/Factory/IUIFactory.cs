using System.Threading.Tasks;
using StaticData.View;
using UI.Views;
using UnityEngine;

namespace Infractructure.UIServices.Factory
{
    public interface IUIFactory
    {
        Task WarmUp();
        public GameObject UIRoot { get; }
        Task CreateUIRoot();
        Task<ViewBase> CreateWindow(ViewId viewId);
        void Cleanup();
    }
}