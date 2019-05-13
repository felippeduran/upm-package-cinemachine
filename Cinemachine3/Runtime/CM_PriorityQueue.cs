﻿using Unity.Collections;
using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Unity.Cinemachine3
{
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public unsafe struct CM_PriorityQueue
    {
        public struct QueueEntry
        {
            public Entity entity;
            public CM_VcamPriority vcamPriority;
            public CM_VcamShotQuality shotQuality;
        }

        [FieldOffset(0)] QueueEntry* data;
        [FieldOffset(8)] int mLength;

        public int Length { get { return mLength; } }

        // Call from a job
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity EntityAt(int index)
        {
            return index >= 0 && index < Length ? data[index].entity : Entity.Null;
        }

        // Call from a job
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void* GetUnsafeDataPtr() { return data; }

        // Call from a job
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetData([ReadOnly] NativeArray<QueueEntry> array)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (array.Length > Length)
                throw new System.IndexOutOfRangeException("CM_PriorityQueue.SetData out of range");
#endif
            if (Length > 0)
                UnsafeUtility.MemCpy(data, array.GetUnsafeReadOnlyPtr(), sizeof(QueueEntry) * Length);
        }

        // Call outside of job
        public void AllocateData(int length)
        {
            if (length != Length)
            {
                if (data != null)
                    UnsafeUtility.Free(data, Allocator.Persistent);
                mLength = length;
                data = null;
                if (length > 0)
                {
                    int size = sizeof(QueueEntry) * length;
                    data = (QueueEntry*)UnsafeUtility.Malloc(size,
                        UnsafeUtility.AlignOf<QueueEntry>(), Allocator.Persistent);
                    UnsafeUtility.MemClear(data, size);
                }
            }
        }

        // Call outside of job
        public void Dispose()
        {
            if (data != null)
                UnsafeUtility.Free(data, Allocator.Persistent);
            data = null;
            mLength = 0;
        }
    }
}

