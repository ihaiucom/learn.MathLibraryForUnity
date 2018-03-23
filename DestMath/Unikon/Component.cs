using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace UnityEngine
{
    public class Component : Object
    {
        public string name
        {
            get; set;
        }

        public Transform transform
        {
            get;
        }


        public GameObject gameObject
        {
            get;
        }

    }
}
