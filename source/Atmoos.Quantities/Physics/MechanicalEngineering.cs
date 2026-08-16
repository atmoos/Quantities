using static Atmoos.Quantities.Extensions;

namespace Atmoos.Quantities.Physics;

public static class Geometry
{
    extension(Length)
    {
        public static Area operator *(in Length left, in Length right) => Create<Area>(left.Value * right.Value);

        public static Volume operator *(in Length length, in Area area) => Create<Volume>(length.Value * area.Value);
    }

    extension(Area)
    {
        public static Volume operator *(in Area area, in Length length) => Create<Volume>(area.Value * length.Value);

        public static Length operator /(in Area left, in Length right) => Create<Length>(left.Value / right.Value);
    }

    extension(Volume)
    {
        public static Length operator /(in Volume volume, in Area area) => Create<Length>(volume.Value / area.Value);

        public static Area operator /(in Volume volume, in Length length) => Create<Area>(volume.Value / length.Value);
    }
}

public static class Kinematics
{
    extension(Length)
    {
        public static Time operator /(in Length length, in Velocity velocity) => Create<Time>(length.Value / velocity.Value);

        public static Velocity operator /(in Length length, in Time time) => Create<Velocity>(length.Value / time.Value);
    }

    extension(Velocity)
    {
        public static Length operator *(in Velocity velocity, in Time time) => Create<Length>(velocity.Value * time.Value);

        public static Time operator /(in Velocity velocity, in Acceleration acceleration) => Create<Time>(velocity.Value / acceleration.Value);

        public static Acceleration operator /(in Velocity velocity, in Time time) => Create<Acceleration>(velocity.Value / time.Value);

        public static Power operator *(in Velocity velocity, in Force force) => Create<Power>(force.Value * velocity.Value);

        public static Momentum operator *(in Velocity velocity, in Mass mass) => Create<Momentum>(velocity.Value * mass.Value);
    }

    extension(Acceleration)
    {
        public static Velocity operator *(in Acceleration acceleration, in Time time) => Create<Velocity>(acceleration.Value * time.Value);
    }

    extension(Area)
    {
        public static Force operator *(in Area area, in Pressure pressure) => Create<Force>(pressure.Value * area.Value);
    }

    extension(Time)
    {
        public static Energy operator *(in Time time, in Power power) => Create<Energy>(power.Value * time.Value);

        public static Length operator *(in Time time, in Velocity velocity) => Create<Length>(velocity.Value * time.Value);

        public static Velocity operator *(in Time time, in Acceleration acceleration) => Create<Velocity>(acceleration.Value * time.Value);

        public static Impulse operator *(in Time time, in Force force) => Create<Impulse>(time.Value * force.Value);
    }

    extension(Angle)
    {
        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static Time operator /(in Angle angle, in AngularVelocity angularVelocity) => Create<Time>(angle.Value / angularVelocity.Value);

        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static AngularVelocity operator /(in Angle angle, in Time time) => Create<AngularVelocity>(angle.Value / time.Value);
    }

    extension(AngularVelocity)
    {
        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static Angle operator *(in AngularVelocity angularVelocity, in Time time) => Create<Angle>(angularVelocity.Value * time.Value);

        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static Time operator /(in AngularVelocity angularVelocity, in AngularAcceleration angularAcceleration) => Create<Time>(angularVelocity.Value / angularAcceleration.Value);

        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static AngularAcceleration operator /(in AngularVelocity angularVelocity, in Time time) => Create<AngularAcceleration>(angularVelocity.Value / time.Value);
    }

    extension(AngularAcceleration)
    {
        [Ai(Model = "GPT", Version = "5.3", Variant = "Codex")]
        public static AngularVelocity operator *(in AngularAcceleration angularAcceleration, in Time time) => Create<AngularVelocity>(angularAcceleration.Value * time.Value);
    }

    extension(Pressure)
    {
        public static Force operator *(in Pressure pressure, in Area area) => Create<Force>(pressure.Value * area.Value);
    }

    extension(Force)
    {
        public static Pressure operator /(in Force force, in Area area) => Create<Pressure>(force.Value / area.Value);

        public static Power operator *(in Force force, in Velocity velocity) => Create<Power>(force.Value * velocity.Value);

        public static Torque operator *(in Force force, in Length length) => Create<Torque>(force.Value * length.Value);

        public static Impulse operator *(in Force force, in Time time) => Create<Impulse>(force.Value * time.Value);
    }

    extension(Length)
    {
        public static Torque operator *(in Length length, in Force force) => Create<Torque>(length.Value * force.Value);
    }

    extension(Torque)
    {
        public static Force operator /(in Torque torque, in Length length) => Create<Force>(torque.Value / length.Value);

        public static Length operator /(in Torque torque, in Force force) => Create<Length>(torque.Value / force.Value);
    }

    extension(Power)
    {
        public static Velocity operator /(in Power power, in Force force) => Create<Velocity>(power.Value / force.Value);

        public static Force operator /(in Power power, in Velocity velocity) => Create<Force>(power.Value / velocity.Value);

        public static Energy operator *(in Power power, in Time time) => Create<Energy>(power.Value * time.Value);
    }

    extension(Energy)
    {
        public static Power operator /(in Energy energy, in Time time) => Create<Power>(energy.Value / time.Value);

        public static Time operator /(in Energy energy, in Power power) => Create<Time>(energy.Value / power.Value);

