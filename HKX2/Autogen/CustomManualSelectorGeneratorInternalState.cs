using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class CustomManualSelectorGeneratorInternalState : hkReferencedObject
    {
        public sbyte m_currentGeneratorIndex;
        public sbyte m_generatorIndexAtActivate;
        public List<hkbStateMachineActiveTransitionInfo> m_activeTransitions;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_currentGeneratorIndex = br.ReadSByte();
            m_generatorIndexAtActivate = br.ReadSByte();
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            m_activeTransitions = des.ReadClassArray<hkbStateMachineActiveTransitionInfo>(br);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteSByte(m_currentGeneratorIndex);
            bw.WriteSByte(m_generatorIndexAtActivate);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
        }
    }
}