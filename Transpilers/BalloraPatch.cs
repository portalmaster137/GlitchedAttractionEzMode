using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class BalloraPatch : ITranspiler
{
    public System.Reflection.MethodBase Original =>
        typeof(Ballora_AI1).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
    public HarmonyLib.HarmonyMethod Patch =>
        new HarmonyLib.HarmonyMethod(
            typeof(BalloraPatch).GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static)
        );
    public string debugName => "Ballora_AI1.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[BalloraPatch] " + item.ToString());
            }
        }
        codes[3] = new CodeInstruction(OpCodes.Nop);
        codes[4] = new CodeInstruction(OpCodes.Nop);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[BalloraPatch] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