        public static SpecificEnergy operator /(in Energy energy, in Mass mass) => Create<SpecificEnergy>(energy.Value / mass.Value);

        public static Mass operator /(in Energy energy, in SpecificEnergy specificEnergy) => Create<Mass>(energy.Value / specificEnergy.Value);
    }

    extension(Mass)
    {
        public static Momentum operator *(in Mass mass, in Velocity velocity) => Create<Momentum>(mass.Value * velocity.Value);

        public static Energy operator *(in Mass mass, in SpecificEnergy specificEnergy) => Create<Energy>(mass.Value * specificEnergy.Value);
    }

    extension(Momentum)
    {
        public static Velocity operator /(in Momentum momentum, in Mass mass) => Create<Velocity>(momentum.Value / mass.Value);

        public static Mass operator /(in Momentum momentum, in Velocity velocity) => Create<Mass>(momentum.Value / velocity.Value);
    }

    extension(Impulse)
    {
        public static Time operator /(in Impulse impulse, in Force force) => Create<Time>(impulse.Value / force.Value);

        public static Force operator /(in Impulse impulse, in Time time) => Create<Force>(impulse.Value / time.Value);
    }

    extension(SpecificEnergy)
    {
        public static Energy operator *(in SpecificEnergy specificEnergy, in Mass mass) => Create<Energy>(specificEnergy.Value * mass.Value);
    }
}

[Ai(Model = "Claude", Version = "4.6", Variant = "Opus")]
public static class FluidDynamics
{
    extension(Mass)
    {
        public static Density operator /(in Mass mass, in Volume volume) => Create<Density>(mass.Value / volume.Value);

        public static MassFlowRate operator /(in Mass mass, in Time time) => Create<MassFlowRate>(mass.Value / time.Value);

        public static Volume operator /(in Mass mass, in Density density) => Create<Volume>(mass.Value / density.Value);

        public static Time operator /(in Mass mass, in MassFlowRate massFlowRate) => Create<Time>(mass.Value / massFlowRate.Value);
    }

    extension(Volume)
    {
        public static VolumetricFlowRate operator /(in Volume volume, in Time time) => Create<VolumetricFlowRate>(volume.Value / time.Value);

        public static Time operator /(in Volume volume, in VolumetricFlowRate volumetricFlowRate) => Create<Time>(volume.Value / volumetricFlowRate.Value);

        public static Mass operator *(in Volume volume, in Density density) => Create<Mass>(volume.Value * density.Value);
    }

    extension(Density)
    {
        public static Mass operator *(in Density density, in Volume volume) => Create<Mass>(density.Value * volume.Value);

        public static MassFlowRate operator *(in Density density, in VolumetricFlowRate volumetricFlowRate) => Create<MassFlowRate>(density.Value * volumetricFlowRate.Value);
    }

    extension(VolumetricFlowRate)
    {
        public static Volume operator *(in VolumetricFlowRate volumetricFlowRate, in Time time) => Create<Volume>(volumetricFlowRate.Value * time.Value);

        public static MassFlowRate operator *(in VolumetricFlowRate volumetricFlowRate, in Density density) => Create<MassFlowRate>(volumetricFlowRate.Value * density.Value);
    }

    extension(MassFlowRate)
    {
        public static Mass operator *(in MassFlowRate massFlowRate, in Time time) => Create<Mass>(massFlowRate.Value * time.Value);

        public static VolumetricFlowRate operator /(in MassFlowRate massFlowRate, in Density density) => Create<VolumetricFlowRate>(massFlowRate.Value / density.Value);

        public static Density operator /(in MassFlowRate massFlowRate, in VolumetricFlowRate volumetricFlowRate) => Create<Density>(massFlowRate.Value / volumetricFlowRate.Value);
    }

    extension(Time)
    {
        public static Volume operator *(in Time time, in VolumetricFlowRate volumetricFlowRate) => Create<Volume>(time.Value * volumetricFlowRate.Value);

        public static Mass operator *(in Time time, in MassFlowRate massFlowRate) => Create<Mass>(time.Value * massFlowRate.Value);

        public static DynamicViscosity operator *(in Time time, in Pressure pressure) => Create<DynamicViscosity>(time.Value * pressure.Value);
    }

    extension(Pressure)
    {
        public static DynamicViscosity operator *(in Pressure pressure, in Time time) => Create<DynamicViscosity>(pressure.Value * time.Value);
    }

    extension(DynamicViscosity)
    {
        public static Pressure operator /(in DynamicViscosity dynamicViscosity, in Time time) => Create<Pressure>(dynamicViscosity.Value / time.Value);

        public static Time operator /(in DynamicViscosity dynamicViscosity, in Pressure pressure) => Create<Time>(dynamicViscosity.Value / pressure.Value);
    }
}

[Ai(Model = "Claude", Version = "4.5", Variant = "Haiku")]
public static class PhotometryLaws
{
    extension(LuminousFlux)
    {
        public static Illuminance operator /(in LuminousFlux luminousFlux, in Area area) => Create<Illuminance>(luminousFlux.Value / area.Value);
    }

    extension(Illuminance)
    {
        public static LuminousFlux operator *(in Illuminance illuminance, in Area area) => Create<LuminousFlux>(illuminance.Value * area.Value);

        public static Area operator /(in LuminousFlux luminousFlux, in Illuminance illuminance) => Create<Area>(luminousFlux.Value / illuminance.Value);
    }
}
