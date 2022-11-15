using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;

public class EnnardPatch : ITranspiler
{
    public System.Reflection.MethodBase Original =>
        typeof(Ennard_AI).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
    public HarmonyLib.HarmonyMethod Patch =>
        new HarmonyLib.HarmonyMethod(
            typeof(EnnardPatch).GetMethod("Transpiler", BindingFlags.Public | BindingFlags.Static)
        );
    public string debugName => "Ennard_AI.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLoader.MelonLogger.Msg(
                    System.ConsoleColor.Cyan,
                    "[EnnardPatch] " + item.ToString()
                );
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
                MelonLoader.MelonLogger.Msg(
                    System.ConsoleColor.Yellow,
                    "[EnnardPatch] " + item.ToString()
                );
            }
        }
        return codes.AsEnumerable();
    }
}
