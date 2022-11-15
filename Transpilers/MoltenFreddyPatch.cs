using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class MoltenFreddyPatch : ITranspiler
{
    public MethodBase Original =>
        typeof(MoltenFreddy_AI).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);

    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(MoltenFreddyPatch).GetMethod(
                "Transpiler",
                BindingFlags.Public | BindingFlags.Static
            )
        );

    public string debugName => "MoltenFreddy_AI.Update";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[MFreddyPatch] " + item.ToString());
            }
        }
        codes[0] = new CodeInstruction(OpCodes.Nop);
        codes[1] = new CodeInstruction(OpCodes.Nop);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[MFreddyPatch] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
