using System;
using System.Collections;

namespace Unikon.UnityEngine
{
    public class MonoBehaviour : Component
    {
        internal bool enableChanged = false;

        private bool m_Enabled = true;

        public bool enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled == value)
                    return;

                m_Enabled = value;
                enableChanged = true;
            }
        }

        public bool isActiveAndEnabled
        {
            get { return m_Enabled && gameObject != null && gameObject.activeInHierarchy; }
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Starts a coroutine named methodName.</para>
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="value"></param>
        public Coroutine StartCoroutine(string methodName, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Starts a coroutine named methodName.</para>
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="value"></param>    
        public Coroutine StartCoroutine(string methodName)
        {
            object obj = (object) null;
            return this.StartCoroutine(methodName, obj);
        }

        /// <summary>
        ///   <para>Stops the first coroutine named methodName, or the coroutine stored in routine running on this behaviour.</para>
        /// </summary>
        /// <param name="methodName">Name of coroutine.</param>
        /// <param name="routine">Name of the function in code.</param>    
        public void StopCoroutine(string methodName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Stops the first coroutine named methodName, or the coroutine stored in routine running on this behaviour.</para>
        /// </summary>
        /// <param name="methodName">Name of coroutine.</param>
        /// <param name="routine">Name of the function in code.</param>
        public void StopCoroutine(IEnumerator routine)
        {
            throw new NotImplementedException();
        }

        public void StopCoroutine(Coroutine routine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Stops all coroutines running on this behaviour.</para>
        /// </summary>
        public void StopAllCoroutines()
        {
            throw new NotImplementedException();
        }
    }
}