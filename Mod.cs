using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using HarmonyLib;

[assembly: MelonInfo(typeof(GlitchedAttraction.Mod), "GlitchedAttraction", "1.0.0", "Porta")]
[assembly: MelonGame("PowerLine Studios", "The Glitched Attraction")]

namespace GlitchedAttraction
{
    public class Mod : MelonMod
    {
        public static bool MASTER_ENABLED;
        public static bool DEBUG;
        public static HarmonyLib.Harmony harmony;

        public override void OnInitializeMelon()
        {
            harmony = new HarmonyLib.Harmony("GlitchedAttraction");
            var prefs = LoadPrefs.Register();
            MelonLogger.Msg("GlitchedAttraction Prefrences Loaded");
            MelonLogger.Msg("GlitchedAttraction Enabled: " + prefs.enabled.Value);
            MelonLogger.Msg("GlitchedAttraction Debug logging: " + prefs.debug.Value);
            MASTER_ENABLED = prefs.enabled.Value;
            DEBUG = prefs.debug.Value;
            if (MASTER_ENABLED)
            {
                PatchModular();
                MelonLogger.Msg(ConsoleColor.Green, "GlitchedAttraction All Patches Applied");
            }
        }

        public static void PatchModular()
        {
            //for every patch in the patches folder, get the Original and Patch fields
            //and patch them
            var patches = typeof(Mod).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IPatch)));
            var trans = typeof(Mod).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ITranspiler)));
            if (DEBUG)
            {
                MelonLogger.Msg(
                    "GlitchedAttraction Patching "
                        + patches.Count()
                        + " patches and "
                        + trans.Count()
                        + " transpilers"
                );
                MelonLogger.Msg("Starting Transpilers");
            }
            foreach (var transpiler in trans)
            {
                var instance = Activator.CreateInstance(transpiler) as ITranspiler;
                harmony.Patch(instance.Original, transpiler: instance.Patch);
                if (DEBUG)
                {
                    MelonLogger.Msg(
                        ConsoleColor.DarkGreen,
                        "GlitchedAttraction Patched " + instance.debugName
                    );
                }
            }

            if (DEBUG)
            {
                MelonLogger.Msg(ConsoleColor.Green, "Starting Prefixes and Postfixes");
            }
            foreach (var patch in patches)
            {
                var patchInstance = Activator.CreateInstance(patch) as IPatch;
                if (patchInstance.patchType == PatchType.Prefix)
                {
                    harmony.Patch(patchInstance.Original, prefix: patchInstance.Patch);
                }
                else if (patchInstance.patchType == PatchType.Postfix)
                {
                    harmony.Patch(patchInstance.Original, postfix: patchInstance.Patch);
                }
                if (DEBUG)
                {
                    MelonLogger.Msg("GlitchedAttraction Patched " + patchInstance.debugName);
                }
            }
        }
    }

    public interface IPatch
    {
        System.Reflection.MethodBase Original { get; }
        HarmonyMethod Patch { get; }
        string debugName { get; }
        PatchType patchType { get; }
    }

    public interface ITranspiler
    {
        System.Reflection.MethodBase Original { get; }
        HarmonyMethod Patch { get; }
        string debugName { get; }
    }

    public enum PatchType
    {
        Prefix,
        Postfix,
        Transpiler
    }
}
