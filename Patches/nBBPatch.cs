using System.Reflection;
using GlitchedAttraction;
using HarmonyLib;

public class NBBPatch : IPatch
{
    public MethodBase Original => typeof(nBB_AI1).GetMethod("Update");

    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(NBBPatch).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static)
        );

    public string debugName => "nBB_AI1.Update";

    public PatchType patchType => PatchType.Prefix;

    private static void Prefix(nBB_AI1 __instance)
    {
        __instance.stade = -1f;
    }
}
