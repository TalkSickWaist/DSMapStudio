using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hclBufferDefinition : hkReferencedObject
    {
        public string m_name;
        public int m_type;
        public int m_subType;
        public uint m_numVertices;
        public uint m_numTriangles;
        public hclBufferLayout m_bufferLayout;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_name = des.ReadStringPointer(br);
            m_type = br.ReadInt32();
            m_subType = br.ReadInt32();
            m_numVertices = br.ReadUInt32();
            m_numTriangles = br.ReadUInt32();
            m_bufferLayout = new hclBufferLayout();
            m_bufferLayout.Read(des, br);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteInt32(m_type);
            bw.WriteInt32(m_subType);
            bw.WriteUInt32(m_numVertices);
            bw.WriteUInt32(m_numTriangles);
            m_bufferLayout.Write(bw);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
        }
    }
}