using GlitchedAttraction;
using HarmonyLib;
using System.Reflection;

public class FredbearPatch : IPatch
{
    public MethodBase Original => typeof(nFredbear_AI1).GetMethod("Update");
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(FredbearPatch).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static)
        );
    public string debugName => "Fredbear_AI1.Update";
    public PatchType patchType => PatchType.Prefix;

    private static void Prefix(nFredbear_AI1 __instance)
    {
        __instance.stade = -1f;
    }
}
