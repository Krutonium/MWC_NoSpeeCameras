using System.Linq;
using MSCLoader;
using HutongGames.PlayMaker;
using UnityEngine;

namespace NoSpeedCameras {
    public class NoSpeedCameras : Mod {
        public override string ID => "NoSpeedCameras"; // Your (unique) mod ID 
        public override string Name => "No Speed Cameras!"; // Your mod name
        public override string Author => "Krutonium"; // Name of the Author (your name)
        public override string Version => "1.0"; // Version
        public override string Description => "Remove the Traffic Cameras!"; // Short description of your mod
        public override Game SupportedGames => Game.MyWinterCar; //Supported Games
        public override void ModSetup()
        {
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.ModSettings, Mod_Settings);
        }

        private static SettingsCheckBox camerasEnabled;
        private void Mod_Settings()
        {
            Settings.AddText("Enabled doesn't mean Active; It just means the games normal logic applies.");
            camerasEnabled = Settings.AddCheckBox("SpeedCamsEnabled", "Speed Cameras Enabled", false);
            Settings.AddButton("Apply", Update);
        }

        private static GameObject SpeedCams;
        private void Mod_OnLoad()
        {
           SpeedCams = GameObject.Find("TRAFFIC").gameObject.transform.Find("SpeedCams").gameObject;
           Update();
        }

        private void Update()
        {
            if (SpeedCams != null)
            {
                if (camerasEnabled.GetValue()) // If they should be enabled
                {
                    SpeedCams.SetActive(true);
                    SpeedCams.GetPlayMaker("Database").enabled = true;
                }
                else
                {
                    SpeedCams.SetActive(false);
                    SpeedCams.GetPlayMaker("Database").enabled = false;
                }
            }
            else
            {
                ModConsole.Error("I CAN NOT FIND THE CAMERAS!!!");
            }
        }
    }
}