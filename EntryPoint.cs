using BepInEx.Unity.IL2CPP;
using BepInEx;
using Il2CppInterop.Runtime.Injection;
using HarmonyLib;
using Hikaria.DevConsoleLight.Utils;

namespace Hikaria.DevConsoleLight
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class EntryPoint : BasePlugin
    {
        public override void Load()
        {
            Instance = this;
            ClassInjector.RegisterTypeInIl2Cpp<DevConsoleMono>();
            m_Harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            m_Harmony.PatchAll();
            Logs.LogMessage("OK");
        }

        internal Harmony m_Harmony;

        internal static EntryPoint Instance;
    }
}
