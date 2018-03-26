using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Unikon.UnityEngine
{
    internal class ComponentList
    {
        // TODO @jian, 这里改为链表结构
        public readonly List<Component> components = new List<Component>();
        public Type type { get; private set; }

        public MethodInfo awakeMethod { get; private set; }
        public MethodInfo startMethod { get; private set; }
        public MethodInfo updateMethod { get; private set; }        
        public MethodInfo lateUpdateMethod { get; private set; }
        public MethodInfo onEnableMethod { get; private set; }
        public MethodInfo onDisableMethod { get; private set; }
        public MethodInfo onDestroyMethod { get; private set; }

#if SERVER
        public delegate void MethodCall(Component component);
        
        public MethodCall awakeCall { get; private set; }
        public MethodCall startCall { get; private set; }
        public MethodCall updateCall { get; private set; }        
        public MethodCall lateUpdateCall { get; private set; }
        public MethodCall onEnableCall { get; private set; }
        public MethodCall onDisableCall { get; private set; }
        public MethodCall onDestroyCall { get; private set; }
#endif        
        
        
        public bool isMonoBehaviour { get; private set; }
        
        
        public ComponentList(Type componentType)
        {
            // @qiujian TODO
            //Assert.IsNotNull(componentType);
            
            this.type = componentType;

            InitializeMethods();
        }

        private void InitializeMethods()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic ;
            
            awakeMethod = GetMethodFlattenHiearchy(type, "Awake", bindingFlags);
            startMethod = GetMethodFlattenHiearchy(type, "Start", bindingFlags);
            updateMethod = GetMethodFlattenHiearchy(type, "Update", bindingFlags);
            lateUpdateMethod = GetMethodFlattenHiearchy(type, "LateUpdate", bindingFlags);
            onEnableMethod = GetMethodFlattenHiearchy(type, "OnEnable", bindingFlags);
            onDisableMethod = GetMethodFlattenHiearchy(type, "OnDisable", bindingFlags);
            onDestroyMethod = GetMethodFlattenHiearchy(type, "OnDestroy", bindingFlags);

#if SERVER
            if (awakeMethod != null)
                awakeCall = CreateDelegateFromMethodInfo(type, awakeMethod);
            if (startMethod != null)
                startCall = CreateDelegateFromMethodInfo(type, startMethod);
            if (updateMethod != null)
                updateCall = CreateDelegateFromMethodInfo(type, updateMethod);
            if (lateUpdateMethod != null)
                lateUpdateCall = CreateDelegateFromMethodInfo(type, lateUpdateMethod);
            if (onEnableMethod != null)
                onEnableCall = CreateDelegateFromMethodInfo(type, onEnableMethod);
            if (onDisableMethod != null)
                onDisableCall = CreateDelegateFromMethodInfo(type, onDisableMethod);
            if (onDestroyMethod != null)
                onDestroyCall = CreateDelegateFromMethodInfo(type, onDestroyMethod);
#endif

            
            isMonoBehaviour = typeof(MonoBehaviour).IsAssignableFrom(type);
        }

        public static MethodInfo GetMethodFlattenHiearchy(Type type, string methodName, BindingFlags bindingFlags)
        {
            var method = type.GetMethod(methodName, bindingFlags);
            if (method == null && type.BaseType != null)
            {
                return GetMethodFlattenHiearchy(type.BaseType, methodName, bindingFlags);
            }

            return method;
        }

#if SERVER        
        public static MethodCall CreateDelegateFromMethodInfo(Type type, MethodInfo methodInfo)
        {
            var inputExpr = Expression.Parameter(typeof(Component));
            var lambdaExpr =
                Expression.Lambda<MethodCall>(Expression.Call(Expression.Convert(inputExpr, type), methodInfo),
                    inputExpr);
            var exprCall = lambdaExpr.Compile();

            return exprCall;
        }
#endif        

        public void Add(Component component)
        {
            components.Add(component);
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public void InvokeAwake(Component component)
        {
#if SERVER
            if (awakeCall != null)
                awakeCall.Invoke(component);
#else            
            if (awakeMethod != null)
                awakeMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeStart(Component component)
        {
#if SERVER
            if (startCall != null)
                startCall.Invoke(component);
#else            
            if (startMethod != null)
                startMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    

            component.isStarted = true;
        }
        
        public void InvokeUpdate(Component component)
        {
#if SERVER
            if (updateCall != null)
                updateCall.Invoke(component);
#else            
            if (updateMethod != null)
                updateMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeLateUpdate(Component component)
        {
#if SERVER
            if (lateUpdateCall != null)
                lateUpdateCall.Invoke(component);
#else            
            if (lateUpdateMethod != null)
                lateUpdateMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeOnEnable(Component component)
        {
#if SERVER
            if (onEnableCall != null)
                onEnableCall.Invoke(component);
#else            
            if (onEnableMethod != null)
                onEnableMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeOnDisable(Component component)
        {
#if SERVER
            if (onDisableCall != null)
                onDisableCall.Invoke(component);
#else            
            if (onDisableMethod != null)
                onDisableMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeOnDestroy(Component component)
        {
#if SERVER
            if (onDestroyCall != null)
                onDisableCall.Invoke(component);
#else            
            if (onDestroyMethod != null)
                onDestroyMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
        }
        
        public void InvokeOnEnableAll()
        {
            if (!isMonoBehaviour)
                return;                     
                
            foreach (var component in components)
            {
                var monoBehaviour = component as MonoBehaviour;
                if ((monoBehaviour.enableChanged || !monoBehaviour.isStarted) && monoBehaviour.enabled)
                {
#if SERVER
                    if (onEnableCall != null)
                        onEnableCall.Invoke(component);
#else
                    if (onEnableMethod != null)
                        onEnableMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
                    monoBehaviour.enableChanged = false;
                }                                                                                        
            }    
        }
        
        public void InvokeOnDisableAll()
        {
            if (!isMonoBehaviour)
                return;
            
            
            foreach (var component in components)
            {
                var monoBehaviour = component as MonoBehaviour;
                if (monoBehaviour.enableChanged && !monoBehaviour.enabled)
                {
#if SERVER
                    if (onDisableCall != null)
                        onDisableCall.Invoke(component);
#else
                    if (onDisableMethod != null)
                        onDisableMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
                    monoBehaviour.enableChanged = false;
                }                                                                                        
            }    
                
        }
        
        public void InvokeOnDestroyAll()
        {            
            if (!isMonoBehaviour)
                return;

#if SERVER
            if (onDestroyCall == null)
                return;
#else            
            if (onDestroyMethod == null)
                return;
#endif    
            foreach (var component in components)
            {
                var monoBehaviour = component as MonoBehaviour;
                if (monoBehaviour.isDestroyed)
                {
#if SERVER
                    onDestroyCall.Invoke(component);
#else                    
                    onDestroyMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
                }                                                                                        
            }                
        }


        public void InvokeStartAll()
        {
            foreach (var component in components)
            {
                if (component.isDestroyed)
                    continue;
                
                if (component.isStarted)
                    continue;
                
                if (!CheckEnable(component))
                    continue;

#if SERVER
                if (startCall != null)
                    startCall.Invoke(component);
#else
                if (startMethod != null)                
                    startMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
                component.isStarted = true;
            }                
        }

        public void InvokeUpdateAll()
        {
#if SERVER
            if (updateCall == null)
                return;
#else            
           if (updateMethod == null)
                return;
#endif    
                        
            foreach (var component in components)
            {
                if (component.isDestroyed)
                    continue;
                
                if (!CheckEnable(component))
                    continue;
                
                if (!component.isStarted)
                    continue;

#if SERVER
                updateCall.Invoke(component);
#else                
                updateMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
            }    
            
        }

        public void InvokeLateUpdateAll()
        {
#if SERVER
            if (lateUpdateCall == null)
                return;
#else            
            if (lateUpdateMethod == null)
                return;
#endif    
            
            foreach (var component in components)
            {
                if (component.isDestroyed)
                    continue;
                                
                if (!CheckEnable(component))
                    continue;
                
                if (!component.isStarted)
                    continue;
                

#if SERVER
                lateUpdateCall.Invoke(component);
#else                
                lateUpdateMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
#endif    
            }
        }

        public void RemoveDestroyed()
        {
            // TODO @jian, 这里改为链表结构 
            for (var i = components.Count - 1; i >= 0; i--)
            {
                var component = components[i];
                if (component.isDestroyed)
                {
                    components.RemoveAt(i);
                }
            }
        }

        private bool CheckEnable(Component component)
        {
            return isMonoBehaviour && (component as MonoBehaviour).enabled;
        }
    }
}