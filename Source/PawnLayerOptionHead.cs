using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EdB.PrepareCarefully {
    public class PawnLayerOptionHead : PawnLayerOption {
        protected (string, int) splitCamel(string input) {
            StringBuilder builder = new StringBuilder();
            int seperatorCount = 0;
            foreach(char c in input) {
                if (Char.IsUpper(c) && builder.Length > 0) {
                    seperatorCount += 1;
                    builder.Append(' ');
                };
                builder.Append(c);
            }
            return (builder.ToString(), seperatorCount);
        }
        public override string Label {
            get {
                string[] splitGraphicPath = HeadType.graphicPath.Split('_');
                (string, int) jawPart1 = splitCamel(splitGraphicPath[1]);
                return jawPart1.Item2 > 0 ? jawPart1.Item1 : splitCamel(splitGraphicPath[1]).Item1 + " " + splitGraphicPath[2];
            }
            set {
                throw new NotImplementedException();
            }
        }
        public HeadTypeDef HeadType {
            get;
            set;
        }
    }
}
