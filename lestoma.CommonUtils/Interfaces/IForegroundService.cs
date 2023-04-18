namespace lestoma.CommonUtils.Interfaces
{
    public interface IForegroundService
    {
        void StartMyForegroundService();
        void StopMyForegroundService();
        bool IsForeGroundServiceRunning();
    }
}
