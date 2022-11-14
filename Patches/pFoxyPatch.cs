using System.Reflection;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class pFoxyPatch : IPatch
{
    public MethodBase Original => typeof(PhantomFoxy_AI1).GetMethod("AI");
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(pFoxyPatch).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static)
        );
    public PatchType patchType => PatchType.Prefix;
    public string debugName => "PhantomFoxy_AI1.AI";

    private static void Prefix(PhantomFoxy_AI1 __instance)
    {
        __instance.stade = -1;
    }
}
