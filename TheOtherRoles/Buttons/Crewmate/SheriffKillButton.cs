using System;
using System.Linq;
using MiraAPI.Hud;
using MiraAPI.Utilities;
using MiraAPI.Utilities.Assets;
using TheOtherRolesRemastered.Roles.Crewmate;
using TheOtherRolesRemastered.Utilities;
using UnityEngine;
using AmongUs.GameOptions;

namespace TheOtherRolesRemastered.Buttons.Crewmate
{
    public sealed class SheriffKillButton : CustomActionButton
    {
        public override string Name => "Shoot";
        public override float Cooldown => GetRole<SheriffRole>()?.Cooldown ?? 30f;
        
        // Use our new LoadableResourceAsset with a file we know exists in LegacySource (TargetIcon.png as placeholder)
        public override LoadableAsset<Sprite> Sprite => new LoadableResourceAsset<Sprite>("TargetIcon.png"); 
        
        public bool CanUseInVents => false; 

        public PlayerControl Target { get; private set; }

        public override bool Enabled(RoleBehaviour role)
        {
            return role is SheriffRole;
        }

        private PlayerControl GetTarget()
        {
            PlayerControl closest = null;
            float closestDistance = float.MaxValue;
            Vector2 myPos = PlayerControl.LocalPlayer.GetTruePosition();
            
            float[] killDistances = new float[] { 1.0f, 1.8f, 2.5f };
            int distIndex = Mathf.Clamp(GameOptionsManager.Instance.currentNormalGameOptions.KillDistance, 0, 2);
            float killDist = killDistances[distIndex];

            foreach (var p in PlayerControl.AllPlayerControls)
            {
                if (p.Data.IsDead || p.Data.Disconnected || p == PlayerControl.LocalPlayer) continue;
                
                float dist = Vector2.Distance(myPos, p.GetTruePosition());
                if (dist <= killDist && dist < closestDistance)
                {
                    closest = p;
                    closestDistance = dist;
                }
            }
            return closest;
        }

        protected override void OnClick()
        {
            Target = GetTarget();
            if (Target == null) return;

            var sheriffRole = GetRole<SheriffRole>();
            if (sheriffRole == null) return;

            bool isImpostor = Target.Data.Role.IsImpostor;
            bool isNeutral = !isImpostor && Target.Data.Role.TeamType != RoleTeamTypes.Crewmate; 
            
            bool safeToKill = isImpostor;
            if (isNeutral && sheriffRole.CanKillNeutrals) safeToKill = true;

            if (safeToKill)
            {
                PlayerControl.LocalPlayer.RpcMurderPlayer(Target, true);
            }
            else
            {
                if (sheriffRole.MisfireKillsTarget)
                {
                    PlayerControl.LocalPlayer.RpcMurderPlayer(Target, true);
                }
                
                PlayerControl.LocalPlayer.RpcMurderPlayer(PlayerControl.LocalPlayer, true);
            }
            
            this.Timer = this.Cooldown;
        }

        private T GetRole<T>() where T : class
        {
            return PlayerControl.LocalPlayer.Data.Role as T;
        }
    }
}
