using Cinemachine.ECS;
using Unity.Mathematics;

namespace Cinemachine.ECS_Hybrid
{
    [UnityEngine.DisallowMultipleComponent]
    [CM_Pipeline(CinemachineCore.Stage.Aim)]
    [SaveDuringPlay]
    public class CM_VcamComposerComponent : CM_VcamComponentBase<CM_VcamComposer>
    {
        private void OnValidate()
        {
            var v = Value;
            v.damping = math.max(float2.zero, v.damping);
            v.screenPosition = math.clamp(v.screenPosition, new float2(-1, -1), new float2(1, 1));
            v.deadZoneSize = math.max(float2.zero, v.deadZoneSize);
            v.softZoneSize = math.max(float2.zero, v.softZoneSize);
            v.softZoneBias = math.clamp(v.softZoneBias, new float2(-1, -1), new float2(1, 1));
            Value = v;
        }

        private void Reset()
        {
            Value = new CM_VcamComposer
            {
                softZoneSize = new float2(0.6f, 0.6f),
                centerOnActivate = 1
            };
        }
    }
}
