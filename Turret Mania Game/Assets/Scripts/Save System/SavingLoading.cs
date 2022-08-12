using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TurretGame
{
    public class SavingLoading : MonoBehaviour
    {
        private static SavingLoading instance;
        public static SavingLoading Instance
        {
            // Get the functions
            get
            {
                // Check if Instance is not there
                if (instance == null)
                {
                    // If not then find a object with the component PauseManager
                    instance = FindObjectOfType<SavingLoading>();
                }
                // Return the Instance
                return instance;
            }

            // Private set so other components cant modify this script
            private set
            {
                instance = value;
            }
        }
        private void Start()
        {
            instance = this;
        }

        private string SavePath => $"{Application.persistentDataPath}/save.txt";

        [ContextMenu("Save")]
        public void Save()
        {
            Debug.Log("Saving Text File");
            var state = LoadFile();
            CaptureState(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        public void Load()
        {
            Debug.Log("Loading Text File");
            var state = LoadFile();
            RestoreState(state);
        }

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(SavePath))
            {
                return new Dictionary<string, object>();
            }

            using (FileStream stream = File.Open(SavePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(object state)
        {
            using (var stream = File.Open(SavePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        private void CaptureState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.Id] = saveable.CaptureState();
            }
        }

        public void RestoreState(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            {
                if (state.TryGetValue(saveable.Id, out object value))
                {
                    saveable.RestoreState(value);
                    Debug.Log(value);
                }
            }
        }
    }
}
