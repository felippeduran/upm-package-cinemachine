﻿
using UnityEngine;

namespace Cinemachine.ECS
{
    public sealed class CM_PipelineAttribute : System.Attribute
    {
        public CinemachineCore.Stage Stage { get; private set; }
        public CM_PipelineAttribute(CinemachineCore.Stage stage) { Stage = stage; }
    }

    public sealed class CM_RangePropertyAttribute : PropertyAttribute {}
    public sealed class CM_InputAxisPropertyAttribute : PropertyAttribute {}
    public sealed class CM_OrbitPropertyAttribute : PropertyAttribute {}
}
