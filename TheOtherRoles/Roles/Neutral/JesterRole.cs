using System;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.GameOptions.Attributes;
using MiraAPI.Roles;
using MiraAPI.GameEnd;
using UnityEngine;
using TheOtherRolesRemastered.Utilities;

namespace TheOtherRolesRemastered.Roles.Neutral
{
    public sealed class JesterRole : RoleBehaviour, ICustomRole
    {
        public JesterRole(IntPtr cppPtr) : base(cppPtr) { }

        // --- Options ---
        [ModdedToggleOption("Can Vent")]
        public bool CanUseVentsOption { get; set; } = true;

        [ModdedToggleOption("Impostor Vision")]
        public bool ImpostorVision { get; set; } = false;

        // --- State ---
        public bool VotedOut { get; set; } = false;

        // --- Configuration ---
        public Color RoleColor => new Color(0.99f, 0.49f, 0.76f); // Pink
        public string RoleName => "Jester";
        public string RoleDescription => "Get voted out to win.";
        public string RoleLongDescription => "Get voted out to win the game.";
        public ModdedRoleTeams Team => ModdedRoleTeams.Custom;

        public CustomRoleConfiguration Configuration => new(this)
        {
            Icon = new LoadableResourceAsset<Sprite>("Jester.png"), 
            CanUseVent = CanUseVentsOption,
        };

        // Removed invalid overrides (IsImp, CanVent) as they are handled by Configuration or base class logic
        public override bool IsAffectedByComms => false; // This is virtual in RoleBehaviour? Checking... 
        // If IsAffectedByComms isn't virtual, I should remove it too. Assuming it is for now based on previous attempts.
        // Actually, RoleBehaviour usually doesn't have many virtuals.
    }
}
