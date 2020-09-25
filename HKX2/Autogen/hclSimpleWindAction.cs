using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hclSimpleWindAction : hclAction
    {
        public Vector4 m_windDirection;
        public float m_windMinSpeed;
        public float m_windMaxSpeed;
        public float m_windFrequency;
        public float m_maximumDrag;
        public Vector4 m_airVelocity;
        public float m_currentTime;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_windDirection = des.ReadVector4(br);
            m_windMinSpeed = br.ReadSingle();
            m_windMaxSpeed = br.ReadSingle();
            m_windFrequency = br.ReadSingle();
            m_maximumDrag = br.ReadSingle();
            m_airVelocity = des.ReadVector4(br);
            m_currentTime = br.ReadSingle();
            br.AssertUInt64(0);
            br.AssertUInt32(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteSingle(m_windMinSpeed);
            bw.WriteSingle(m_windMaxSpeed);
            bw.WriteSingle(m_windFrequency);
            bw.WriteSingle(m_maximumDrag);
            bw.WriteSingle(m_currentTime);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
        }
    }
}