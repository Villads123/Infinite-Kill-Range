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

        [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
        public static class PerformKillPatch
        {
            public static void Prefix(KillButtonManager __instance)
            {
                double CurDist = double.MaxValue;
                PlayerControl targetPlayer = PlayerControl.AllPlayerControls[0];
                foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                {
                    if (player.NetId != PlayerControl.LocalPlayer.NetId && player.Data.IsDead==false)
                    {
                        float playerX = player.NetTransform.transform.position.x;
                        float playerY = player.NetTransform.transform.position.y;
                        float localX = PlayerControl.LocalPlayer.transform.position.x;
                        float localY = PlayerControl.LocalPlayer.transform.position.y;

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
        
        [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.SetCoolDown))]
        public static class KillPatch
        {
            public static void Postfix()
            {
                CurDist = double.MaxValue;
            }
        }
    }
}
