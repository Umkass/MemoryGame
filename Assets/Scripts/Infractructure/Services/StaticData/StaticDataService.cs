using System.Collections.Generic;
using System.Linq;
using Data;
using StaticData.Cards;
using StaticData.GameSettings;
using StaticData.View;
using UnityEngine;

namespace Infractructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ViewId, ViewData> _viewsData;
        private Dictionary<CardId, CardData> _cardsData;
        private CardsStaticData _cardsStaticData;
        private DefaultGameSettingsData _defaultGameSettingsData;

        public void LoadAll()
        {
            LoadViews();
            LoadDefaultGameSettings();
            LoadCards();
        }

        private void LoadViews()
        {
            _viewsData = Resources
                .Load<ViewsStaticData>(FilePaths.ViewsDataPath)
                .ViewsData.ToDictionary(x => x.ViewId, x => x);
        }
        
        private void LoadCards()
        {
            _cardsStaticData = Resources.Load<CardsStaticData>(FilePaths.CardsDataPath);
            _cardsData = _cardsStaticData.CardsData.ToDictionary(x => x.CardId, x => x);
        }

        private void LoadDefaultGameSettings()
        {
            _defaultGameSettingsData = Resources
                .Load<DefaultGameSettingsData>(FilePaths.DefaultGameSettingsDataPath);
        }

        public ViewData GetView(ViewId viewId) =>
            _viewsData.TryGetValue(viewId, out ViewData viewData)
                ? viewData
                : null;
        
        public List<CardData> GetRandomCards(int quantity)
        {
            List<CardData> cardList = _cardsData.Values.ToList();
            Shuffle(cardList);
            List<CardData> randomCards = cardList.Take(quantity).ToList();
            foreach (var cardData in randomCards)
            {
                Debug.Log("VVV randomCardData " + cardData);
            }
            return randomCards;
        }
        
        public DefaultGameSettingsData GetDefaultGameSettings() =>
            _defaultGameSettingsData;

        public CardsStaticData GetCardsStaticData() =>
            _cardsStaticData;

        private void Shuffle<T>(List<T> list)
        {
            System.Random rnd = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}