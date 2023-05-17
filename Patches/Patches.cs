using GameData;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Hikaria.DevConsoleLight.Utils;
using Player;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using BepInEx;
using CellMenu;

namespace Hikaria.DevConsoleLight.Patches
{
    [HarmonyPatch]
    internal class Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(GameDataInit), "Initialize")]
        private static void GameDataInit__Initialize__Prefix()
        {
            DevConsole.Init();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Cursor), "visible", MethodType.Setter)]
        private static void Cursor__visible__Prefix(ref bool value)
        {
            if (DevConsoleMono.Instance == null)
            {
                return;
            }

            bool flag = DevConsoleMono.Instance.ConsoleIsShowing;
            if (flag)
            {
                DevConsoleMono.lastVisible = value;
                value = true;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Cursor), "lockState", MethodType.Setter)]
        private static void Cursor__lockState__Prefix(ref CursorLockMode value)
        {
            if (DevConsoleMono.Instance == null)
            {
                return;
            }

            bool flag = DevConsoleMono.Instance.ConsoleIsShowing;
            if (flag)
            {
                bool flag2 = value > CursorLockMode.None;
                if (flag2)
                {
                    DevConsoleMono.lastLockMode = value;
                }
                value = CursorLockMode.None;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(InputMapper), "DoGetAxis")]
        private static bool InputMapper__DoGetAxis__Prefix(ref float __result)
        {
            bool flag = Cursor.lockState > CursorLockMode.None;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                __result = 0f;
                result = false;
            }
            return result;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(InputMapper), "DoGetButton")]
        [HarmonyPatch(typeof(InputMapper), "DoGetButtonUp")]
        [HarmonyPatch(typeof(InputMapper), "DoGetButtonDown")]
        private static bool InputMapper__DoGetButton__Prefix(bool __result)
        {
            bool flag = Cursor.lockState > CursorLockMode.None;
            bool result;
            if (flag)
            {
                result = true;
            }
            else
            {
                __result = false;
                result = false;
            }
            return result;
        }
    }
}
