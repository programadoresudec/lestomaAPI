namespace lestoma.CommonUtils.Constants
{
    public class Constants
    {
        #region iconos de lestoma app
        public const string ICON_ERROR = "icon_fish_error.png";
        public const string ICON_INFO = "icon_info.png";
        public const string ICON_WARNING = "icon_warning.png";
        #endregion

        #region Variables de data usuarios email y pwd semilla
        public static string EMAIL_SUPER_ADMIN_LESTOMA = Decript("pjDmTWJ+UVk+ksN4yw1koq37M2ysKRZRIuCBzQWh12Mmjr4AIEsTW0mrS35275cflbWiSYn0uFnUoc3zuuuzlw==");
        public static string EMAIL_SUPER_ADMIN = Decript("3MemG2+jDqmU8zsW++6lXHH9EhsubfMb313Rl2+4Z9p01eIERn5XkEcqglHaZNGs");
        public static string EMAIL_ADMIN = Decript("b4nfhxk+/UltMSEK/ezW0UnqHgRebq1OvBaaxaxfAh09enB9S+Apd/IJU9kNcxtTak6zQeDmNIeh9zf2eYiLGg==");
        public static string EMAIL_AUXILIAR = Decript("RSLv5clu0Wsk3bSqz0DTYJm+mEg9AOFcPX9lNLSMUw0bbyzEaKUITvP6OYkZ8cpteVGRTcGPULaMTV6JqCzdfw==");
        public static string PWD_SUPER_ADMIN = Decript("HgDftgHM+PpknE3oxRH4Q7cxN4G9oOcw0RVJMZu+ol0=");
        public static string PWD_ADMIN = Decript("7Cq3fMRoo69IHINVq7YRYkVgJo42bk1bkvaIZRBAyME=");
        public static string PWD_AUXILIAR = Decript("VKsb3eDWTZSiU8IhKCFIDvHMy5hTQbJt+HrauPNPASg=");
        #endregion

        #region Variables cache 
        public const string REFRESH = "refresh";
        public const string PROTECT_USER = "UserInfo.Protector";
        public const string CACHE_REPORTE_KEY = "KEYREPORTE";
        #endregion

        private static string Decript(string pwd)
        {
            return Encryption.EncryptDecrypt.Decrypt(pwd);
        }
    }
}
