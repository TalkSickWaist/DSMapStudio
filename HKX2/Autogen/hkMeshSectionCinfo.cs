using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkMeshSectionCinfo : IHavokObject
    {
        public hkMeshVertexBuffer m_vertexBuffer;
        public hkMeshMaterial m_material;
        public hkMeshBoneIndexMapping m_boneMatrixMap;
        public PrimitiveType m_primitiveType;
        public int m_numPrimitives;
        public MeshSectionIndexType m_indexType;
        public int m_vertexStartIndex;
        public int m_transformIndex;
        
        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_vertexBuffer = des.ReadClassPointer<hkMeshVertexBuffer>(br);
            m_material = des.ReadClassPointer<hkMeshMaterial>(br);
            m_boneMatrixMap = new hkMeshBoneIndexMapping();
            m_boneMatrixMap.Read(des, br);
            m_primitiveType = (PrimitiveType)br.ReadByte();
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_numPrimitives = br.ReadInt32();
            m_indexType = (MeshSectionIndexType)br.ReadByte();
            br.AssertUInt64(0);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_vertexStartIndex = br.ReadInt32();
            m_transformIndex = br.ReadInt32();
        }
        
        public virtual void Write(BinaryWriterEx bw)
        {
            // Implement Write
            // Implement Write
            m_boneMatrixMap.Write(bw);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteInt32(m_numPrimitives);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteInt32(m_vertexStartIndex);
            bw.WriteInt32(m_transformIndex);
        }
    }
}