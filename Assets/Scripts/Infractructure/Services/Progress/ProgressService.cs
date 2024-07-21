using Data;

namespace Infractructure.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public bool IsLastGameWon { get; set; }
        public GameSettingsData GameSettingsData { get; set; }
    }
}