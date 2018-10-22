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
            this.ticksOffset = (Rand.Value >= 0.5f) ? (int)settings.cyclePeriods / 2 : 0;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.ticksOffset, "ticksOffset", 0, false);
        }

        //one day this'll be convoluted enough for me to need comments.
        public override float TemperatureOffset()
        {
            if (settings.cycleType == "Winter is coming")
                return Mathf.Sin((GenDate.YearsPassedFloat + this.ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) * 20f - (GenDate.YearsPassedFloat * settings.cycleMultiplier) - 20f;

            if (settings.cycleType == "Waiting for the Sun")
                return Mathf.Sin((GenDate.YearsPassedFloat + this.ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) * 20f + (GenDate.YearsPassedFloat * settings.cycleMultiplier);

            if (settings.cycleType == "Normal Summer, Cold Winter")
            {
                if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(this.SingleMap.Tile)) == Season.Fall)
                    return Mathf.Sin((GenDate.YearsPassedFloat + this.ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) * 20f - (GenDate.YearsPassedFloat * settings.cycleMultiplier / 2) - 20f;

                if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(this.SingleMap.Tile)) == Season.Winter)
                    return Mathf.Sin((GenDate.YearsPassedFloat + this.ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) * 20f - (GenDate.YearsPassedFloat * settings.cycleMultiplier) - (GenDate.YearsPassedFloat * settings.cycleMultiplier) - 20f;

                return 0f;
            }

            return Mathf.Sin((GenDate.YearsPassedFloat + this.ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) * (20f + (GenDate.YearsPassedFloat * settings.cycleMultiplier));
        }
    }
}
