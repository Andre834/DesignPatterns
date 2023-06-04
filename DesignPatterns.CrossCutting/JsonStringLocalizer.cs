using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;

namespace DesignPatterns.CrossCutting;

public class JsonStringLocalizer : IStringLocalizer
{
    private static ConcurrentDictionary<string, ConcurrentDictionary<string, string>> Strings => JsonSerializer.Deserialize<ConcurrentDictionary<string, ConcurrentDictionary<string, string>>>(File.ReadAllText("AppStrings.json"));

    public LocalizedString this[string name] => Get(name);

    public LocalizedString this[string name, params object[] arguments] => Get(name, arguments);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => Strings.Keys.Select(key => new LocalizedString(key, key));

    private static LocalizedString Get(string name, params object[] arguments)
    {
        var localizedString = new LocalizedString(name, name, true, nameof(JsonStringLocalizer));

        if (string.IsNullOrWhiteSpace(name) || !Strings.TryGetValue(name, out var dictionary)) return localizedString;

        if (!dictionary.TryGetValue(CultureInfo.CurrentCulture.Name, out var value)) return localizedString;

        return new LocalizedString(name, string.Format(value, arguments), false, nameof(JsonStringLocalizer));
    }
}
