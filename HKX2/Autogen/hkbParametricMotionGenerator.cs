using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public enum MotionSpaceType
    {
        MST_UNKNOWN = 0,
        MST_ANGULAR = 1,
        MST_DIRECTIONAL = 2,
    }
    
    public class hkbParametricMotionGenerator : hkbProceduralBlenderGenerator
    {
        public MotionSpaceType m_motionSpace;
        public List<hkbGenerator> m_generators;
        public float m_xAxisParameterValue;
        public float m_yAxisParameterValue;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_motionSpace = (MotionSpaceType)br.ReadSByte();
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_generators = des.ReadClassPointerArray<hkbGenerator>(br);
            m_xAxisParameterValue = br.ReadSingle();
            m_yAxisParameterValue = br.ReadSingle();
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt64(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteSingle(m_xAxisParameterValue);
            bw.WriteSingle(m_yAxisParameterValue);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
        }
    }
}