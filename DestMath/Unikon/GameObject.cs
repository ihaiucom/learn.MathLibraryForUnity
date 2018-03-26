
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Unikon.UnityEngine
{    
    public sealed class GameObject : Object
    {
//        internal bool dontAwake = false;
        
        private Scene m_Scene;

        internal Scene scene
        {
            get { return m_Scene; }
            set
            {
                if (value == m_Scene)
                    return;
                if (value != null && m_Scene != null)
                    // TODO
                    throw new System.Exception("TODO");

                m_Scene = value;
            }
        }


        private readonly List<object> m_Components = new List<object>();

        internal IEnumerable<Component> components
        {
            get
            {
                // TODO @jian
                var components = new List<object>(m_Components);
                foreach (var componet in components)
                {
                    yield return componet as Component;
                }
            }
        }

        public T AddComponent<T>()
        {
            var component = Activator.CreateInstance<T>();

            return (T)AddComponent(component as Component);
        }

        public Component AddComponent(Type type)
        {
            var component = (Component)Activator.CreateInstance(type);

            return AddComponent(component) as Component;
        }

        internal object AddComponent(Component component)
        {
            if (component.gameObject == this)
                return component;

            if (component.gameObject != null)
                throw new Exception("Can't add componet");

            component.gameObject = this;
            m_Components.Add(component);
            if (scene != null)
                scene.AddComponent(component);

            return component;
        }

        public T GetComponent<T>()
        {
            
            for (var i = 0; i < m_Components.Count; i++)
            {
                if (m_Components[i] is T)
                {
                    return (T)m_Components[i];
                }
            }
            return default(T);
        }

        public T[] GetComponents<T>() 
        {
            var components = new List<T>();

            var count = m_Components.Count;
            for (var i = 0; i < count; i++)
            {
                if (m_Components[i] is T)
                {
                    components.Add((T)m_Components[i]);
                }
            }

            return components.ToArray();
        }

        public Component GetComponent(Type type)
        {
            var count = m_Components.Count;
            for (var i = 0; i < count; i++)
            {
                var component = m_Components[i];
                var componentType = component.GetType();
                if (type.IsAssignableFrom(componentType))
                    return component as Component;
            }
            return null;
        }

        private Transform m_Transform;

        public Transform transform
        {
            get { return m_Transform; }
        }

        public string tag { get; internal set; }

        public void Destroy()
        {
            Object.Destroy(this);            
        }



        // TODO: Layers.Default
        public int layer = 1;

        public bool activeSelf { get; private set; }

        public bool activeInHierarchy
        {
            get
            {
                if (!activeSelf)
                    return false;

                if (transform.parent != null)
                    return transform.parent.gameObject.activeInHierarchy;

                return true;
            }
        }

        public GameObject gameObject
        {
            get
            {
                return this;
            }
        }
        
        public GameObject()
        {
            m_Transform = AddComponent<Transform>();
            SceneManager.AddGameObject(this);                        
        }

        public GameObject(string name)
        {
            this.name = name;
            m_Transform = AddComponent<Transform>();
            SceneManager.AddGameObject(this);            
        }

        public GameObject(string name, params Type[] components)
        {
            this.name = name;
            m_Transform = AddComponent<Transform>();
            SceneManager.AddGameObject(this);

            foreach (Type component in components)
                AddComponent(component);
        }

        // TODO: @jian 这个需要测试
        public Component GetComponent(string type)
        {            
            foreach (var component in components)
            {
                if (component.GetType().Name == type)
                    return component as Component;
            }

            return null;
        }

        public Component GetComponentInChildren(Type type, bool includeInactive)
        {
            foreach (var component in components)
            {
                if (component.GetType() == type)
                    return component as Component;
            }
            
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeSelf || includeInactive)
                {
                    var component = child.gameObject.GetComponentInChildren(type, includeInactive);
                    if (component != null)
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        public Component GetComponentInChildren(Type type)
        {
            return GetComponentInChildren(type, false);
        }

        public T GetComponentInChildren<T>() 
        {
            return GetComponentInChildren<T>(false);
        }

        public T GetComponentInChildren<T>(bool includeInactive)
        {
            return (T) (object)GetComponentInChildren(typeof (T), includeInactive);
        }

        public Component GetComponentInParent(System.Type type)
        {
            if (transform.parent != null)
            {
                var component = transform.parent.gameObject.GetComponent(type);
                if (component != null)
                {
                    return component;
                }
                
                return transform.parent.gameObject.GetComponentInParent(type);
            }

            return null;            
        }

        public T GetComponentInParent<T>() 
        {
            return (T) (object)GetComponentInParent(typeof (T));
        }

        // TODO:@jian 这里临时使用的List是否可以做缓存
        public Component[] GetComponents(System.Type type)
        {
            var results = new List<Component>();
            GetComponents(type, results);
            return results.ToArray();            
        }

        public void GetComponents(System.Type type, List<Component> results)
        {
            foreach (var component in components)
            {
                if (component.GetType() == type)
                    results.Add(component as Component);
            }
        }

        public void GetComponents<T>(List<T> results)
        {
            foreach (var component in components)
            {
                if (component is T)
                {
                    results.Add((T)(object)component);
                }
            }            
        }

        public Component[] GetComponentsInChildren(System.Type type)
        {            
            return this.GetComponentsInChildren(type, false);
        }

        public Component[] GetComponentsInChildren(System.Type type, bool includeInactive)
        {
            var results = new List<Component>();

            GetComponentsInChildren(type, includeInactive, results);

            return results.ToArray();            
        }

        public void GetComponentsInChildren(Type type, bool includeInactive, List<Component> results)
        {
            if (gameObject.activeSelf || includeInactive)
                GetComponents(type, results);

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeSelf || includeInactive)
                {
                    child.gameObject.GetComponentsInChildren(type, includeInactive, results);
                }
            }
        }

        public T[] GetComponentsInChildren<T>(bool includeInactive)
        {
            var results = new List<T>();
            GetComponentsInChildren(includeInactive, results);
            return results.ToArray();
        }

        public void GetComponentsInChildren<T>(bool includeInactive, List<T> results) 
        {
            if (gameObject.activeSelf || includeInactive)
                GetComponents(results);

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.activeSelf || includeInactive)
                {
                    child.gameObject.GetComponentInChildren<T>(includeInactive);
                }
            }
        }

        public T[] GetComponentsInChildren<T>()
        {
            return this.GetComponentsInChildren<T>(false);
        }

        public void GetComponentsInChildren<T>(List<T> results)
        {
            this.GetComponentsInChildren<T>(false, results);
        }

        public Component[] GetComponentsInParent(System.Type type)
        {            
            return this.GetComponentsInParent(type, false);
        }

        /// <summary>
        ///   <para>Returns all components of Type type in the GameObject or any of its parents.</para>
        /// </summary>
        /// <param name="type">The type of Component to retrieve.</param>
        /// <param name="includeInactive">Should inactive Components be included in the found set?</param>
        public Component[] GetComponentsInParent(System.Type type, bool includeInactive)
        {
            var results = new List<Component>();
            GetComponentsInParent(type, includeInactive, results);
            return results.ToArray();
        }

        public void GetComponentsInParent(Type type, bool includeInactive, List<Component> results)
        {
            if (transform.parent != null && (transform.parent.gameObject.activeSelf || includeInactive))
            {
                transform.parent.gameObject.GetComponents(type, results);
                transform.parent.gameObject.GetComponentsInParent(type, includeInactive, results);    
            }
        }
        

        public void GetComponentsInParent<T>(bool includeInactive, List<T> results) 
        {
            if (transform.parent == null)
                return;

            if (transform.parent.gameObject.activeSelf || includeInactive)
            {
                transform.parent.gameObject.GetComponents<T>(results);
                transform.parent.gameObject.GetComponentsInParent(includeInactive, results);    
            }
        }

        public T[] GetComponentsInParent<T>(bool includeInactive)
        {
            var results = new List<T>();
            GetComponentsInParent<T>(includeInactive, results);
            return results.ToArray();
        }

        public T[] GetComponentsInParent<T>()
        {
            return this.GetComponentsInParent<T>(false);
        }

        public void SetActive(bool value)
        {
            if (activeSelf == value)
                return;
            
            activeSelf = value;            
        }


        public bool CompareTag(string tag)
        {
            return this.tag == tag;
        }

        public static GameObject FindGameObjectWithTag(string tag)
        {
            var scene = SceneManager.GetActiveScene();
            if (scene != null)
            {
                foreach (var root in scene.rootGameObjects)
                {
                    var found = FindTransformWithTag(tag, root.transform); 
                    if (found != null)
                    {
                        return found.gameObject;
                    }
                }
            }
            return null;
        }

        public static GameObject FindWithTag(string tag)
        {
            return GameObject.FindGameObjectWithTag(tag);
        }

        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            var foundList = new List<GameObject>();

            var scene = SceneManager.GetActiveScene();
            if (scene != null)
            {
                foreach (var root in scene.rootGameObjects)
                {
                    FindGameObjectsWithTag(tag, root, foundList);
                }
            }

            return foundList.ToArray();            
        }
        
        private static Transform FindTransformWithTag(string tag, Transform root)
        {
            if (root.tag == tag)
                return root;

            for (var i = 0; i < root.childCount; i++)
            {
                var child = root.GetChild(i);
                if (FindTransformWithTag(tag, child) != null)
                {
                    return child;
                }
            }

            return null;
        }
        
        private static void FindGameObjectsWithTag(string tag, GameObject root, List<GameObject> foundList)
        {
            if (root.tag == tag)
                foundList.Add(root);

            for (var i = 0; i < root.transform.childCount; i++)
            {
                var child = root.transform.GetChild(i);
                FindGameObjectsWithTag(tag, child.gameObject, foundList);
            }
        }

        internal void InvokeAwakeAll()
        {
            foreach (var component in components)
            {
                var awakeMethod = component.GetType().GetMethod("Awake",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (awakeMethod != null)
                {
                    Debug.Log("Invoke Awake " + component.GetType());
                    awakeMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
                }                        
            }                
        }
    }
}
