using GlitchedAttraction;
using HarmonyLib;
using System.Reflection;

public class WFreddyPatch : IPatch
{
    public MethodBase Original => typeof(WitheredFreddy_AI1).GetMethod("Update");
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(WFreddyPatch).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static)
        );
    public string debugName => "WFreddy_AI1.Update";
    public PatchType patchType => PatchType.Prefix;

    private static void Prefix(WitheredFreddy_AI1 __instance)
    {
        __instance.musicBox = 100f;
    }
}
