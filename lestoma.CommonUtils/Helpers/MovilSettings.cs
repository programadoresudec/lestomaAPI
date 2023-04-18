using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace lestoma.CommonUtils.Helpers
{
    public static class MovilSettings
    {
        private const string _token = "token";
        private const string _estadoComponente = "estadoComponente";
        private const string _macBluetooth = "mac";
        private const string _isLogin = "isLogin";
        private const string _isOnNotificationsEmail = "isOn";
        private const string _isOnMigrationSucess= "isOnSyncToDevice";
        private const string _connectionString = "ConnectionStringBD";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }
        public static string GetConnectionString
        {
            get => AppSettings.GetValueOrDefault(_connectionString, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_connectionString, value);
        }

        public static string MacBluetooth
        {
            get => AppSettings.GetValueOrDefault(_macBluetooth, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_macBluetooth, value);
        }

        public static bool IsOnNotificationsViaMail
        {
            get => AppSettings.GetValueOrDefault(_isOnNotificationsEmail, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isOnNotificationsEmail, value);
        }

        public static bool IsOnSyncToDevice
        {
            get => AppSettings.GetValueOrDefault(_isOnMigrationSucess, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isOnMigrationSucess, value);
        }

        public static string EstadoComponente
        {
            get => AppSettings.GetValueOrDefault(_estadoComponente, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_estadoComponente, value);
        }
        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
    }
}
