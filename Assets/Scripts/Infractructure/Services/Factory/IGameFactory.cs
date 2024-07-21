using System.Collections.Generic;
using System.Threading.Tasks;
using GameCore;

namespace Infractructure.Services.Factory
{
    public interface IGameFactory
    {
        Task<GameField> CreateGameField(int verticalSize, int horizontalSize);
        Task<List<Card>> CreateCards(int quantity);
        void Cleanup();
    }
}