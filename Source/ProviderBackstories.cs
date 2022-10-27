using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
namespace EdB.PrepareCarefully {
    public class ProviderBackstories {
        protected List<BackstoryDef> childhoodBackstories = SolidBioDatabase.allBios.ConvertAll<BackstoryDef>(bio => bio.childhood);
        protected List<BackstoryDef> adulthoodBackstories = SolidBioDatabase.allBios.ConvertAll<BackstoryDef>(bio => bio.adulthood);
        protected List<BackstoryDef> sortedChildhoodBackstories;
        protected List<BackstoryDef> sortedAdulthoodBackstories;

        protected Dictionary<string, List<BackstoryDef>> childhoodBackstoryLookup = new Dictionary<string, List<BackstoryDef>>();
        protected Dictionary<string, List<BackstoryDef>> adulthoodBackstoryLookup = new Dictionary<string, List<BackstoryDef>>();
        protected Dictionary<string, HashSet<BackstoryDef>> backstoryHashSetLookup = new Dictionary<string, HashSet<BackstoryDef>>();

        public List<BackstoryDef> GetChildhoodBackstoriesForPawn(CustomPawn pawn) {
            return GetChildhoodBackstoriesForPawnKindDef(pawn.OriginalKindDef);
        }
        public List<BackstoryDef> GetAdulthoodBackstoriesForPawn(CustomPawn pawn) {
            return GetAdulthoodBackstoriesForPawnKindDef(pawn.OriginalKindDef);
        }
        public List<BackstoryDef> GetChildhoodBackstoriesForPawnKindDef(PawnKindDef kindDef) {
            if (!backstoryHashSetLookup.ContainsKey(kindDef.defName)) {
                InitializeBackstoriesForPawnKind(kindDef);
            }
            return childhoodBackstoryLookup[kindDef.defName];
        }
        public List<BackstoryDef> GetAdulthoodBackstoriesForPawnKindDef(PawnKindDef kindDef) {
            if (!backstoryHashSetLookup.ContainsKey(kindDef.defName)) {
                InitializeBackstoriesForPawnKind(kindDef);
            }
            return adulthoodBackstoryLookup[kindDef.defName];
        }
        public HashSet<BackstoryDef> BackstoriesForPawnKindDef(PawnKindDef kindDef) {
            if (!backstoryHashSetLookup.ContainsKey(kindDef.defName)) {
                InitializeBackstoriesForPawnKind(kindDef);
            }
            return backstoryHashSetLookup[kindDef.defName];
        }
        public List<BackstoryDef> AllChildhoodBackstories {
            get {
                return sortedChildhoodBackstories;
            }
        }
        public List<BackstoryDef> AllAdulthoodBackstories {
            get {
                return sortedAdulthoodBackstories;
            }
        }
        public ProviderBackstories() {
            // Create sorted versions of the backstory lists
            sortedChildhoodBackstories = new List<BackstoryDef>(childhoodBackstories);
            sortedChildhoodBackstories.Sort((b1, b2) => b1.TitleCapFor(Gender.Male).CompareTo(b2.TitleCapFor(Gender.Male)));
            sortedAdulthoodBackstories = new List<BackstoryDef>(adulthoodBackstories);
            sortedAdulthoodBackstories.Sort((b1, b2) => b1.TitleCapFor(Gender.Male).CompareTo(b2.TitleCapFor(Gender.Male)));
        }

        private void InitializeBackstoriesForPawnKind(PawnKindDef def) {
            HashSet<string> categories = BackstoryCategoriesForPawnKindDef(def);
            Func<List<BackstoryDef>, List<BackstoryDef>> reduceAndSortBackstories = (List<BackstoryDef> backStories) => {
                return backStories.Aggregate(new List<BackstoryDef>(), (acc, backStory) => {
                    if (backStory.spawnCategories.Aggregate(false, (acc1, c) => acc1 == true ? acc1 : categories.Contains(c))) {
                        return acc.Concat(new[] {backStory}).ToList();
                    } else {
                        return acc;
                    };
                })
                .OrderBy(x => x.TitleCapFor(Gender.Male)).ToList();
            };
            List<BackstoryDef> childhood = reduceAndSortBackstories(childhoodBackstories);
            childhoodBackstoryLookup[def.defName] = childhood;

            List<BackstoryDef> adulthood = reduceAndSortBackstories(adulthoodBackstories);
            adulthoodBackstoryLookup[def.defName] = adulthood;

            HashSet<BackstoryDef> backstorySet = new HashSet<BackstoryDef>(childhood);
            backstorySet.AddRange(adulthood);
            this.backstoryHashSetLookup[def.defName] = backstorySet;
        }

        public HashSet<string> BackstoryCategoriesForPawnKindDef(PawnKindDef kindDef) {
            Faction faction = PrepareCarefully.Instance.Providers.Factions.GetFaction(kindDef);
            var filters = GetBackstoryCategoryFiltersFor(kindDef, faction != null ? faction.def : null);
            return AllBackstoryCategoriesFromFilterList(filters);
        }

        // EVERY RELEASE:
        // Evaluate to make sure the logic in PawnBioAndNameGenerator.GetBackstoryCategoryFiltersFor() has not changed in a way
        // that invalidates this rewrite. This is a modified version of that method but with the first argument a PawnKindDef
        // instead of a Pawn and with logging removed.
        private List<BackstoryCategoryFilter> GetBackstoryCategoryFiltersFor(PawnKindDef kindDef, FactionDef faction) {
            if (kindDef.pawnGroupDevelopmentStage.HasValue) {
                if (kindDef.pawnGroupDevelopmentStage.Value == DevelopmentalStage.Baby || kindDef.pawnGroupDevelopmentStage.Value == DevelopmentalStage.Newborn) {
                    return new List<BackstoryCategoryFilter>(){ 
                        new BackstoryCategoryFilter() { categories = new List<string>() { "Newborn" }, commonality = 1f } 
                    };
                }
                if (kindDef.pawnGroupDevelopmentStage.Value == DevelopmentalStage.Child) {
                    return new List<BackstoryCategoryFilter>(){
                        new BackstoryCategoryFilter() { categories = new List<string>() { "Child" }, commonality = 1f }
                    };
                }
            }
            if (!kindDef.backstoryFiltersOverride.NullOrEmpty<BackstoryCategoryFilter>()) {
                return kindDef.backstoryFiltersOverride;
            }
            List<BackstoryCategoryFilter> list = new List<BackstoryCategoryFilter>();
            if (kindDef.backstoryFilters != null) {
                list.AddRange(kindDef.backstoryFilters);
            }
            if (faction != null && !faction.backstoryFilters.NullOrEmpty<BackstoryCategoryFilter>()) {
                for (int i = 0; i < faction.backstoryFilters.Count; i++) {
                    BackstoryCategoryFilter item = faction.backstoryFilters[i];
                    if (!list.Contains(item)) {
                        list.Add(item);
                    }
                }
            }
            if (!list.NullOrEmpty<BackstoryCategoryFilter>()) {
                return list;
            }
            return new List<BackstoryCategoryFilter> {
                new BackstoryCategoryFilter {
                    categories = new List<string> {
                        "Civil"
                    },
                    commonality = 1f
                }
            };
        }

        private HashSet<string> AllBackstoryCategoriesFromFilterList(List<BackstoryCategoryFilter> filterList) {
            HashSet<string> result = new HashSet<string>();
            filterList.ForEach(f => f.categories.ForEach(c => result.Add(c)));
            return result;
        }
    }
}
