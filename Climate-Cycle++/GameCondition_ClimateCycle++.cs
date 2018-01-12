using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace ClimateCyclePlusPlus
{
    public class GameCondition_ClimateCyclePlusPlus : GameCondition
    {
        private int ticksOffset;
        public static CycleSettings settings;

        public override void Init()
        {
            this.ticksOffset = ((Rand.Value >= 0.5f) ? 7200000 : 0);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.ticksOffset, "ticksOffset", 0, false);
        }

        public override float TemperatureOffset()
        {
            if (settings.cycleType == "Winter is coming")
                return Mathf.Sin(GenDate.YearsPassedFloat / settings.cyclePeriods * 3.14159274f * 2f) * 20f - (GenDate.YearsPassedFloat * settings.cycleMultiplier);

            if (settings.cycleType == "Waiting for the Sun")
                return Mathf.Sin(GenDate.YearsPassedFloat / settings.cyclePeriods * 3.14159274f * 2f) * 20f + (GenDate.YearsPassedFloat * settings.cycleMultiplier);
            
            else
                return Mathf.Sin(GenDate.YearsPassedFloat / settings.cyclePeriods * 3.14159274f * 2f) * (20f + (GenDate.YearsPassedFloat * settings.cycleMultiplier));
        }
    }
}
