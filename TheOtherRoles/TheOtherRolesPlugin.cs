using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using MiraAPI;
using MiraAPI.PluginLoading;
using MiraAPI.GameEnd;
using MiraAPI.Roles; // Wymagane do CustomRoleManager
using TheOtherRolesRemastered.GameOver; // Upewnij się, że masz ten namespace
using TheOtherRolesRemastered.Roles.Crewmate; // Namespace Twojego Sheriffa
using TheOtherRolesRemastered.Roles.Neutral;  // Namespace Twojego Jestera

namespace TheOtherRolesRemastered
{
    [BepInPlugin("com.eisbison.theotherroles.remastered", "The Other Roles Remastered", "1.0.0")]
    [BepInProcess("Among Us.exe")]
    [BepInDependency("gg.reactor.api")] // Zostawiamy, jeśli Reactor jest w libs
    [BepInDependency("com.allofus.miraapi")] // POPRAWIONE ID
    public partial class TheOtherRolesPlugin : BasePlugin, IMiraPlugin
    {
        public static TheOtherRolesPlugin Instance;
        public Harmony Harmony { get; } = new("com.eisbison.theotherroles.remastered");

        // --- Implementacja IMiraPlugin (Wymagane!) ---
        public string Name => "The Other Roles Remastered";
        public string Description => "Classic TOR ported to MiraAPI";
        public string Version => "1.0.0";

        // Teksty w menu
        public string OptionsTitleText => "TOR Remastered";
        public string CustomOptionMenuNameOne => "TOR Settings";
        public string CustomOptionMenuOneDescription => "Settings for The Other Roles Remastered";
        public string ModifierMenuDescription => "Modifier Settings";

        public ConfigFile GetConfigFile() => Config;

        public override void Load()
        {
            Instance = this;
            
            // 1. Rejestracja w API (KLUCZOWE)
            MiraPluginManager.RegisterPlugin(this);
            
            // 2. Patchowanie gry
            Harmony.PatchAll();
            
            // 3. Rejestracja Ról (Bez tego nie ma suwaków w lobby!)
            // Upewnij się, że klasy SheriffRole i JesterRole istnieją i są publiczne
            CustomRoleManager.RegisterRole<SheriffRole>();
            CustomRoleManager.RegisterRole<JesterRole>();
            // CustomRoleManager.RegisterRole<ArsonistRole>(); // Odkomentuj jak zrobisz Arsonista
            
            // 4. Rejestracja Ekranów Końcowych
            // (Upewnij się, że JesterGameOver istnieje, jeśli nie - zakomentuj tymczasowo)
            if (typeof(JesterGameOver) != null) 
            {
                 GameOverManager.RegisterGameOver(typeof(JesterGameOver));
            }
            
            Log.LogInfo("The Other Roles Remastered loaded successfully!");
        }
    }
}