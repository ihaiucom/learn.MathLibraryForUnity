using System;
using System.Collections.Generic;
using System.Linq;


namespace Unikon.UnityEngine
{
    public class Scene
    {
        private readonly List<GameObject> m_RootGameObjects;

        public List<GameObject> rootGameObjects
        {
            get { return m_RootGameObjects; }
        }
        
        private readonly Dictionary<Type, ComponentList> m_ComponentMap;
        private ComponentList[] cachedComponentList;
        private bool needUpdateCacheComponentList = true;
        
        
        public Scene()
        {            
            m_ComponentMap = new Dictionary<Type, ComponentList>();        
            m_RootGameObjects = new List<GameObject>();
        }

        internal ComponentList GetComponentList(Type type)
        {            
            ComponentList componentList;

            if (m_ComponentMap.TryGetValue(type, out componentList))
            {
                return componentList;
            }

            return null;
        }

        internal void AddGameObject(GameObject gameObject)
        {          
            if (gameObject.scene != null)
            {
                // TODO
                throw new Exception("");
            }

            gameObject.scene = this;

            foreach (var component in gameObject.components)
            {
                AddComponent(component);
            }

            if (gameObject.transform.parent == null)
            {
                AddToRootGameObjects(gameObject);
            }
        }
        
        internal void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject.scene != this)
                throw new System.Exception("");

            gameObject.scene = null;

            if (gameObject.transform.parent == null)
            {
                RemoveFromRootGameObjects(gameObject);
            }

            foreach (var component in gameObject.components)
            {
                // 标记为移除
                component.isDestroyed = true;
            }
        }

        internal void AddComponent(Component component)
        {
            var type = component.GetType();

            ComponentList componentList;

            if (!m_ComponentMap.TryGetValue(type, out componentList))
            {
                componentList = new ComponentList(type);
                m_ComponentMap[type] = componentList;
                needUpdateCacheComponentList = true;
            }

            componentList.Add(component);            
            componentList.InvokeAwake(component);
        }

        private void UpdateCachedComponentLists()
        {
            if (needUpdateCacheComponentList)
            {
                cachedComponentList = new ComponentList[m_ComponentMap.Count];
                m_ComponentMap.Values.CopyTo(cachedComponentList, 0);
                needUpdateCacheComponentList = false;    
            }
            
        }

        public void Update()
        {
            // Copy列表，避免迭代时修改
            UpdateCachedComponentLists();
            
            // Enable
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeOnEnableAll();
            }
            
            // Start
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeStartAll();                
            }

            // Update
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeUpdateAll();
            }
            
            // LateUpdate
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeLateUpdateAll();
            }
            
            // Disable
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeOnDisableAll();
            }
            
            // OnDestroy
            foreach (var componentList in cachedComponentList)
            {
                componentList.InvokeOnDestroyAll();
            }
            
            // Destory
            foreach (var componentList in cachedComponentList)
            {
                componentList.RemoveDestroyed();
            }             
        }

        public int rootCount
        {
            get { return m_RootGameObjects.Count; }
        }

        public GameObject[] GetRootGameObjects()
        {            
            return m_RootGameObjects.ToArray();
        }

        public void GetRootGameObjects(List<GameObject> rootGameObjects)
        {
            if (rootGameObjects.Capacity < this.rootCount)
                rootGameObjects.Capacity = this.rootCount;
            rootGameObjects.Clear();

            if (this.rootCount == 0)
                return;

            rootGameObjects.AddRange(m_RootGameObjects);                        
        }
                

        internal void RemoveFromRootGameObjects(GameObject gameObject)
        {
            m_RootGameObjects.Remove(gameObject);
        }

        internal void AddToRootGameObjects(GameObject gameObject)
        {
            m_RootGameObjects.Add(gameObject);
        }
    }
}
