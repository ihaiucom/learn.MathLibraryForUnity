using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Push + Pull Hybrid Model
    /// Reference: http://gamedev.stackexchange.com/questions/32043/optimizing-hierarchical-transform
    /// </summary>
    public class Transform : Component, IEnumerable
    {

        #region Parenting

        private Transform _parent;

        public Transform parent
        {
            get { return _parent; }

            internal set {
                SetParent(value);
            }
        }

        public void SetParent(Transform parent)
        {
            this.SetParent(parent, true);
        }

        /// <summary>
        ///   <para>Set the parent of the transform.</para>
        /// </summary>
        /// <param name="parent">The parent Transform to use.</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        public void SetParent(Transform parent, bool worldPositionStays)
        {
            //if (_parent == parent)
            //    return;

            //if (_parent == null && gameObject.scene != null)
            //    gameObject.scene.RemoveFromRootGameObjects(gameObject);

            //if (_parent != null)
            //    _parent.RemoveChildInternal(this);     
            
            //_parent = parent;

            //if (_parent == null && gameObject.scene != null)
            //    gameObject.scene.AddToRootGameObjects(gameObject);
            
            //if (_parent != null)
            //    _parent.AddChildInternal(this);
            
            //if (worldPositionStays)
            //    WorldToLocal();
            
            //_localDirty = true;
            //MarkDirty();
        }

        private void WorldToLocal()
        {
            if (parent == null)
            {
                localPosition = _position;
                localRotation = _rotation;
            }
            else
            {
                localPosition = worldToLocalMatrix.MultiplyPoint3x4(_position);
                localRotation = _rotation * Quaternion.Inverse(_parent.rotation);
            }
        }

        /// <summary>
        ///   <para>Returns the topmost transform in the hierarchy.</para>
        /// </summary>
        public Transform root { get; private set; }

        /// <summary>
        ///   <para>The number of children the Transform has.</para>
        /// </summary>
        public int childCount {
            get { return children.Count; } 
        }

        public IEnumerator GetEnumerator()
        {
            return this.children.GetEnumerator();
        }

        private List<Transform> children = new List<Transform>(0);

        internal void AddChildInternal(Transform child)
        {
            if (child.parent != this)
            {
                child.parent.RemoveChildInternal(child);
            }

            child._parent = this;
            children.Add(child);
        }

        internal void RemoveChildInternal(Transform child)
        {
            if (child.parent != this)
                return;

            child._parent = null;
            children.Remove(child);
        }

        /// <summary>
        ///   <para>Unparents all children.</para>
        /// </summary>
        public void DetachChildren()
        {
            foreach (var child in children)
            {
                child.parent = null;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Move the transform to the start of the local transform list.</para>
        /// </summary>
        public void SetAsFirstSibling()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Move the transform to the end of the local transform list.</para>
        /// </summary>
        public void SetAsLastSibling()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Sets the sibling index.</para>
        /// </summary>
        /// <param name="index">Index to set.</param>
        public void SetSiblingIndex(int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Gets the sibling index.</para>
        /// </summary>
        public int GetSiblingIndex()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Finds a child by name and returns it.</para>
        /// </summary>
        /// <param name="name">Name of child to be found.</param>
        public Transform Find(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            
            string nodeName;
            string childPath = null;
            
            var splitIndex = name.IndexOf('/');
                        
            if (splitIndex == -1)
            {
                nodeName = name;
            }
            else
            {
                nodeName = name.Substring(0, splitIndex+1);
                childPath = name.Substring(splitIndex+1);
            }

            foreach (var node in children)
            {
                if (node.name != nodeName) continue;
                
                if (string.IsNullOrEmpty(childPath))
                {
                    return node;
                }
                else
                {
                    return node.Find(childPath);
                }
            }

            return null;
        }

        /// <summary>
        ///   <para>Is this transform a child of parent?</para>
        /// </summary>
        /// <param name="parent"></param>
        public bool IsChildOf(Transform parent)
        {
            return _parent == parent;            
        }

        public Transform FindChild(string name)
        {
            return this.Find(name);
        }

        /// <summary>
        ///   <para>Returns a transform child by index.</para>
        /// </summary>
        /// <param name="index">Index of the child transform to return. Must be smaller than Transform.childCount.</param>
        /// <returns>
        ///   <para>Transform child by index.</para>
        /// </returns>
        public Transform GetChild(int index)
        {
            return children[index];            
        }

        #endregion

        #region RTS

        private bool _positionDirty = false;

        private Vector3 _position;

        public Vector3 position{
            get
            {
                UpdateTransform();
                if (_positionDirty)
                {
                    if (parent == null)
                    {
                        _position = _localPosition;
                    }
                    else
                    {
                        parent.UpdateTransform();
                        _position = parent.TransformPoint(_localPosition);
                    }
                    _positionDirty = false;
                }
                return _position;
            }
            set
            {
                if (_position == value)
                    return;

                _position = value;

                if (parent == null)
                {
                    localPosition = _position;
                }
                else
                {
                    localPosition = worldToLocalMatrix.MultiplyPoint3x4(_position);
                }

                _positionDirty = false;
            }
        }

        private bool _localDirty = false;

        private Vector3 _localPosition;

        public Vector3 localPosition
        {
            get
            {
                UpdateTransform();
                return _localPosition;
            }
            set
            {
                if (_localPosition == value)
                    return;
                
                _localPosition = value;
                _positionDirty = true;
                _localDirty = true;

                MarkDirty();
            }
        }



        /// <summary>
        ///   <para>The rotation of the transform in world space stored as a Quaternion.</para>
        /// </summary>
        public Quaternion rotation
        {
            get
            {
                UpdateTransform();
                return _rotation;
            }
            set
            {
                _rotation = value;

                if (_parent == null)
                {
                    localRotation = _rotation;
                }
                else
                {                    
                    localRotation = _rotation * Quaternion.Inverse(_parent.rotation);
                }
            }
        }
        
        private Quaternion _rotation;


        /// <summary>
        ///   <para>The rotation of the transform relative to the parent transform's rotation.</para>
        /// </summary>
        public Quaternion localRotation
        {
            get
            {
                UpdateTransform();

                return _localRotation;
            }
            set
            {
                if (_localRotation == value)
                    return;

                _localRotation = value;

                if (_parent == null)
                {
                    _rotation = _localRotation;
                }
                else
                {
                    _rotation = _parent.rotation * _localRotation;
                }

                _localDirty = true;
                _positionDirty = true;
                MarkDirty();
            }
        }
        
        private Quaternion _localRotation;

        //internal RotationOrder rotationOrder { [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall, MethodImpl(MethodImplOptions.InternalCall)] set; }


        /// <summary>
        ///   <para>The scale of the transform relative to the parent.</para>
        /// </summary>
        public Vector3 localScale
        {
            get
            {
                UpdateTransform();

                return _localScale;
            }
            set
            {
                if (_localScale == value)
                    return;

                _localScale = value;
                
                _positionDirty = true;
                _localDirty = true;
                MarkDirty();
            }
        }
        
        private Vector3 _localScale = Vector3.one;

//        /// <summary>
//        ///   <para>The global scale of the object (Read Only).</para>
//        /// </summary>        
//        public Vector3 lossyScale {
//            get
//            {
//                UpdateTransform();
//                return _scale;                
//            } 
//        }
//
//        private Vector3 _scale = Vector3.one;

        public Vector3 eulerAngles
        {
            get
            {
                return this.rotation.eulerAngles;
            }
            set
            {
                this.rotation = Quaternion.Euler(value);
            }
        }

        public Vector3 localEulerAngles
        {
            get
            {
                return this.localRotation.eulerAngles;
            }
            set
            {
                this.localRotation = Quaternion.Euler(value);
            }
        }



        #endregion

        #region Matrix



        /// <summary>
        /// Matrix 修改的标志，当
        /// </summary>

        private bool _isDirty;

        internal bool isDirty
        {
            get { return _isDirty; }
        }

        internal void MarkDirty()
        {
            _isDirty = true;
            
            foreach (var child in children)
            {
                child.MarkDirty();
            }
        }

        
        /// <summary>
        ///   <para>Matrix that transforms a point from world space into local space (Read Only).</para>
        /// </summary>
        private Matrix4x4 _worldToLocalMatrix = Matrix4x4.identity;

        public Matrix4x4 worldToLocalMatrix
        {
            get
            {
                if (_worldToLocalDirty)
                {
                    if (parent == null)
                    {
                        _worldToLocalMatrix = Matrix4x4.identity;
                    }
                    else
                    {
                        parent.UpdateTransform();
                        _worldToLocalMatrix = Matrix4x4.Inverse(parent.localToWorldMatrix);
                    }
                    _worldToLocalDirty = false;
                }                
                
                return _worldToLocalMatrix;
            }
        }

        private bool _worldToLocalDirty = false;
        
        /// <summary>
        ///   <para>Matrix that transforms a point from local space into world space (Read Only).</para>
        /// </summary>
        private Matrix4x4 _localToWorldMatrix = Matrix4x4.identity;
        
        public Matrix4x4 localToWorldMatrix {
            get
            {
                UpdateTransform();
                return _localToWorldMatrix;
            }
        }

        private Matrix4x4 _localMatrix = Matrix4x4.identity;
        
        //private bool _localToWorldDirty = false;
        
     
        #endregion

        public Vector3 right
        {
            get
            {
                return this.rotation * Vector3.right;
            }
            set
            {
                this.rotation = Quaternion.FromToRotation(Vector3.right, value);
            }
        }

        public Vector3 up
        {
            get
            {
                return this.rotation * Vector3.up;
            }
            set
            {
                this.rotation = Quaternion.FromToRotation(Vector3.up, value);
            }
        }
        
        internal void UpdateTransform()
        {
            if (_isDirty)
            {
                if (parent != null)                
                    parent.UpdateTransform();
                
                if (_localDirty)
                {                        
                    _localMatrix = Matrix4x4.TRS(_localPosition, _localRotation, _localScale);                    
                    
                    if (parent == null)
                    {
                        _localToWorldMatrix = _localMatrix;
                        _rotation = _localRotation;
//                        _scale = _localScale;
                    }

                    _localDirty = false;
                }
                
                if (parent != null)
                {
                    _localToWorldMatrix = _localMatrix * parent.localToWorldMatrix;
                    _rotation = parent.rotation * _localRotation;
//                    _scale = parent.TransformVector(_localScale);
                }

                _worldToLocalDirty = true;
                _positionDirty = true;
                _isDirty = false;
            }
        }

        /// <summary>
        ///   <para>The blue axis of the transform in world space.</para>
        /// </summary>
        public Vector3 forward
        {
            get
            {
                return this.rotation * Vector3.forward;
            }
            set
            {
                this.rotation = Quaternion.LookRotation(value);
            }
        }



        /// <summary>
        ///   <para>Has the transform changed since the last time the flag was set to 'false'?</para>
        /// </summary>
        public bool hasChanged { get; set; }

        /// <summary>
        ///   <para>The transform capacity of the transform's hierarchy data structure.</para>
        /// </summary>
        public int hierarchyCapacity {  get;  set; }

        /// <summary>
        ///   <para>The number of transforms in the transform's hierarchy data structure.</para>
        /// </summary>
        public int hierarchyCount { get; private set; }




        internal Vector3 GetLocalEulerAngles(RotationOrder order)
        {
            Vector3 vector3;
            Transform.INTERNAL_CALL_GetLocalEulerAngles(this, order, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_GetLocalEulerAngles(Transform self, RotationOrder order, out Vector3 value)
        {
            throw new NotImplementedException();
        }

        internal void SetLocalEulerAngles(Vector3 euler, RotationOrder order)
        {
            Transform.INTERNAL_CALL_SetLocalEulerAngles(this, ref euler, order);
        }

        private static void INTERNAL_CALL_SetLocalEulerAngles(Transform self, ref Vector3 euler, RotationOrder order)
        {
            throw new NotImplementedException();
        }

        internal void SetLocalEulerHint(Vector3 euler)
        {
            Transform.INTERNAL_CALL_SetLocalEulerHint(this, ref euler);
        }

        private static void INTERNAL_CALL_SetLocalEulerHint(Transform self, ref Vector3 euler)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///   <para>Moves the transform in the direction and distance of translation.</para>
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="relativeTo"></param>
        public void Translate(Vector3 translation)
        {
            Space relativeTo = Space.Self;
            this.Translate(translation, relativeTo);
        }

        /// <summary>
        ///   <para>Moves the transform in the direction and distance of translation.</para>
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="relativeTo"></param>
        public void Translate(Vector3 translation, Space relativeTo)
        {
            if (relativeTo == Space.World)
                this.position += translation;
            else
                this.position += this.TransformDirection(translation);
        }

        /// <summary>
        ///   <para>Moves the transform by x along the x axis, y along the y axis, and z along the z axis.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="relativeTo"></param>
        public void Translate(float x, float y, float z)
        {
            Space relativeTo = Space.Self;
            this.Translate(x, y, z, relativeTo);
        }

        /// <summary>
        ///   <para>Moves the transform by x along the x axis, y along the y axis, and z along the z axis.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="relativeTo"></param>
        public void Translate(float x, float y, float z, Space relativeTo)
        {
            this.Translate(new Vector3(x, y, z), relativeTo);
        }

        /// <summary>
        ///   <para>Moves the transform in the direction and distance of translation.</para>
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="relativeTo"></param>
        public void Translate(Vector3 translation, Transform relativeTo)
        {
            if (relativeTo != null)
                this.position += relativeTo.TransformDirection(translation);
            else
                this.position += translation;
        }

        /// <summary>
        ///   <para>Moves the transform by x along the x axis, y along the y axis, and z along the z axis.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="relativeTo"></param>
        public void Translate(float x, float y, float z, Transform relativeTo)
        {
            this.Translate(new Vector3(x, y, z), relativeTo);
        }

        public void Rotate(Vector3 eulerAngles)
        {
            Space relativeTo = Space.Self;
            this.Rotate(eulerAngles, relativeTo);
        }

        /// <summary>
        ///   <para>Applies a rotation of eulerAngles.z degrees around the z axis, eulerAngles.x degrees around the x axis, and eulerAngles.y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="eulerAngles">Rotation to apply.</param>
        /// <param name="relativeTo">Rotation is local to object or World.</param>
        public void Rotate(Vector3 eulerAngles, Space relativeTo)
        {
            Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
            if (relativeTo == Space.Self)
                this.localRotation = this.localRotation * quaternion;
            else
                this.rotation = this.rotation * Quaternion.Inverse(this.rotation) * quaternion * this.rotation;
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            Space relativeTo = Space.Self;
            this.Rotate(xAngle, yAngle, zAngle, relativeTo);
        }

        /// <summary>
        ///   <para>Applies a rotation of zAngle degrees around the z axis, xAngle degrees around the x axis, and yAngle degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="xAngle">Degrees to rotate around the X axis.</param>
        /// <param name="yAngle">Degrees to rotate around the Y axis.</param>
        /// <param name="zAngle">Degrees to rotate around the Z axis.</param>
        /// <param name="relativeTo">Rotation is local to object or World.</param>
        public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
        {
            this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
        }

        internal void RotateAroundInternal(Vector3 axis, float angle)
        {
            Transform.INTERNAL_CALL_RotateAroundInternal(this, ref axis, angle);
        }


        private static void INTERNAL_CALL_RotateAroundInternal(Transform self, ref Vector3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vector3 axis, float angle)
        {
            Space relativeTo = Space.Self;
            this.Rotate(axis, angle, relativeTo);
        }

        /// <summary>
        ///   <para>Rotates the object around axis by angle degrees.</para>
        /// </summary>
        /// <param name="axis">Axis to apply rotation to.</param>
        /// <param name="angle">Degrees to rotation to apply.</param>
        /// <param name="relativeTo">Rotation is local to object or World.</param>
        public void Rotate(Vector3 axis, float angle, Space relativeTo)
        {
            if (relativeTo == Space.Self)
                this.RotateAroundInternal(this.transform.TransformDirection(axis), angle * ((float) Math.PI / 180f));
            else
                this.RotateAroundInternal(axis, angle * ((float) Math.PI / 180f));
        }

        /// <summary>
        ///   <para>Rotates the transform about axis passing through point in world coordinates by angle degrees.</para>
        /// </summary>
        /// <param name="point"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        public void RotateAround(Vector3 point, Vector3 axis, float angle)
        {
            Vector3 position = this.position;
            Vector3 vector3 = Quaternion.AngleAxis(angle, axis) * (position - point);
            this.position = point + vector3;
            this.RotateAroundInternal(axis, angle * ((float) Math.PI / 180f));
        }

        /// <summary>
        ///   <para>Rotates the transform so the forward vector points at target's current position.</para>
        /// </summary>
        /// <param name="target">Object to point towards.</param>
        /// <param name="worldUp">Vector specifying the upward direction.</param>
        public void LookAt(Transform target)
        {
            Vector3 up = Vector3.up;
            this.LookAt(target, up);
        }

        /// <summary>
        ///   <para>Rotates the transform so the forward vector points at target's current position.</para>
        /// </summary>
        /// <param name="target">Object to point towards.</param>
        /// <param name="worldUp">Vector specifying the upward direction.</param>
        public void LookAt(Transform target, Vector3 worldUp)
        {
            if (target == null)
                return;
            this.LookAt(target.position, worldUp);
        }

        /// <summary>
        ///   <para>Rotates the transform so the forward vector points at worldPosition.</para>
        /// </summary>
        /// <param name="worldPosition">Point to look at.</param>
        /// <param name="worldUp">Vector specifying the upward direction.</param>
        public void LookAt(Vector3 worldPosition, Vector3 worldUp)
        {
            Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref worldUp);
        }

        /// <summary>
        ///   <para>Rotates the transform so the forward vector points at worldPosition.</para>
        /// </summary>
        /// <param name="worldPosition">Point to look at.</param>
        /// <param name="worldUp">Vector specifying the upward direction.</param>
        public void LookAt(Vector3 worldPosition)
        {
            Vector3 up = Vector3.up;
            Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref up);
        }

        private static void INTERNAL_CALL_LookAt(Transform self, ref Vector3 worldPosition, ref Vector3 worldUp)
        {
            Vector3 forward = worldPosition - self.position;
            self.rotation = Quaternion.LookRotation(forward, worldUp);
        }

        /// <summary>
        ///   <para>Transforms direction from local space to world space.</para>
        /// </summary>
        /// <param name="direction"></param>
        public Vector3 TransformDirection(Vector3 direction)
        {
            Vector3 vector3;
            Transform.INTERNAL_CALL_TransformDirection(this, ref direction, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_TransformDirection(Transform self, ref Vector3 direction, out Vector3 value)
        {
            Quaternion lhs = self.rotation;
            Vector3 rhs = direction;
            value = Quaternion.RotateVectorByQuat(self, ref lhs, ref rhs);
        }

        /// <summary>
        ///   <para>Transforms direction x, y, z from local space to world space.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 TransformDirection(float x, float y, float z)
        {
            return this.TransformDirection(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.</para>
        /// </summary>
        /// <param name="direction"></param>
        public Vector3 InverseTransformDirection(Vector3 direction)
        {
            Vector3 vector3;
            Transform.INTERNAL_CALL_InverseTransformDirection(this, ref direction, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_InverseTransformDirection(Transform self, ref Vector3 direction, out Vector3 value)
        {
            Quaternion lhs = Quaternion.Inverse(self.rotation);
            Vector3 rhs = direction;
            value = Quaternion.RotateVectorByQuat(self, ref lhs, ref rhs);
        }

        /// <summary>
        ///   <para>Transforms the direction x, y, z from world space to local space. The opposite of Transform.TransformDirection.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 InverseTransformDirection(float x, float y, float z)
        {
            return this.InverseTransformDirection(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Transforms vector from local space to world space.</para>
        /// </summary>
        /// <param name="vector"></param>
        public Vector3 TransformVector(Vector3 vector)
        {
            Vector3 vector3;
            Transform.INTERNAL_CALL_TransformVector(this, ref vector, out vector3);
            return vector3;
        }

        private static void INTERNAL_CALL_TransformVector(Transform self, ref Vector3 vector, out Vector3 value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   <para>Transforms vector x, y, z from local space to world space.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 TransformVector(float x, float y, float z)
        {
            return localToWorldMatrix.MultiplyVector(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Transforms a vector from world space to local space. The opposite of Transform.TransformVector.</para>
        /// </summary>
        /// <param name="vector"></param>
        public Vector3 InverseTransformVector(Vector3 vector)
        {
            return worldToLocalMatrix.MultiplyVector(vector);
        }

        /// <summary>
        ///   <para>Transforms the vector x, y, z from world space to local space. The opposite of Transform.TransformVector.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 InverseTransformVector(float x, float y, float z)
        {
            return this.InverseTransformVector(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Transforms position from local space to world space.</para>
        /// </summary>
        /// <param name="position"></param>
        public Vector3 TransformPoint(Vector3 position)
        {
            return localToWorldMatrix.MultiplyPoint3x4(position);
        }

        /// <summary>
        ///   <para>Transforms the position x, y, z from local space to world space.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 TransformPoint(float x, float y, float z)
        {
            return this.TransformPoint(new Vector3(x, y, z));
        }

        /// <summary>
        ///   <para>Transforms position from world space to local space.</para>
        /// </summary>
        /// <param name="position"></param>
        public Vector3 InverseTransformPoint(Vector3 position)
        {
            return worldToLocalMatrix.MultiplyPoint3x4(position);
        }

        /// <summary>
        ///   <para>Transforms the position x, y, z from world space to local space. The opposite of Transform.TransformPoint.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3 InverseTransformPoint(float x, float y, float z)
        {
            return this.InverseTransformPoint(new Vector3(x, y, z));
        }






    }
}