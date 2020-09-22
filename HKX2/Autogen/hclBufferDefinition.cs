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
    }
}
