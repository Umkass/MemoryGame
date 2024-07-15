using UnityEngine;
using Zenject;

namespace Infractructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        private Game _game;

        [Inject]
        public void Construct(Game game) => 
            _game = game;

        private void Start() => 
            _game.Init(_coroutineRunner);
    }
}
