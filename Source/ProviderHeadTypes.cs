using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace EdB.PrepareCarefully {
    public class ProviderHeadTypes {
        public ProviderAlienRaces AlienRaceProvider {
            get; set;
        }
        public IEnumerable<HeadTypeDef> GetHeadTypes(ThingDef race, Gender gender) {
            OptionsHeadType headTypes = InitializeHeadTypes();
            return headTypes.GetHeadTypesForGender(gender);
        }
        public HeadTypeDef FindHeadTypeForPawn(Pawn pawn) {
            OptionsHeadType headTypes = InitializeHeadTypes();
            var result = headTypes.FindHeadTypeForPawn(pawn);
            if (result == null) {
                Logger.Warning("Could not find a head type for the pawn: " + pawn.def.defName + ". Head type selection disabled for this pawn");
            }
            return result;
        }
        protected OptionsHeadType InitializeHeadTypes() {
            List<HeadTypeDef> headTypeDefs = DefDatabase<HeadTypeDef>.AllDefs.ToList();
            OptionsHeadType result = new OptionsHeadType();
            headTypeDefs.ForEach((headTypeDef) => result.AddHeadType(headTypeDef));
            return result;
        }
    }
}
