using Data;
using UI;

namespace Infractructure.StateMachine
{
    public class MenuState : IDefaultState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _loadingCurtain;

        public MenuState(ISceneLoader sceneLoader, ICurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(SceneNames.Game, OnLoaded);
        }

        private void OnLoaded() => 
            _loadingCurtain.Hide();

        public void Exit()
        {
        }
    }
}