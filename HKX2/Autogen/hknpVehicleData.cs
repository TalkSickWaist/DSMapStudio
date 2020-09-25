using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hknpVehicleData : hkReferencedObject
    {
        public Vector4 m_gravity;
        public sbyte m_numWheels;
        public Matrix4x4 m_chassisOrientation;
        public float m_torqueRollFactor;
        public float m_torquePitchFactor;
        public float m_torqueYawFactor;
        public float m_extraTorqueFactor;
        public float m_maxVelocityForPositionalFriction;
        public float m_chassisUnitInertiaYaw;
        public float m_chassisUnitInertiaRoll;
        public float m_chassisUnitInertiaPitch;
        public float m_frictionEqualizer;
        public float m_normalClippingAngleCos;
        public float m_maxFrictionSolverMassRatio;
        public List<hknpVehicleDataWheelComponentParams> m_wheelParams;
        public List<sbyte> m_numWheelsPerAxle;
        public hkpVehicleFrictionDescription m_frictionDescription;
        public Vector4 m_chassisFrictionInertiaInvDiag;
        public bool m_alreadyInitialised;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            m_gravity = des.ReadVector4(br);
            m_numWheels = br.ReadSByte();
            br.AssertUInt64(0);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_chassisOrientation = des.ReadMatrix3(br);
            m_torqueRollFactor = br.ReadSingle();
            m_torquePitchFactor = br.ReadSingle();
            m_torqueYawFactor = br.ReadSingle();
            m_extraTorqueFactor = br.ReadSingle();
            m_maxVelocityForPositionalFriction = br.ReadSingle();
            m_chassisUnitInertiaYaw = br.ReadSingle();
            m_chassisUnitInertiaRoll = br.ReadSingle();
            m_chassisUnitInertiaPitch = br.ReadSingle();
            m_frictionEqualizer = br.ReadSingle();
            m_normalClippingAngleCos = br.ReadSingle();
            m_maxFrictionSolverMassRatio = br.ReadSingle();
            br.AssertUInt32(0);
            m_wheelParams = des.ReadClassArray<hknpVehicleDataWheelComponentParams>(br);
            m_numWheelsPerAxle = des.ReadSByteArray(br);
            m_frictionDescription = new hkpVehicleFrictionDescription();
            m_frictionDescription.Read(des, br);
            m_chassisFrictionInertiaInvDiag = des.ReadVector4(br);
            m_alreadyInitialised = br.ReadBoolean();
            br.AssertUInt64(0);
            br.AssertUInt32(0);
            br.AssertUInt16(0);
            br.AssertByte(0);
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteSByte(m_numWheels);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteSingle(m_torqueRollFactor);
            bw.WriteSingle(m_torquePitchFactor);
            bw.WriteSingle(m_torqueYawFactor);
            bw.WriteSingle(m_extraTorqueFactor);
            bw.WriteSingle(m_maxVelocityForPositionalFriction);
            bw.WriteSingle(m_chassisUnitInertiaYaw);
            bw.WriteSingle(m_chassisUnitInertiaRoll);
            bw.WriteSingle(m_chassisUnitInertiaPitch);
            bw.WriteSingle(m_frictionEqualizer);
            bw.WriteSingle(m_normalClippingAngleCos);
            bw.WriteSingle(m_maxFrictionSolverMassRatio);
            bw.WriteUInt32(0);
            m_frictionDescription.Write(bw);
            bw.WriteBoolean(m_alreadyInitialised);
            bw.WriteUInt64(0);
            bw.WriteUInt32(0);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
        }
    }
}