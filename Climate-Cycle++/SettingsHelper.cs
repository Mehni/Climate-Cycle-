using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus
{

    //thanks to AlexTD and Why_is_that for the below
    internal static class SettingsHelper
    {

        public static Rect GetRect(this Listing_Standard listing_Standard, float? height = null)
        {
            return listing_Standard.GetRect(height ?? Text.LineHeight);
        }

        public static void AddLabeledRadioList(this Listing_Standard listing_Standard, string header, string[] labels, ref string val, float? headerHeight = null)
        {
            //listing_Standard.Gap();
            if (header != string.Empty) { Widgets.Label(listing_Standard.GetRect(headerHeight), header); }
            listing_Standard.AddRadioList<string>(GenerateLabeledRadioValues(labels), ref val);
        }

        private static void AddRadioList<T>(this Listing_Standard listing_Standard, List<LabeledRadioValue<T>> items, ref T val, float? height = null)
        {
            foreach (LabeledRadioValue<T> item in items)
            {
                //listing_Standard.Gap();
                Rect lineRect = listing_Standard.GetRect(height);
                if (Widgets.RadioButtonLabeled(lineRect, item.Label, EqualityComparer<T>.Default.Equals(item.Value, val)))
                    val = item.Value;
            }
        }

        private static List<LabeledRadioValue<string>> GenerateLabeledRadioValues(string[] labels)
        {
            List<LabeledRadioValue<string>> list = new List<LabeledRadioValue<string>>();
            foreach (string label in labels)
            {
                list.Add(new LabeledRadioValue<string>(label, label));
            }
            return list;
        }

        public class LabeledRadioValue<T>
        {
            private string label;
            private T val;

            public LabeledRadioValue(string label, T val)
            {
                Label = label;
                Value = val;
            }

            public string Label
            {
                get => label;
                set => label = value;
            }

            public T Value
            {
                get => val;
                set => val = value;
            }

        }

        public static void AddLabeledNumericalTextField<T>(this Listing_Standard listing_Standard, string label, ref T settingsValue, float leftPartPct = 0.5f, float minValue = 1f, float maxValue = 100000f) where T : struct
        {
            listing_Standard.Gap(12);
            listing_Standard.LineRectSpilter(out Rect leftHalf, out Rect rightHalf, leftPartPct);

            Widgets.Label(leftHalf, label);

            string buffer = settingsValue.ToString();
            Widgets.TextFieldNumeric<T>(rightHalf, ref settingsValue, ref buffer, minValue, maxValue);
        }

        public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf, out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing_Standard.LineRectSpilter(out leftHalf, leftPartPct, height);
            rightHalf = lineRect.RightPart(1f - leftPartPct).Rounded();
            return lineRect;
        }

        public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf, float leftPartPct = 0.5f, float? height = null)
        {
            Rect lineRect = listing_Standard.GetRect(height);
            leftHalf = lineRect.LeftPart(leftPartPct).Rounded();
            return lineRect;
        }
    }
}
