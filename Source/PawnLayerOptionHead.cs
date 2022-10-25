using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace EdB.PrepareCarefully {
    public class PawnLayerOptionHead : PawnLayerOption {
        public override string Label {
            get {
                return HeadType.label;
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
