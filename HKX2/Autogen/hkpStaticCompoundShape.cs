using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkpStaticCompoundShape : hkpBvTreeShape
    {
        public enum Config
        {
            NUM_BYTES_FOR_TREE = 48,
        }
        
        public sbyte m_numBitsForChildShapeKey;
        public List<hkpStaticCompoundShapeInstance> m_instances;
        public List<ushort> m_instanceExtraInfos;
        public hkpShapeKeyTable m_disabledLargeShapeKeyTable;
        public hkcdStaticTreeDefaultTreeStorage6 m_tree;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            br.AssertUInt64(0);
            m_numBitsForChildShapeKey = br.ReadSByte();
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_instances = des.ReadClassArray<hkpStaticCompoundShapeInstance>(br);
            m_instanceExtraInfos = des.ReadUInt16Array(br);
            m_disabledLargeShapeKeyTable = new hkpShapeKeyTable();
            m_disabledLargeShapeKeyTable.Read(des, br);
            br.AssertUInt64(0);
            m_tree = new hkcdStaticTreeDefaultTreeStorage6();
            m_tree.Read(des, br);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteUInt64(0);
            bw.WriteSByte(m_numBitsForChildShapeKey);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            m_disabledLargeShapeKeyTable.Write(bw);
            bw.WriteUInt64(0);
            m_tree.Write(bw);
        }
    }
}