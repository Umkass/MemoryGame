using System.Collections.Generic;
using StaticData.Cards;
using StaticData.GameSettings;
using StaticData.View;

namespace Infractructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ViewData GetView(ViewId viewId);
        List<CardData> GetRandomCards(int quantity);
        CardsStaticData GetCardsStaticData();
        DefaultGameSettingsData GetDefaultGameSettings();
    }
}