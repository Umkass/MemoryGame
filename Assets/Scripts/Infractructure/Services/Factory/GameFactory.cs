using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using GameCore;
using Infractructure.AssetManagement;
using Infractructure.Services.StaticData;
using Infractructure.UIServices.Factory;
using StaticData.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace Infractructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;
        private readonly IUIFactory _uiFactory;
        private GameField _gameField;

        public GameFactory(IStaticDataService staticDataService, IAssetProvider assetProvider, IUIFactory uiFactory)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            _uiFactory = uiFactory;
        }

        public async Task<GameField> CreateGameField(int verticalSize, int horizontalSize)
        {
            GameObject gameFieldGo = await _assetProvider.Instantiate(AssetAddress.GameField, _uiFactory.UIRoot.transform);
            gameFieldGo.transform.SetAsLastSibling();
            _gameField = gameFieldGo.GetComponent<GameField>();
            RectTransform gameFieldRect = _gameField.GetComponent<RectTransform>();
            GridLayoutGroup gridLayoutGroup = _gameField.gridLayoutGroup;

            Vector2 cellSize = CalculateCellSize(verticalSize, horizontalSize, gridLayoutGroup.spacing);
            gridLayoutGroup.cellSize = cellSize;

            if (Consts.MaxRowsForSquareCards >= verticalSize)
            {
                gameFieldRect.sizeDelta = new Vector2(
                    (horizontalSize * cellSize.y) + (gridLayoutGroup.spacing.x * (horizontalSize - 1)),
                    Consts.GameFieldHeight);

                gridLayoutGroup.cellSize = new Vector2(cellSize.y, cellSize.y);
            }
            else
            {
                gameFieldRect.sizeDelta = new Vector2(Consts.GameFieldWidth, Consts.GameFieldHeight);
            }

            return _gameField;
        }

        public async Task<List<Card>> CreateCards(int quantity)
        {
            Transform parent = _gameField.gridLayoutGroup.transform;
            List<Card> cards = new List<Card>();
            CardsStaticData cardsStaticData = _staticDataService.GetCardsStaticData();
            List<CardData> cardsData = _staticDataService.GetRandomCards(quantity / 2);

            await _assetProvider.Load<GameObject>(cardsStaticData.PrefabReference);

            foreach (CardData cardData in cardsData)
            {
                GameObject cardGo = await _assetProvider.Instantiate(cardsStaticData.PrefabReference.AssetGUID, parent);
                GameObject cardPairGo =
                    await _assetProvider.Instantiate(cardsStaticData.PrefabReference.AssetGUID, parent);

                Card card = cardGo.GetComponent<Card>();
                Card cardPair = cardPairGo.GetComponent<Card>();

                card.Initialize(cardData.CardId, cardData.CardSprite);
                cardPair.Initialize(cardData.CardId, cardData.CardSprite);

                cards.Add(card);
                cards.Add(cardPair);
            }

            return cards;
        }
        
        public void Cleanup()
        {
            Object.Destroy(_gameField);
            _assetProvider.Cleanup();
        }

        private Vector2 CalculateCellSize(int verticalSize, int horizontalSize, Vector2 spacing)
        {
            float cellWidth = (Consts.GameFieldWidth - (spacing.x * (horizontalSize - 1))) / horizontalSize;
            float cellHeight = (Consts.GameFieldHeight - (spacing.y * (verticalSize - 1))) / verticalSize;

            return new Vector2(cellWidth, cellHeight);
        }
    }
}