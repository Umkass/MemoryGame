using System.Threading.Tasks;
using StaticData.View;
using UI.Views;

namespace Infractructure.UIServices.Factory
{
    public interface IUIFactory
    {
        Task CreateUIRoot();
        Task<ViewBase> CreateWindow(ViewId viewId);
    }
}