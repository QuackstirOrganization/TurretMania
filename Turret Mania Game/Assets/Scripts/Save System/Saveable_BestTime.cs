using UnityEngine;

namespace TurretGame
{
    public class Saveable_BestTime : MonoBehaviour, ISaveable
    {
        public int bestTime;
        public int StarsCollected;
        public int Collisions;

        public object CaptureState()
        {
            return new BestTime
            {
                i_BestTime = bestTime,
                i_StarsCollected = StarsCollected,
                i_Collisions = Collisions,
            };
        }


        public void RestoreState(object state)
        {
            var BestTimeData = (BestTime)state;

            bestTime = BestTimeData.i_BestTime;
            StarsCollected = BestTimeData.i_StarsCollected;
            Collisions = BestTimeData.i_Collisions;
        }
        [System.Serializable]
        private struct BestTime
        {
            public int i_BestTime;
            public int i_StarsCollected;
            public int i_Collisions;
        }
    }
}
