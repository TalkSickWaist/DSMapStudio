using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkaiLineOfSightUtilLineOfSightOutput : IHavokObject
    {
        public List<uint> m_visitedEdgesOut;
        public List<float> m_distancesOut;
        public List<Vector4> m_pointsOut;
        public bool m_doNotExceedArrayCapacity;
        public int m_numIterationsOut;
        public uint m_finalFaceKey;
        public float m_accumulatedDistance;
        public Vector4 m_finalPoint;
        
        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_visitedEdgesOut = des.ReadUInt32Array(br);
            m_distancesOut = des.ReadSingleArray(br);
            m_pointsOut = des.ReadVector4Array(br);
            m_doNotExceedArrayCapacity = br.ReadBoolean();
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_numIterationsOut = br.ReadInt32();
            m_finalFaceKey = br.ReadUInt32();
            m_accumulatedDistance = br.ReadSingle();
            m_finalPoint = des.ReadVector4(br);
        }
        
        public virtual void Write(BinaryWriterEx bw)
        {
            bw.WriteBoolean(m_doNotExceedArrayCapacity);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteInt32(m_numIterationsOut);
            bw.WriteUInt32(m_finalFaceKey);
            bw.WriteSingle(m_accumulatedDistance);
        }
    }
}