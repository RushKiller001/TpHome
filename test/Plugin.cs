using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using Utilla;
using Utilla.Models;

namespace test
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        GameObject quitbox;
        Rigidbody PlayerRB;
        void Start()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
            new Harmony("TPHome").PatchAll(Assembly.GetExecutingAssembly());
        }
        void OnGameInitialized()
        {
            quitbox = GameObject.Find("QuitBox");
            quitbox.transform.localScale = new Vector3(9999999999, 247, 9999999999);
            PlayerRB = GorillaTagger.Instance.rigidbody;
        }

        void Update()
        {
            if (PlayerRB.gameObject.transform.position.y > 5000 || PlayerRB.gameObject.transform.position.y < -5000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }
            if (PlayerRB.gameObject.transform.position.x > 5000 || PlayerRB.gameObject.transform.position.x < -5000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }
            if (PlayerRB.gameObject.transform.position.z > 5000 || PlayerRB.gameObject.transform.position.z < -5000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }

        }

    }
    [HarmonyPatch(typeof(GorillaQuitBox))]
    [HarmonyPatch("Start")]
    class BoxPath
    {
        static bool Prefix(GorillaQuitBox __instance)
        {
            __instance.AddComponent<Tphome>();
            return false;
        }
    }

    public class Tphome : GorillaTriggerBox
    {
        public Rigidbody PlayerRB;
        public override void OnBoxTriggered()
        {
            PlayerRB.velocity = Vector3.zero;
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().transform.parent.position = new Vector3(-66.4736f, 12.7625f, -82.3621f);
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().transform.localPosition = Vector3.zero;
            GorillaLocomotion.Player.Instance.InitializeValues();
            GameObject[] array = GorillaNetworking.PhotonNetworkController.Instance.disableOnStartup;
            for (int i = 0; i < array.Length; i++)
            {
                array[i].SetActive(false);
            }
            array = GorillaNetworking.PhotonNetworkController.Instance.enableOnStartup;
            for (int i = 0; i < array.Length; i++)
            {
                array[i].SetActive(true);
            }
            if (!NetworkSystem.Instance.GameModeString.Contains("MODDED_"))
            {
                NetworkSystem.Instance.ReturnToSinglePlayer();
            }
        }
        void Awake()
        {
            PlayerRB = PlayerRB = GorillaTagger.Instance.rigidbody;
            Destroy(gameObject.GetComponent<GorillaQuitBox>());
        }
    }
}
