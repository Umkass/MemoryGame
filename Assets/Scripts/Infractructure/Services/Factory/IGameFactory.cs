using System.Collections.Generic;
using System.Threading.Tasks;
using Audio;
using Logic.GameCore;
using Logic.GameCore.Cards;

namespace Infractructure.Services.Factory
{
    public interface IGameFactory
    {
        Task WarmUp();
        Task<AudioManager> CreateAudioManager();
        Task<GameField> CreateGameField(int verticalSize, int horizontalSize);
        Task<List<Card>> CreateCards(int quantity);
        void Cleanup();
    }
}