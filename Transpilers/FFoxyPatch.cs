using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class FFoxyPatch : ITranspiler
{
    public System.Reflection.MethodBase Original =>
        typeof(FuntimeFoxy_AI1).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
    public HarmonyLib.HarmonyMethod Patch =>
        new HarmonyLib.HarmonyMethod(
            typeof(FFoxyPatch).GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static)
        );
    public string debugName => "FuntimeFoxy_AI1.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[FFoxyPatch] " + item.ToString());
            }
        }
        codes[3] = new CodeInstruction(OpCodes.Nop);
        codes[4] = new CodeInstruction(OpCodes.Nop);
        codes[5] = new CodeInstruction(OpCodes.Nop);
        codes[6] = new CodeInstruction(OpCodes.Nop);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[FFoxyPatch] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
