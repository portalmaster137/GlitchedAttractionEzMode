using GlitchedAttraction;
using System.Reflection;
using HarmonyLib;

public class NightmarionnePatch : IPatch
{
    public MethodBase Original => typeof(nightmarionne_AI1).GetMethod("Update");
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(NightmarionnePatch).GetMethod(
                "Prefix",
                BindingFlags.NonPublic | BindingFlags.Static
            )
        );
    public string debugName => "nightmarionne_AI1.Update";
    public PatchType patchType => PatchType.Prefix;

    private static void Prefix(nightmarionne_AI1 __instance)
    {
        __instance.stade = -1f;
    }
}
