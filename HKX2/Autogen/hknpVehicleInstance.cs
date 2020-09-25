using SoulsFormats;
using System.Collections.Generic;
using System.Numerics;

namespace HKX2
{
    public class hknpVehicleInstance : hknpUnaryAction
    {
        public hknpVehicleData m_data;
        public hknpVehicleDriverInput m_driverInput;
        public hknpVehicleSteering m_steering;
        public hknpVehicleEngine m_engine;
        public hknpVehicleTransmission m_transmission;
        public hknpVehicleBrake m_brake;
        public hknpVehicleSuspension m_suspension;
        public hknpVehicleAerodynamics m_aerodynamics;
        public hknpVehicleWheelCollide m_wheelCollide;
        public hknpTyremarksInfo m_tyreMarks;
        public hknpVehicleVelocityDamper m_velocityDamper;
        public List<hknpVehicleInstanceWheelInfo> m_wheelsInfo;
        public hkpVehicleFrictionStatus m_frictionStatus;
        public hknpVehicleDriverInputStatus m_deviceStatus;
        public List<bool> m_isFixed;
        public float m_wheelsTimeSinceMaxPedalInput;
        public bool m_tryingToReverse;
        public float m_torque;
        public float m_rpm;
        public float m_mainSteeringAngle;
        public float m_mainSteeringAngleAssumingNoReduction;
        public List<float> m_wheelsSteeringAngle;
        public bool m_isReversing;
        public sbyte m_currentGear;
        public bool m_delayed;
        public float m_clutchDelayCountdown;
        
        public override void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            base.Read(des, br);
            br.AssertUInt64(0);
            m_data = des.ReadClassPointer<hknpVehicleData>(br);
            m_driverInput = des.ReadClassPointer<hknpVehicleDriverInput>(br);
            m_steering = des.ReadClassPointer<hknpVehicleSteering>(br);
            m_engine = des.ReadClassPointer<hknpVehicleEngine>(br);
            m_transmission = des.ReadClassPointer<hknpVehicleTransmission>(br);
            m_brake = des.ReadClassPointer<hknpVehicleBrake>(br);
            m_suspension = des.ReadClassPointer<hknpVehicleSuspension>(br);
            m_aerodynamics = des.ReadClassPointer<hknpVehicleAerodynamics>(br);
            m_wheelCollide = des.ReadClassPointer<hknpVehicleWheelCollide>(br);
            m_tyreMarks = des.ReadClassPointer<hknpTyremarksInfo>(br);
            m_velocityDamper = des.ReadClassPointer<hknpVehicleVelocityDamper>(br);
            m_wheelsInfo = des.ReadClassArray<hknpVehicleInstanceWheelInfo>(br);
            m_frictionStatus = new hkpVehicleFrictionStatus();
            m_frictionStatus.Read(des, br);
            m_deviceStatus = des.ReadClassPointer<hknpVehicleDriverInputStatus>(br);
            m_isFixed = des.ReadBooleanArray(br);
            m_wheelsTimeSinceMaxPedalInput = br.ReadSingle();
            m_tryingToReverse = br.ReadBoolean();
            br.AssertUInt16(0);
            br.AssertByte(0);
            m_torque = br.ReadSingle();
            m_rpm = br.ReadSingle();
            m_mainSteeringAngle = br.ReadSingle();
            m_mainSteeringAngleAssumingNoReduction = br.ReadSingle();
            m_wheelsSteeringAngle = des.ReadSingleArray(br);
            m_isReversing = br.ReadBoolean();
            m_currentGear = br.ReadSByte();
            m_delayed = br.ReadBoolean();
            br.AssertByte(0);
            m_clutchDelayCountdown = br.ReadSingle();
        }
        
        public override void Write(BinaryWriterEx bw)
        {
            base.Write(bw);
            bw.WriteUInt64(0);
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            // Implement Write
            m_frictionStatus.Write(bw);
            // Implement Write
            bw.WriteSingle(m_wheelsTimeSinceMaxPedalInput);
            bw.WriteBoolean(m_tryingToReverse);
            bw.WriteUInt16(0);
            bw.WriteByte(0);
            bw.WriteSingle(m_torque);
            bw.WriteSingle(m_rpm);
            bw.WriteSingle(m_mainSteeringAngle);
            bw.WriteSingle(m_mainSteeringAngleAssumingNoReduction);
            bw.WriteBoolean(m_isReversing);
            bw.WriteSByte(m_currentGear);
            bw.WriteBoolean(m_delayed);
            bw.WriteByte(0);
            bw.WriteSingle(m_clutchDelayCountdown);
        }
    }
}