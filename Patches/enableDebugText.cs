using System.Reflection;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class enableDebugText : IPatch
{
    public MethodBase Original => typeof(Player_DebugText).GetMethod("Awake");
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(enableDebugText).GetMethod(
                "Postfix",
                BindingFlags.NonPublic | BindingFlags.Static
            )
        );
    public PatchType patchType => PatchType.Postfix;
    public string debugName => "Player_DebugText.Awake";

    private static void Postfix(Player_DebugText __instance)
    {
        __instance.debugText.SetActive(true);
    }
}
