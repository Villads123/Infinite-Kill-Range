using System;
using HarmonyLib;

namespace InfiniKillPlugin
{
    class KillPatches
    {
        public static double CurDist = double.MaxValue; public static LBADNGJPJGH GetTargetPlayer()
        {
            CurDist = double.MaxValue;
            LBADNGJPJGH target = LBADNGJPJGH.AllPlayerControls[0];
            foreach (LBADNGJPJGH player in LBADNGJPJGH.AllPlayerControls)
            {
                if (player.NetId != LBADNGJPJGH.LocalPlayer.NetId && !player.NIFDFKDENKA.DHENNPCNJGB && !player.NIFDFKDENKA.FKGFOMBFMHD)
                {
                    float playerX = player.NetTransform.transform.position.x;
                    float playerY = player.NetTransform.transform.position.y;
                    float localX = LBADNGJPJGH.LocalPlayer.transform.position.x;
                    float localY = LBADNGJPJGH.LocalPlayer.transform.position.y;

                    float xDistance = Math.Abs(localX - playerX);
                    float yDistance = Math.Abs(localY - playerY);
                    double NewDist = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                    if (NewDist < CurDist)
                    {
                        CurDist = NewDist;
                        target = player;
                    }
                }
            }
            return target;
        }

        [HarmonyPatch(typeof(FEEPMKCPHAP), nameof(FEEPMKCPHAP.PerformKill))]
        public static class PerformKillPatch
        {
            public static void Prefix(FEEPMKCPHAP __instance)
            {
                __instance.isActive = true;
                __instance.SetTarget(GetTargetPlayer());
                __instance.CurrentTarget = GetTargetPlayer();
            }
        }
    }
}
