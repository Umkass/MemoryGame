using System.Collections.Generic;
using System.Linq;
using Data;
using StaticData.GameSettings;
using StaticData.View;
using UnityEngine;

namespace Infractructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ViewId, ViewData> _viewsData;
        private DefaultGameSettingsData _defaultGameSettingsData;

        public void LoadAll()
        {
            LoadViews();
            LoadDefaultGameSettings();
        }

        private void LoadViews()
        {
            _viewsData = Resources
                .Load<ViewsStaticData>(FilePaths.ViewsDataPath)
                .ViewsData.ToDictionary(x => x.ViewId, x => x);
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

        public DefaultGameSettingsData GetDefaultGameSettings() =>
            _defaultGameSettingsData;
    }
}