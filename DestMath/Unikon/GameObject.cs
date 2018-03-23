using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
    public sealed class GameObject : Object
    {
        public  Transform transform
        {
            get;
        }

        public  int layer
        {
            get;
            set;
        }
    }
}
