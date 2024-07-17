using System.Threading.Tasks;
using StaticData.View;
using UI.Views;
using UnityEngine;

namespace Infractructure.UIServices.Factory
{
    public interface IUIFactory
    {
        public Transform UIRoot { get; }
        Task CreateUIRoot();
        Task<ViewBase> CreateWindow(ViewId viewId);
    }
}