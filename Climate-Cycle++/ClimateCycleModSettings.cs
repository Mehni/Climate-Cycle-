using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus
{
    public class CycleSettings : ModSettings
    {
        private const string defaultCycleType = "Regular Cycle";
        public string cycleType = defaultCycleType;

        public float cyclePeriods = 4f;
        public float cycleMultiplier = 5f;

        //public bool fuckMyShitUp = false;
        //public bool reallyFuckItUp = false;
        //not today.

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.cyclePeriods, "cyclePeriods", 4f);
            Scribe_Values.Look(ref this.cycleMultiplier, "cycleMultiplier", 5f);
            Scribe_Values.Look(ref this.cycleType, "cycleType", "Regular Cycle");
        }
    }

    class ClimateCycle : Mod
    {
        CycleSettings settings;

        public static string[] cycleTypes = { "Regular Cycle", "Winter is coming", "Waiting for the Sun", "Normal Summer, Cold Winter"};

        public ClimateCycle(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<CycleSettings>();
            GameCondition_ClimateCyclePlusPlus.settings = this.settings;
        }

        public override string SettingsCategory() => "Climate Cycle++";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            //listing_Standard.AddLabeledCheckbox("CCPP_ColdNightHotDays".Translate(), ref settings.showWeather);
            //listing_Standard.AddHorizontalLine(3f);
            listing_Standard.AddLabeledRadioList($"{"CCPP_CycleTypeChoice".Translate()}:", cycleTypes, ref settings.cycleType);
            listing_Standard.GapLine(3f);
            listing_Standard.AddLabeledNumericalTextField<float>("CCPP_Multiplier".Translate(), ref settings.cycleMultiplier, minValue: 1f, maxValue: 20f);
            listing_Standard.AddLabeledNumericalTextField<float>("CCPP_Periods".Translate(), ref settings.cyclePeriods, minValue: 1f, maxValue: 8f);
            listing_Standard.End();
            settings.Write();
        }
    }
}
