using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public enum MaxConstraintSetSize
    {
        MAX_CONSTRAINT_SET_SIZE = 128,
    }
    
    public class hclConstraintSet : hkReferencedObject
    {
        public string m_name;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_name = des.ReadStringPointer(br);
            br.AssertUInt64(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteUInt64(0);
        }
    }
}