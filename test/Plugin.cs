using BepInEx;
using GorillaNetworking;
using System;
using UnityEngine;
using Utilla;

namespace test
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        GameObject quitbox;
        GameObject quitbox_1;
        GameObject quitbox_2;
        GameObject quitbox_3;
        GameObject quitbox_4;
        Rigidbody Direction;




        void Start ()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }
        void OnGameInitialized(object sender, EventArgs e)
        {
            quitbox = GameObject.Find("QuitBox");
            quitbox_1 = GameObject.Find("QuitBox (1)");
            quitbox_2 = GameObject.Find("QuitBox (2)");
            quitbox_3 = GameObject.Find("QuitBox (3)");
            quitbox_4 = GameObject.Find("QuitBox (4)");
            quitbox.AddComponent<Tphome>();
            quitbox_1.AddComponent<Tphome>();
            quitbox_2.AddComponent<Tphome>();
            quitbox_3.AddComponent<Tphome>();
            quitbox_4.AddComponent<Tphome>();

            quitbox.transform.localScale = new Vector3(9999999999, 247, 9999999999);

            Direction = GameObject.Find("GorillaPlayer").GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Direction.gameObject.transform.position.y > 2000 || Direction.gameObject.transform.position.y > -2000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }
            if (Direction.gameObject.transform.position.x > 2000 || Direction.gameObject.transform.position.x > -2000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }
            if (Direction.gameObject.transform.position.z > 2000 || Direction.gameObject.transform.position.z > -2000f)
            {
                quitbox.GetComponent<Tphome>().OnBoxTriggered();
            }

        }

    }
    public class Tphome : GorillaTriggerBox
    {
        GorillaQuitBox quitbox;
        Rigidbody Direction;
        public override void OnBoxTriggered()
        {
            Direction.velocity = Vector3.zero;
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().transform.parent.position = new Vector3(-64f, 12.745f, -83.04f);
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
        }
        void Awake()
        {
            Direction = GameObject.Find("GorillaPlayer").GetComponent<Rigidbody>();
            quitbox = gameObject.GetComponent<GorillaQuitBox>();    
            Destroy(quitbox);
        }
    }
}
