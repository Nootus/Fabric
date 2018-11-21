using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Settings
{
    public class SettingsService
    {
        public Task AddOrUpdateValue(string key, bool value) => AddOrUpdateValueInternal(key, value);
        public Task AddOrUpdateValue(string key, string value) => AddOrUpdateValueInternal(key, value);
        public bool GetValueOrDefault(string key, bool defaultValue) => GetValueOrDefaultInternal(key, defaultValue);
        public string GetValueOrDefault(string key, string defaultValue) => GetValueOrDefaultInternal(key, defaultValue);

        public TValue GetValue<TValue>(string key) => GetValueOrDefaultInternal(key, default(TValue));

        async Task AddOrUpdateValueInternal<T>(string key, T value)
        {
            if (value == null)
            {
                await Remove(key);
            }
            else
            {
                Application.Current.Properties[key] = value;
                await Application.Current.SavePropertiesAsync();
            }
        }

        T GetValueOrDefaultInternal<T>(string key, T defaultValue = default(T))
        {
            object value = null;
            if (Application.Current.Properties.ContainsKey(key))
            {
                value = Application.Current.Properties[key];
            }
            return null != value ? (T)value : defaultValue;
        }

        async Task Remove(string key)
        {
            if (Application.Current.Properties[key] != null)
            {
                Application.Current.Properties.Remove(key);
                await Application.Current.SavePropertiesAsync();
            }
        }
    }
}
