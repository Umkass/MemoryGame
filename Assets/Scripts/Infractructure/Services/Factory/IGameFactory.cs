using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using GameCore;

namespace Infractructure.Services.Factory
{
    public interface IGameFactory
    {
        Task<AudioManager> CreateAudioManager();
        Task<GameField> CreateGameField(int verticalSize, int horizontalSize);
        Task<List<Card>> CreateCards(int quantity);
        void Cleanup();
    }
}