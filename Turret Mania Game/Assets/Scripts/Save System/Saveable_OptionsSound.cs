using UnityEngine;

namespace TurretGame
{
    public class Saveable_OptionsSound : MonoBehaviour, ISaveable
    {
        public SliderData[] DataOptions;
        public object CaptureState()
        {
            return new AllOptionData { DataTest = DataOptions };
        }

        public void RestoreState(object state)
        {
            var newDatas = (AllOptionData)state;

            DataOptions = newDatas.DataTest;
        }

        [System.Serializable]
        private struct AllOptionData
        {
            public SliderData[] DataTest;
        }

        [System.Serializable]
        public struct SliderData
        {
            public string s_OptionName;
            public float f_OptionSet;
        }
    }
}
