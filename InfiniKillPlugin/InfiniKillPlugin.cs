using BepInEx.IL2CPP;
using HarmonyLib;
using System.Reflection;
using BepInEx;

namespace InfiniKillPlugin
{
    [BepInPlugin("lol.xtracube.InfiniPlugin", "Infinite Kill Range", "1.1.0")]
    public class InfiniteKillPlugin : BasePlugin  
    {

        public static BepInEx.Logging.ManualLogSource log;

        public void BepInExLoader()
        {
            log = Log;
        }
        public override void Load()
        {
            Harmony _harmony;
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
