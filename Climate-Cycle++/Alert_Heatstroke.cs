using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace ClimateCyclePlusPlus
{
    //"Adapted" from RimWorld.Alert_Hypothermia
    public class Alert_Heatstroke : Alert_Critical
    {
        private IEnumerable<Pawn> HeatstrokeDangerColonists
        {
            get
            {
                foreach (Pawn p in PawnsFinder.AllMaps_FreeColonistsSpawned)
                {
                    if (!p.SafeTemperatureRange().Includes(p.AmbientTemperature))
                    {
                        Hediff heatstroke = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Heatstroke, false);
                        if (heatstroke != null && heatstroke.CurStageIndex >= 3)
                        {
                            yield return p;
                        }
                    }
                }
            }
        }

        public Alert_Heatstroke()
        {
            this.defaultLabel = "AlertHeatstroke".Translate();
        }

        public override string GetExplanation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Pawn current in this.HeatstrokeDangerColonists)
            {
                stringBuilder.AppendLine("    " + current.LabelShort);
            }
            return "AlertHeatstrokeDesc".Translate(new object[]
            {
                stringBuilder.ToString()
            });
        }

        public override AlertReport GetReport()
        {
            return AlertReport.CulpritsAre(this.HeatstrokeDangerColonists);
        }
    }
}
