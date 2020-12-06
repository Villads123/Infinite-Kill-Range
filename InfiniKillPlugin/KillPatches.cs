using System;
using HarmonyLib;

namespace InfiniKillPlugin
{
    class KillPatches
    {
        public static double CurDist = double.MaxValue; 
        public static BHHCHKFKPOE GetTargetPlayer()
        {
            CurDist = double.MaxValue;
            BHHCHKFKPOE target = BHHCHKFKPOE.AllPlayerControls[0];
            foreach (BHHCHKFKPOE player in BHHCHKFKPOE.AllPlayerControls)// DIFO or KHOAK | DFE FNH MKM
            {
                if (player.NetId != BHHCHKFKPOE.LocalPlayer.NetId && !player.DIFOAIGNFJF.DFEMAKIPLDO && !player.DIFOAIGNFJF.FNHEFMMHADP)
                {
                    float playerX = player.NetTransform.transform.position.x;
                    float playerY = player.NetTransform.transform.position.y;
                    float localX = BHHCHKFKPOE.LocalPlayer.transform.position.x;
                    float localY = BHHCHKFKPOE.LocalPlayer.transform.position.y;

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

        [HarmonyPatch(typeof(APCIFDAHMLI), nameof(APCIFDAHMLI.PerformKill))]
        public static class PerformKillPatch
        {
            public static void Prefix(APCIFDAHMLI __instance)

            {
                __instance.isActive = true;
                __instance.SetTarget(GetTargetPlayer());
                __instance.CurrentTarget = GetTargetPlayer();
            }
        }
    }
}
