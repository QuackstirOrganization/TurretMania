using UnityEngine;

namespace TurretGame
{
    public class Saveable_Options : MonoBehaviour, ISaveable
    {
        [SerializeField] private string optionName = "suk";
        [SerializeField] private float optionSet = 1f;

        public object CaptureState()
        {
            return new OptionsData
            {
                s_OptionName = optionName,
                f_OptionSet = optionSet
            };

        }

        public void RestoreState(object state)
        {
            var optionsData = (OptionsData)state;

            optionName = optionsData.s_OptionName;
            optionSet = optionsData.f_OptionSet;
        }

        [System.Serializable]
        private struct OptionsData
        {
            public string s_OptionName;
            public float f_OptionSet;
        }

    }
}
