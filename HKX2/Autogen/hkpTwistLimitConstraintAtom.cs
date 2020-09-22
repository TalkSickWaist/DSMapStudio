using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hkpTwistLimitConstraintAtom : hkpConstraintAtom
    {
        public byte m_isEnabled;
        public byte m_twistAxis;
        public byte m_refAxis;
        public float m_minAngle;
        public float m_maxAngle;
        public float m_angularLimitsTauFactor;
    }
}
