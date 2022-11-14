using MelonLoader;

public struct Prefs
{
    public MelonPreferences_Category category;
    public MelonPreferences_Entry<bool> enabled;
    public MelonPreferences_Entry<bool> debug;
}

public static class LoadPrefs
{
    public static Prefs Register()
    {
        var cat = MelonPreferences.CreateCategory("GlitchedAttraction", "GlitchedAttraction");
        var enabled = cat.CreateEntry("enabled", true, "Enabled");
        var debug = cat.CreateEntry("debug", false, "Debug");
        return new Prefs
        {
            category = cat,
            enabled = enabled,
            debug = debug
        };
    }
}
