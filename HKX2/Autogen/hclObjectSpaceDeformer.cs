using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hclObjectSpaceDeformer : IHavokObject
    {
        public enum ControlByte
        {
            FOUR_BLEND = 0,
            THREE_BLEND = 1,
            TWO_BLEND = 2,
            ONE_BLEND = 3,
            NEXT_SPU_BATCH = 4,
        }
        
        public List<hclObjectSpaceDeformerFourBlendEntryBlock> m_fourBlendEntries;
        public List<hclObjectSpaceDeformerThreeBlendEntryBlock> m_threeBlendEntries;
        public List<hclObjectSpaceDeformerTwoBlendEntryBlock> m_twoBlendEntries;
        public List<hclObjectSpaceDeformerOneBlendEntryBlock> m_oneBlendEntries;
        public List<byte> m_controlBytes;
        public ushort m_startVertexIndex;
        public ushort m_endVertexIndex;
        public ushort m_batchSizeSpu;
        public bool m_partialWrite;
        
        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_fourBlendEntries = des.ReadClassArray<hclObjectSpaceDeformerFourBlendEntryBlock>(br);
            m_threeBlendEntries = des.ReadClassArray<hclObjectSpaceDeformerThreeBlendEntryBlock>(br);
            m_twoBlendEntries = des.ReadClassArray<hclObjectSpaceDeformerTwoBlendEntryBlock>(br);
            m_oneBlendEntries = des.ReadClassArray<hclObjectSpaceDeformerOneBlendEntryBlock>(br);
            m_controlBytes = des.ReadByteArray(br);
            m_startVertexIndex = br.ReadUInt16();
            m_endVertexIndex = br.ReadUInt16();
            m_batchSizeSpu = br.ReadUInt16();
            m_partialWrite = br.ReadBoolean();
            br.AssertByte(0);
        }
        
        public virtual void Write(BinaryWriterEx bw)
        {
            bw.WriteUInt16(m_startVertexIndex);
            bw.WriteUInt16(m_endVertexIndex);
            bw.WriteUInt16(m_batchSizeSpu);
            bw.WriteBoolean(m_partialWrite);
            bw.WriteByte(0);
        }
    }
}