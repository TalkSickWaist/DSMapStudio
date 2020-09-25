using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkaiPlaneVolume : hkaiVolume
    {
        public List<Vector4> m_planes;
        public hkGeometry m_geometry;
        public bool m_isInverted;
        public hkAabb m_aabb;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_planes = des.ReadVector4Array(br);
            m_geometry = new hkGeometry();
            m_geometry.Read(des, br);
            m_isInverted = br.ReadBoolean();
            br.AssertUInt64(0);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_aabb = new hkAabb();
            m_aabb.Read(des, br);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            m_geometry.Write(bw);
            bw.WriteBoolean(m_isInverted);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            m_aabb.Write(bw);
        }
    }
}