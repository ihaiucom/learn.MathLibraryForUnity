using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using MsgPack;
using MsgPack.Serialization;
using ParadoxNotion.Serialization.FullSerializer;


namespace Unikon.UnityEngine
{
    public class Object
    {
        #region Unikon
        internal MessagePackObject mpo;
        #endregion
        
        // private int m_InstanceID;        

        public string name { get;  set; }

        public HideFlags hideFlags { get; set; }

//        public static implicit operator bool(Object exists)
//        {
//            return !Object.CompareBaseObjects(exists, (Object) null);
//        }
//
//        public static bool operator ==(Object x, Object y)
//        {
//            return Object.CompareBaseObjects(x, y);
//        }
//
//        public static bool operator !=(Object x, Object y)
//        {
//            return !Object.CompareBaseObjects(x, y);
//        }


        public static void Destroy(Object obj)
        {
            if (obj is GameObject)
            {
                var gameObject = obj as GameObject;
                if (gameObject.scene != null)
                {
                    gameObject.scene.RemoveGameObject(gameObject);
                }
            }
            else
            {
                Debug.LogWarning("Destory " + obj.GetType().Name + " is not supported!");                
            }
        }

        public static void DestroyImmediate(Object obj)
        {
            throw new NotImplementedException();
        }

        public static void DontDestroyOnLoad(Object obj)
        {
            // throw new NotImplementedException();
        }

        public static Object[] FindSceneObjectsOfType(System.Type type)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.name;            
        }

        // public int GetInstanceID()
        // {
        //     return this.m_InstanceID;
        // }

//        public override bool Equals(object o)
//        {
//            return Object.CompareBaseObjects(this, o as Object);
//        }

        // private static bool CompareBaseObjects(Object lhs, Object rhs)
        // {
        //     return lhs == rhs;
//            if (lhs == null && rhs == null)
//                return true;
//            if (lhs != null && rhs != null)
//                return lhs.m_InstanceID == rhs.m_InstanceID;
//
//            return false;
        // }



//        public static T DeepClone<T>(T obj)
//        {
//            using (var ms = new MemoryStream())
//            {
//                var formatter = new BinaryFormatter();
//                formatter.Serialize(ms, obj);
//                ms.Position = 0;
//
//                return (T) formatter.Deserialize(ms);
//            }
//        }
        
        public static Object Instantiate(Object original)
        {
            Object.CheckNullArgument((object) original, "The Object you want to instantiate is null.");

            var gameObject = original as GameObject;
            if (gameObject != null)
            {
                SceneManager.dontAddGameObject = true;
            }
                                    
            var serializer = UnikonEngine.serializationContext.GetSerializer(original.GetType());            
            MessagePackObject mpo = serializer.ToMessagePackObject(original);
            var clone = serializer.FromMessagePackObject(mpo);
            
            if (gameObject != null)
            {
#if DEBUG_UNIKON
                Debug.Log("GameObject Instantiate");
                Debug.Log(mpo.ToString());
                
                
                foreach (var component in (clone as GameObject).components)
                {
                    Debug.Log(component.GetType().Name);
                }
#endif
                
                SceneManager.dontAddGameObject = false;
                SceneManager.AddGameObject(clone as GameObject);

//                gameObject.dontAwake = false;
//                foreach (var component in gameObject.components)
//                {
//                    var awakeMethod = component.GetType().GetMethod("Awake",
//                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
//                    if (awakeMethod != null)
//                    {
//                        Debug.Log("Invoke Awake " + component.GetType());
//                        awakeMethod.Invoke(component, BindingFlags.InvokeMethod, null, null, CultureInfo.CurrentCulture);
//                    }
//                        
//                }                
            }
            return (Object) clone;
        }

        public static T Instantiate<T>(T original) where T : Object
        {
            return (T)Instantiate(original as Object);
            
//            Object.CheckNullArgument((object) original, "The Object you want to instantiate is null.");
//            
//            var serializer = UnikonEngine.serializationContext.GetSerializer<T>();
//            
//            var mpo = serializer.ToMessagePackObject(original);
//            var clone = serializer.FromMessagePackObject(mpo);
//            return clone;
        }

        private static void CheckNullArgument(object arg, string message)
        {
            if (arg == null)
                throw new ArgumentException(message);
        }

        public static Object[] FindObjectsOfType(System.Type type)
        {
            throw new NotImplementedException();
        }

        public static T[] FindObjectsOfType<T>() where T : Object
        {
            throw new NotImplementedException();
            //return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof (T)));
        }

        public static Object FindObjectOfType(System.Type type)
        {
            if (UnikonEngine.currentEngine == null)
                return null;

            if (UnikonEngine.currentEngine.activeScene == null)
                return null;
            
            var componentList = UnikonEngine.currentEngine.activeScene.GetComponentList(type);
            if (componentList == null)
                return null;

            if (componentList.components.Count <= 0)
                return null;
            
            return (Object) componentList.components[0];
        }

        public static T FindObjectOfType<T>() where T : Object
        {
            return (T) Object.FindObjectOfType(typeof (T));
        }
    }
}
