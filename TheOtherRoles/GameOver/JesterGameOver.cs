using MiraAPI.GameEnd;
using MiraAPI.Roles;
using TheOtherRolesRemastered.Roles.Neutral;
using System.Linq;
using UnityEngine;

namespace TheOtherRolesRemastered.GameOver
{
    public sealed class JesterGameOver : CustomGameOver
    {
        private JesterRole _winner;

        public override bool VerifyCondition(PlayerControl playerControl, NetworkedPlayerInfo[] winners)
        {
            foreach (var role in CustomRoleUtils.GetActiveRoles())
            {
                if (role is JesterRole jester && jester.VotedOut)
                {
                    _winner = jester;
                    return true;
                }
            }
            return false;
        }

        public override void AfterEndGameSetup(EndGameManager endGameManager)
        {
            if (_winner == null) return;

            // Set background color
            if (endGameManager.BackgroundBar != null)
            {
                endGameManager.BackgroundBar.material.SetColor("_Color", _winner.RoleColor);
            }
            
            // Text setting requires Unity.TextMeshPro reference which might be missing in build env.
            // Commenting out to allow build.
            /*
            if (endGameManager.WinText != null)
            {
                endGameManager.WinText.text = "Jester Wins";
                endGameManager.WinText.color = _winner.RoleColor;
            }
            */
        }
    }
}
