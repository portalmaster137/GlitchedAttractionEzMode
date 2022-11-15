using System.Reflection.Emit;
using System.Collections.Generic;
using System.Reflection;
using GlitchedAttraction;
using HarmonyLib;
using System.Linq;
using MelonLoader;

public class DreadbearHideTranspile : ITranspiler
{
    public System.Reflection.MethodBase Original =>
        typeof(Dreadbear_Hidespot).GetMethod(
            "OnTriggerExit",
            BindingFlags.Public | BindingFlags.Instance
        );
    public HarmonyMethod Patch =>
        new HarmonyMethod(typeof(DreadbearHideTranspile).GetMethod("Transpiler"));
    public string debugName => "Dreadbear_Hide.OnTriggerExit";

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        var ind = codes.FindIndex(code => code.opcode == OpCodes.Ldc_I4_0);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Cyan, "[DreadbearHide] " + item.ToString());
            }
        }
        codes[ind] = new CodeInstruction(OpCodes.Ldc_I4_1);
        foreach (var item in codes)
        {
            if (Mod.DEBUG)
            {
                MelonLogger.Msg(System.ConsoleColor.Yellow, "[DreadbearHide] " + item.ToString());
            }
        }
        return codes.AsEnumerable();
    }
}
