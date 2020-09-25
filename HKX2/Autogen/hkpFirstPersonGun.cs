using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkpFirstPersonGun : hkReferencedObject
    {
        public enum Type
        {
            WEAPON_TYPE_INVALID = 0,
            WEAPON_TYPE_BALLGUN = 1,
            WEAPON_TYPE_GRENADEGUN = 2,
            WEAPON_TYPE_GRAVITYGUN = 3,
            WEAPON_TYPE_MOUNTEDBALLGUN = 4,
            WEAPON_TYPE_TWEAKERGUN = 5,
            WEAPON_TYPE_MISSILEGUN = 6,
            WEAPON_TYPE_RAYCASTGUN = 7,
            WEAPON_TYPE_SPHEREGUN = 8,
            WEAPON_TYPE_STICKYGUN = 9,
            WEAPON_TYPE_NUM_TYPES = 10,
        }
        
        public enum KeyboardKey
        {
            KEY_F1 = 112,
            KEY_F2 = 113,
            KEY_F3 = 114,
            KEY_F4 = 115,
            KEY_F5 = 116,
            KEY_F6 = 117,
            KEY_F7 = 118,
            KEY_F8 = 119,
            KEY_F9 = 120,
            KEY_F10 = 121,
            KEY_F11 = 122,
            KEY_F12 = 123,
        }
        
        public string m_name;
        public KeyboardKey m_keyboardKey;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            br.AssertUInt64(0);
            m_name = des.ReadStringPointer(br);
            m_keyboardKey = (KeyboardKey)br.ReadByte();
            br.AssertUInt64(0);
            br.AssertUInt64(0);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
        }
    }
}