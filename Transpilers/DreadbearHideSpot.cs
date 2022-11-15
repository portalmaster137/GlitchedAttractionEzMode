using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using GlitchedAttraction;
using HarmonyLib;
using MelonLoader;

public class DreadbearHideTranspile : IPatch
{
    public System.Reflection.MethodBase Original =>
        typeof(Dreadbear_Hidespot).GetMethod(
            "OnTriggerExit",
            BindingFlags.Public | BindingFlags.Instance
        );
    public HarmonyMethod Patch =>
        new HarmonyMethod(
            typeof(DreadbearHideTranspile).GetMethod(
                "Transpiler",
                BindingFlags.Public | BindingFlags.Static
            )
        );
    public string debugName => "Dreadbear_Hide.OnTriggerExit";
    public PatchType patchType => PatchType.Transpiler;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        //What is is: IL_0018: ldc.i4.0
        //Desired: IL_0018: ldc.i4.1

        //Find the index of the instruction
        var index = codes.FindIndex(code => code.opcode == OpCodes.Ldc_I4_0);

        //Replace the instruction
        codes[index] = new CodeInstruction(OpCodes.Ldc_I4_1);

        //Return the new instructions

        return codes.AsEnumerable();
    }
}
