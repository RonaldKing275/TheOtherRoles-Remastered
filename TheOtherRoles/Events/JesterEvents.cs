using MiraAPI.Events;
using MiraAPI.Events.Vanilla.Meeting;
using TheOtherRolesRemastered.Roles.Neutral;
using MiraAPI.GameEnd;

namespace TheOtherRolesRemastered.Events
{
    public static class JesterEvents
    {
        [RegisterEvent]
        public static void OnEjection(EjectionEvent @event)
        {
            var exiledPlayer = @event.ExileController?.initData?.networkedPlayer?.Object;
            
            if (exiledPlayer != null && exiledPlayer.Data.Role is JesterRole jester)
            {
                jester.VotedOut = true;
            }
        }
    }
}
