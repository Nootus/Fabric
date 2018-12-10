using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Settings
{
    public class SettingsService
    {
        public Task AddOrUpdateValue<TValue>(string key, TValue value) => AddOrUpdateValueInternal(key, value);
        public TValue GetValueOrDefault<TValue>(string key, TValue defaultValue) => GetValueOrDefaultInternal(key, defaultValue);
        public TValue GetValue<TValue>(string key) => GetValueOrDefaultInternal(key, default(TValue));

        private async Task AddOrUpdateValueInternal<TValue>(string key, TValue value)
        {
            if (value == null)
            {
                await Remove(key);
            }
            else
            {
                Application.Current.Properties[key] = JsonConvert.SerializeObject(value);
                await Application.Current.SavePropertiesAsync();
            }
        }

        private TValue GetValueOrDefaultInternal<TValue>(string key, TValue defaultValue = default(TValue))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = JsonConvert.DeserializeObject<TValue>(Application.Current.Properties[key] as string);
            }
            return null != value ? (TValue)value : defaultValue;
        }

        private async Task Remove(string key)
        {
            if (Application.Current.Properties[key] != null)
            {
                Application.Current.Properties.Remove(key);
                await Application.Current.SavePropertiesAsync();
            }
        }
    }
}
