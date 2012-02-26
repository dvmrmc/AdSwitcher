using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSwitcher.Code.AdProvider
{
    public class AdProviderSettings
    {
        public float Priority;
        public string Name;

        //=============================
        // PUBCENTER
        //=============================

        public string PubcenterAdUnitID = "";
        public string PubcenterApplicationID = "";

        //=============================
        // ADMOB
        //=============================

        public string AdmobAdUnitID = "a14e67895bdffff";

        //=============================
        // SMAATO
        //=============================

        public string SmaatoSpaceID = "";

        //=============================
        // MILLENIALMEDIA
        //=============================

        public string MillenialmediaAppID = "77095";

        //=============================
        // ADDUPLEX
        //=============================

        public string AdduplexAdID = "8703";

        //=============================
        // MOBFOX
        //=============================

        public string MobfoxPublisherID = "808ef87c90af8866e0ac43733b5580be";
    }
}
