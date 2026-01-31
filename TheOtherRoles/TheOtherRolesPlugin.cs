using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MiraAPI;
using MiraAPI.PluginLoading;
using MiraAPI.GameEnd;
using TheOtherRolesRemastered.GameOver;

namespace TheOtherRolesRemastered
{
    [BepInPlugin("com.eisbison.theotherroles.remastered", "The Other Roles Remastered", "1.0.0")]
    [BepInProcess("Among Us.exe")]
    [BepInDependency("gg.reactor.api")]
    [BepInDependency("com.mira.api")]
    public partial class TheOtherRolesPlugin : BasePlugin, IMiraPlugin
    {
        public Harmony Harmony { get; } = new("com.eisbison.theotherroles.remastered");

        public string OptionsTitleText => "TOR Remastered";
        public string CustomOptionMenuNameOne => "TOR Settings";
        public string CustomOptionMenuOneDescription => "Settings for The Other Roles Remastered";
        public string ModifierMenuDescription => "Modifier Settings";

        public ConfigFile GetConfigFile() => Config;

        public override void Load()
        {
            Harmony.PatchAll();
            
            // Register Game Overs
            GameOverManager.RegisterGameOver(typeof(JesterGameOver));
        }
    }
}
