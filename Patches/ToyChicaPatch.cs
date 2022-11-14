using System.Reflection;
using GlitchedAttraction;
using HarmonyLib;

public class ToyChicaPatch : IPatch
{
    public MethodBase Original => typeof(ToyChica_AI1).GetMethod("Update");

    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(ToyChicaPatch).GetMethod("Prefix", BindingFlags.NonPublic | BindingFlags.Static)
        );

    public string debugName => "ToyChica_AI1.Update";

    public PatchType patchType => PatchType.Prefix;

    private static void Prefix(ToyChica_AI1 __instance)
    {
        __instance.followPlayer = false;
    }
}
