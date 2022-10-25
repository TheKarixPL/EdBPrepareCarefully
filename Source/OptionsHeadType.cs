using RimWorld;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace EdB.PrepareCarefully {
    public class OptionsHeadType {
        public Dictionary<string, HeadTypeDef> pathDictionary = new Dictionary<string, HeadTypeDef>();
        public List<HeadTypeDef> headTypes = new List<HeadTypeDef>();
        public OptionsHeadType() {
        }
        public void AddHeadType(HeadTypeDef headType) {
            headTypes.Add(headType);
            //Logger.Debug(headType.ToString());
            if (!headType.graphicPath.NullOrEmpty() && !pathDictionary.ContainsKey(headType.graphicPath)) {
                pathDictionary.Add(headType.graphicPath, headType);
            }
        }
        public IEnumerable<HeadTypeDef> GetHeadTypesForGender(Gender gender) {
            return headTypes.Where((HeadTypeDef headType) => {
                return (headType.gender == gender);
            });
        }
        public HeadTypeDef FindHeadTypeForPawn(Pawn pawn) {
            var result = pawn.story.headType;
            if (result == null) {
                Logger.Warning("Did not find head type for path: " + pawn.story.headType.graphicPath);
            }
            return result;
        }
        public HeadTypeDef FindHeadTypeByGraphicsPath(string graphicsPath) {
            if (pathDictionary.TryGetValue(graphicsPath, out HeadTypeDef result)) {
                return result;
            }
            else {
                return null;
            }
        }
    }
}
