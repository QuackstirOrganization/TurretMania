using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurretGame
{
    public abstract class PlayerSelectButton : SelectOption_SO
    {
        [Header("Player Customization")]
        public int Cost;
    }
}
