using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace EdB.PrepareCarefully {
    class PawnGenerationRequestWrapper {
        private PawnKindDef kindDef = Faction.OfPlayer.def.basicMemberKind;
        private Faction faction = Faction.OfPlayer;
        private PawnGenerationContext context = PawnGenerationContext.PlayerStarter;
        private float? fixedBiologicalAge = null;
        private float? fixedChronologicalAge = null;
        private Gender? fixedGender = null;
        private bool worldPawnFactionDoesntMatter = false;
        private bool mustBeCapableOfViolence = false;
        private Ideo fixedIdeology = null;
        public PawnGenerationRequestWrapper() {
        }
        private PawnGenerationRequest CreateRequest() {
            //public PawnGenerationRequest (

            //string fixedBirthName = null, 
            //RoyalTitleDef fixedTitle = null)

            /*
             * PawnKindDef kind,
             * Faction faction = null,
             * PawnGenerationContext context = PawnGenerationContext.NonPlayer,
             * int tile = -1,
             * bool forceGenerateNewPawn = false,
             * bool allowDead = false,
             * bool allowDowned = false,
             * bool canGeneratePawnRelations = true,
             * bool mustBeCapableOfViolence = false,
             * float colonistRelationChanceFactor = 1,
             * bool forceAddFreeWarmLayerIfNeeded = false,
             * bool allowGay = true,
             * bool allowPregnant = false,
             * bool allowFood = true,
             * bool allowAddictions = true,
             * bool inhabitant = false,
             * bool certainlyBeenInCryptosleep = false,
             * bool forceRedressWorldPawnIfFormerColonist = false,
             * bool worldPawnFactionDoesntMatter = false,
             * float biocodeWeaponChance = 0,
             * float biocodeApparelChance = 0,
             * Pawn extraPawnForExtraRelationChance = null,
             * float relationWithExtraPawnChanceFactor = 1,
             * Predicate<Pawn> validatorPreGear = null,
             * Predicate<Pawn> validatorPostGear = null,
             * IEnumerable<TraitDef> forcedTraits = null,
             * IEnumerable<TraitDef> prohibitedTraits = null,
             * float? minChanceToRedressWorldPawn = null,
             * float? fixedBiologicalAge = null,
             * float? fixedChronologicalAge = null,
             * Gender? fixedGender = null,
             * string fixedLastName = null,
             * string fixedBirthName = null,
             * RoyalTitleDef fixedTitle = null,
             * Ideo fixedIdeo = null,
             * bool forceNoIdeo = false,
             * bool forceNoBackstory = false,
             * bool forbidAnyTitle = false,
             * bool forceDead = false,
             * List<GeneDef> forcedXenogenes = null,
             * List<GeneDef> forcedEndogenes = null,
             * XenotypeDef forcedXenotype = null,
             * CustomXenotype forcedCustomXenotype = null,
             * List<XenotypeDef> allowedXenotypes = null,
             * float forceBaselinerChance = 0,
             * DevelopmentalStage developmentalStages = DevelopmentalStage.Adult,
             * Func<XenotypeDef, PawnKindDef> pawnKindDefGetter = null,
             * FloatRange? excludeBiologicalAgeRange = null,
             * FloatRange? biologicalAgeRange = null,
             * bool forceRecruitable = false 
            */

            return new PawnGenerationRequest(
                kindDef,// kind,
                faction,// faction = null,
                context,// context = PawnGenerationContext.NonPlayer,
                -1,// tile = -1,
                false,// forceGenerateNewPawn = false,
                false,// allowDead = false,
                false,// allowDowned = false,
                true,// canGeneratePawnRelations = true,
                false,// mustBeCapableOfViolence = false,
                1,// colonistRelationChanceFactor = 1,
                false,// forceAddFreeWarmLayerIfNeeded = false,
                true,// allowGay = true,
                false,// allowPregnant = false,
                true,// allowFood = true,
                true,// allowAddictions = true,
                false,// inhabitant = false,
                false,// certainlyBeenInCryptosleep = false,
                false,// forceRedressWorldPawnIfFormerColonist = false,
                false,// worldPawnFactionDoesntMatter = false,
                0,// biocodeWeaponChance = 0,
                0,// biocodeApparelChance = 0,
                null,// extraPawnForExtraRelationChance = null,
                1,// relationWithExtraPawnChanceFactor = 1,
                null,// validatorPreGear = null,
                null,// validatorPostGear = null,
                null,// forcedTraits = null,
                null,// prohibitedTraits = null,
                null,// minChanceToRedressWorldPawn = null,
                null,// fixedBiologicalAge = null,
                null,// fixedChronologicalAge = null,
                null,// fixedGender = null,
                null,// fixedLastName = null,
                null,// fixedBirthName = null,
                null,// fixedTitle = null,
                null,// fixedIdeo = null,
                false,// forceNoIdeo = false,
                false,// forceNoBackstory = false,
                false,// forbidAnyTitle = false,
                false,// forceDead = false,
                null,// forcedXenogenes = null,
                null,// forcedEndogenes = null,
                null,// forcedXenotype = null,
                null,// forcedCustomXenotype = null,
                null,// allowedXenotypes = null,
                0,// forceBaselinerChance = 0,
                DevelopmentalStage.Adult,// developmentalStages = DevelopmentalStage.Adult,
                null,// Func<XenotypeDef, PawnKindDef> pawnKindDefGetter = null,
                null,// excludeBiologicalAgeRange = null,
                null,// biologicalAgeRange = null,
                false // forceRecruitable = false 
            ) {
                ForbidAnyTitle = true
            };
        }
        public PawnGenerationRequest Request {
            get {
                return CreateRequest();
            }
        }
        public PawnKindDef KindDef {
            set {
                kindDef = value;
            }
        }
        public Faction Faction {
            set {
                faction = value;
            }
        }
        public PawnGenerationContext Context {
            set {
                context = value;
            }
        }
        public bool WorldPawnFactionDoesntMatter {
            set {
                worldPawnFactionDoesntMatter = value;
            }
        }
        public float? FixedBiologicalAge {
            set {
                fixedBiologicalAge = value;
            }
        }
        public float? FixedChronologicalAge {
            set {
                fixedChronologicalAge = value;
            }
        }
        public Gender? FixedGender {
            set {
                fixedGender = value;
            }
        }
        public bool MustBeCapableOfViolence {
            set {
                mustBeCapableOfViolence = value;
            }
        }
        public Ideo FixedIdeology {
            set {
                fixedIdeology = value;
            }
        }
    }
}
