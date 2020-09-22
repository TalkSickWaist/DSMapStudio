using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public enum MeshSectionIndexType
    {
        INDEX_TYPE_NONE = 0,
        INDEX_TYPE_UINT16 = 1,
        INDEX_TYPE_UINT32 = 2,
    }
    
    public enum PrimitiveType
    {
        PRIMITIVE_TYPE_UNKNOWN = 0,
        PRIMITIVE_TYPE_POINT_LIST = 1,
        PRIMITIVE_TYPE_LINE_LIST = 2,
        PRIMITIVE_TYPE_TRIANGLE_LIST = 3,
        PRIMITIVE_TYPE_TRIANGLE_STRIP = 4,
    }
    
    public class hkMeshSection
    {
        public PrimitiveType m_primitiveType;
        public int m_numPrimitives;
        public int m_numIndices;
        public int m_vertexStartIndex;
        public int m_transformIndex;
        public MeshSectionIndexType m_indexType;
        public hkMeshVertexBuffer m_vertexBuffer;
        public hkMeshMaterial m_material;
        public hkMeshBoneIndexMapping m_boneMatrixMap;
        public int m_sectionIndex;
    }
}
