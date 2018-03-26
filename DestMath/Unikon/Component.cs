using System;
using System.Collections;
using System.Collections.Generic;

namespace Unikon.UnityEngine
{
    public class Component : Object
    {
        internal bool isStarted = false;        
        internal bool isDestroyed = false;

        public GameObject gameObject { get; internal set; }

        public Transform transform
        {
            get { return gameObject.transform; }
        }

        public string tag
        {
            get { return gameObject.tag; }
            set { gameObject.tag = value; }
        }

//        public T AddComponent<T>() where T:Component
//        {
//            return gameObject.AddComponent<T>();
//        }

        public Component GetComponent(Type type)
        {
            return gameObject.GetComponent(type);
        }

        public T GetComponent<T>()
        {
            return gameObject.GetComponent<T>();
        }

        public Component GetComponent(string type)
        {
            return gameObject.GetComponent(type);
        }

        public Component GetComponentInChildren(System.Type t, bool includeInactive)
        {
            return gameObject.GetComponentInChildren(t, includeInactive);
        }

        public Component GetComponentInChildren(System.Type t)
        {
            return GetComponentInChildren(t, false);
        }

        public T GetComponentInChildren<T>()
        {
            return GetComponentInChildren<T>(false);
        }

        public T GetComponentInChildren<T>(bool includeInactive)
        {
            return (T)(object)GetComponentInChildren(typeof (T), includeInactive);
        }

        public Component[] GetComponentsInChildren(System.Type t)
        {
            bool includeInactive = false;
            return GetComponentsInChildren(t, includeInactive);
        }

        public Component[] GetComponentsInChildren(System.Type t, bool includeInactive)
        {
            return gameObject.GetComponentsInChildren(t, includeInactive);
        }

        public T[] GetComponentsInChildren<T>(bool includeInactive)
        {
            return gameObject.GetComponentsInChildren<T>(includeInactive);
        }

        public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
        {
            gameObject.GetComponentsInChildren<T>(includeInactive, result);
        }

        public T[] GetComponentsInChildren<T>()where T:Component
        {
            return GetComponentsInChildren<T>(false);
        }

        public void GetComponentsInChildren<T>(List<T> results)
        {
            GetComponentsInChildren<T>(false, results);
        }

        public Component GetComponentInParent(System.Type t)
        {
            return gameObject.GetComponentInParent(t);
        }

        public T GetComponentInParent<T>() where T:Component
        {
            return (T) GetComponentInParent(typeof (T));
        }

        public Component[] GetComponentsInParent(System.Type t)
        {
            bool includeInactive = false;
            return GetComponentsInParent(t, includeInactive);
        }

        public Component[] GetComponentsInParent(System.Type t, bool includeInactive)
        {
            return gameObject.GetComponentsInParent(t, includeInactive);
        }

        public T[] GetComponentsInParent<T>(bool includeInactive)
        {
            return gameObject.GetComponentsInParent<T>(includeInactive);
        }

        public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
        {
            gameObject.GetComponentsInParent<T>(includeInactive, results);
        }

        public T[] GetComponentsInParent<T>()
        {
            return GetComponentsInParent<T>(false);
        }

        public Component[] GetComponents(System.Type type)
        {
            return gameObject.GetComponents(type);
        }

        public void GetComponents(System.Type type, List<Component> results)
        {
            gameObject.GetComponents(type, results);
        }

        public void GetComponents<T>(List<T> results) 
        {
            gameObject.GetComponents<T>(results);
        }

        public T[] GetComponents<T>()
        {
            return gameObject.GetComponents<T>();
        }

        public bool CompareTag(string tag)
        {
            return gameObject.CompareTag(tag);
        }
    }
}
