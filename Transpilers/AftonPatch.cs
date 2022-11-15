using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class AftonPatch : ITranspiler
{
    public MethodBase Original =>
        typeof(Afton_AI).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);

    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(AftonPatch).GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static)
        );

    public string debugName => "Afton_AI.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[AftonPatch] " + item.ToString());
            }
        }
        codes[0] = new CodeInstruction(OpCodes.Nop);
        codes[1] = new CodeInstruction(OpCodes.Nop);
        codes[2] = new CodeInstruction(OpCodes.Nop);
        codes[3] = new CodeInstruction(OpCodes.Nop);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[AftonPatch] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
