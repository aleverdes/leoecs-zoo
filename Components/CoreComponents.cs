using UnityEngine;

namespace AffenCode
{
    public struct EcsTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }

    public struct TransformRef
    {
        public Transform Value;
    }
    
    public struct IgnoreTransformSync
    {}
    
    public struct IgnoreRigidbodySync
    {}

    public struct RigidbodyRef
    {
        public Rigidbody Value;
    }

    public struct GameObjectRef
    {
        public GameObject Value;
    }
}