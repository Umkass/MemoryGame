using System.Threading.Tasks;
using Infractructure.StateMachine;
using StaticData.View;
using UI.Views;

namespace Infractructure.UIServices.ViewService
{
    public interface IViewService
    {
        Task<ViewBase> Open(ViewId viewId);
        void Initialize(IGameStateMachine stateMachine);
    }
}