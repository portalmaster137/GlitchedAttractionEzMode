using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class FFreddyPatch : ITranspiler
{
    public System.Reflection.MethodBase Original =>
        typeof(FuntimeFreddy_AI1).GetMethod(
            "Update",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
    public HarmonyLib.HarmonyMethod Patch =>
        new HarmonyLib.HarmonyMethod(
            typeof(FFreddyPatch).GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static)
        );
    public string debugName => "FuntimeFreddy_AI1.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[FFreddyPatch] " + item.ToString());
            }
        }
        codes[3] = new CodeInstruction(OpCodes.Nop);
        codes[4] = new CodeInstruction(OpCodes.Nop);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[FFreddyPatch] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
