using Nootus.Fabric.Mobile.Security;

namespace Nootus.Fabric.Mobile.Settings
{
    public abstract class AppSettings
    {
        private const string AuthToken = "Token";

        protected SettingsService service;
        private Token token;

        public AppSettings(SettingsService service)
        {
            this.service = service;

            token = service.GetValue<Token>(AuthToken);
        }

        public Token Token
        {
            get => token;
            set
            {
                token = value;
                service.AddOrUpdateValue(AuthToken, token);
            }
        }
    }
}
