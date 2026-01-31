using System;
using System.Text;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Hud;
using MiraAPI.Roles;
using MiraAPI.Modifiers;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using TheOtherRolesRemastered.Buttons.Crewmate;
using TheOtherRolesRemastered.Utilities;
using UnityEngine;

namespace TheOtherRolesRemastered.Roles.Crewmate
{
    public sealed class SheriffRole : CrewmateRole, ICustomRole
    {
        public SheriffRole(IntPtr cppPtr) : base(cppPtr) { }

        // --- Options ---
        [ModdedNumberOption("Cooldown", 30f, 10f, 60f, MiraNumberSuffixes.Seconds)]
        public float Cooldown { get; set; } = 30f;

        [ModdedToggleOption("Can Kill Neutrals")]
        public bool CanKillNeutrals { get; set; } = false;

        [ModdedToggleOption("Can Kill Madmate")]
        public bool CanKillMadmate { get; set; } = false;

        [ModdedToggleOption("Misfire Kills Target")]
        public bool MisfireKillsTarget { get; set; } = false;

        public Color RoleColor => new Color(1f, 0.8f, 0.2f); // Gold/Yellowish
        public string RoleName => "Sheriff";
        public string RoleDescription => "Shoot the Impostors.";
        public string RoleLongDescription => "Shoot the Impostors. If you shoot a Crewmate, you die.";
        public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

        public CustomRoleConfiguration Configuration => new(this)
        {
            Icon = new LoadableResourceAsset<Sprite>("TargetIcon.png"), 
            CanUseVent = false,
        };

        [HideFromIl2Cpp]
        public override void Initialize(PlayerControl player)
        {
            base.Initialize(player);
        }
    }
}
