using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugs
{
    public class GlobalDebugs
    {
        public static void DebugPM(Object DebugObject, string Message)
        {
            Debug.Log(DebugObject.name + ": " + Message);
        }
    }
}
