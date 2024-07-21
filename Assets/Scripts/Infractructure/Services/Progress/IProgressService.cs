using Data;

namespace Infractructure.Services.Progress
{
    public interface IProgressService
    {
        bool IsLastGameWon { get; set; }
        GameSettingsData GameSettingsData { get; set; }
    }
}