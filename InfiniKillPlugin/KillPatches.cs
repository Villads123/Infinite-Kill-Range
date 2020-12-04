using System;
using InfiniKillPlugin;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx.Logging;
using HarmonyLib;
using UnhollowerBaseLib;

namespace InfiniKillPlugin
{
    class KillPatches
    {
        public static double CurDist = double.MaxValue;

        [HarmonyPatch(typeof(EHFMOJPIKPE), nameof(EHFMOJPIKPE.PerformKill))]
        public static class PerformKillPatch
        {
            public static void Prefix(EHFMOJPIKPE __instance)
            {
                CurDist = double.MaxValue;
                JENJGDMOEOC targetPlayer = JENJGDMOEOC.AllPlayerControls[0];
                foreach (JENJGDMOEOC player in JENJGDMOEOC.AllPlayerControls)
                {
                    if (player.NetId != JENJGDMOEOC.LocalPlayer.NetId && !player.CJJDCBHJGBL.OKDGIIGNNMG && !player.CJJDCBHJGBL.DOBEJNDNPJI)
                    {
                        float playerX = player.NetTransform.transform.position.x;
                        float playerY = player.NetTransform.transform.position.y;
                        float localX = JENJGDMOEOC.LocalPlayer.transform.position.x;
                        float localY = JENJGDMOEOC.LocalPlayer.transform.position.y;

                        float xDistance = Math.Abs(localX - playerX);
                        float yDistance = Math.Abs(localY - playerY);
                        double NewDist = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
                        if (NewDist < CurDist)
                        {
                            CurDist = NewDist;
                            targetPlayer = player;
                        }
                    }
                }
                __instance.SetTarget(targetPlayer);
                __instance.CurrentTarget = targetPlayer;
            }
        }
        
        [HarmonyPatch(typeof(EHFMOJPIKPE), nameof(EHFMOJPIKPE.SetCoolDown))]
        public static class KillPatch
        {
            public static void Postfix()
            {
                CurDist = double.MaxValue;
            }
        }
    }
}
