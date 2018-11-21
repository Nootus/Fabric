namespace Nootus.Fabric.Mobile.Settings
{
    public abstract class AppSettings
    {
        public const string AuthToken = "JwtToken";
        public const string AuthRefreshToken = "RefreshToken";

        protected SettingsService service;
        private string jwtToken;
        private string refreshToken;

        public AppSettings(SettingsService service)
        {
            this.service = service;

            jwtToken = service.GetValue<string>(AuthToken);
            refreshToken = service.GetValue<string>(AuthRefreshToken);
        }

        public abstract string ApiEndPoint { get; }

        public string JwtToken
        {
            get => jwtToken;
            set
            {
                jwtToken = value;
                service.AddOrUpdateValue(AuthToken, jwtToken);
            }
        }

        public string RefreshToken
        {
            get => refreshToken;
            set
            {
                refreshToken = value;
                service.AddOrUpdateValue(AuthRefreshToken, refreshToken);
            }
        }
    }
}
