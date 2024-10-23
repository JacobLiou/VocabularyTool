using EntryTranslator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntryTranslator.Utils
{
    internal class CultureLangHelper
    {
        private static IEnumerable<string> _cultures;

        public static async Task<List<CultureModel>> GetLanguageList()
        {
            if (_cultures == null)
                _cultures = await GetProperCulturesAsync();
            var languageList = await Task.Factory.StartNew(() =>
                _cultures.Select(x =>
                    new CultureModel { Code = x, Name = CultureInfo.GetCultureInfo(x).DisplayName }).ToList());
            return languageList;
        }

        private static async Task<IEnumerable<string>> GetProperCulturesAsync()
        {
            var allCodes = await Task.Factory.StartNew(() => CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name));

            try
            {
                var downloadedCodes = GetLanguageCodesOffline();
                var properCodes = await Task.Factory.StartNew(() => allCodes.Where(x => downloadedCodes.Contains(x)));

                return properCodes ?? allCodes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            GC.Collect();
            return allCodes;
        }

        private static List<string> GetLanguageCodesOffline()
        {
            //I'm taking a shortcut in here.
            return ("af;af-NA;agq;ak;am;ar;ar-AE;ar-BH;ar-DJ;ar-DZ;ar-EG;ar-ER;ar-IL;ar-IQ;ar-JO;ar-KM;ar-KW;ar-LB;ar-LY;ar-MA;ar-MR;ar-OM;ar-PS;ar-QA;ar-SA;ar-SD;ar-SO;" +
                    "ar-SS;ar-SY;ar-TD;ar-TN;ar-YE;as;asa;ast;az;az-Cyrl;bas;be;bem;bez;bg;bm;bn;bn-IN;bo;bo-IN;br;brx;bs;bs-Cyrl;ca;ca-FR;ccp;ce;ceb;cgg;chr;cs;cu;cy;da;" +
                    "dav;de;de-AT;de-CH;de-IT;de-LI;de-LU;dje;dsb;dua;dyo;dz;ebu;ee;ee-TG;el;en;en-001;en-150;en-AE;en-AG;en-AI;en-AT;en-AU;en-BB;en-BE;en-BI;en-BM;en-BS;" +
                    "en-BW;en-BZ;en-CA;en-CC;en-CH;en-CK;en-CM;en-CX;en-DE;en-DK;en-DM;en-ER;en-FI;en-FJ;en-FK;en-GB;en-GD;en-GG;en-GH;en-GI;en-GM;en-GU;en-GY;en-HK;en-IE;" +
                    "en-IL;en-IM;en-IN;en-IO;en-JE;en-JM;en-KE;en-KI;en-KN;en-KY;en-LC;en-LR;en-LS;en-MG;en-MH;en-MO;en-MP;en-MS;en-MT;en-MU;en-MW;en-MY;en-NA;en-NF;en-NG;" +
                    "en-NL;en-NR;en-NU;en-NZ;en-PG;en-PH;en-PK;en-PN;en-PW;en-RW;en-SB;en-SC;en-SD;en-SE;en-SG;en-SH;en-SI;en-SL;en-SS;en-SX;en-SZ;en-TK;en-TO;en-TT;en-TV;" +
                    "en-TZ;en-UG;en-VC;en-VU;en-WS;en-ZA;en-ZM;en-ZW;eo;es;es-419;es-AR;es-BO;es-BR;es-BZ;es-CL;es-CO;es-CR;es-CU;es-DO;es-EC;es-GQ;es-GT;es-HN;es-MX;es-NI;" +
                    "es-PA;es-PE;es-PH;es-PR;es-PY;es-SV;es-US;es-UY;es-VE;et;eu;ewo;fa;ff;ff-Latn-GH;ff-Latn-GM;ff-Latn-GN;ff-Latn-LR;ff-Latn-MR;ff-Latn-NG;ff-Latn-SL;fi;fil;" +
                    "fo;fo-DK;fr;fr-BE;fr-BI;fr-CA;fr-CD;fr-CH;fr-CI;fr-CM;fr-DJ;fr-DZ;fr-GF;fr-GN;fr-HT;fr-KM;fr-LU;fr-MA;fr-MG;fr-ML;fr-MR;fr-MU;fr-RE;fr-RW;fr-SC;fr-SN;fr-SY;" +
                    "fr-TD;fr-TN;fr-VU;fur;fy;ga;gd;gl;gsw;gu;guz;gv;ha;haw;he;hi;hr;hr-BA;hsb;hu;hy;ia;id;ig;ii;is;it;it-CH;ja;jgo;jmc;jv;ka;kab;kam;kde;kea;khq;ki;kk;kkj;kl;kln;" +
                    "km;kn;ko;ko-KP;kok;ks;ksb;ksf;ksh;ku;kw;ky;lag;lb;lg;lkt;ln;ln-AO;lo;lrc;lrc-IQ;lt;lu;luo;luy;lv;mas;mas-TZ;mer;mfe;mg;mgh;mgo;mi;mk;ml;mn;mni;mr;ms;ms-BN;ms-SG;" +
                    "mt;mua;my;mzn;naq;nb;nd;nds;nds-NL;ne;ne-IN;nl;nl-AW;nl-BE;nl-BQ;nl-CW;nl-SR;nl-SX;nmg;nn;nnh;nus;nyn;om;om-KE;or;os;os-RU;pa;pa-Arab;pl;prg;ps;ps-PK;pt;pt-AO;" +
                    "pt-CV;pt-GW;pt-LU;pt-MO;pt-MZ;pt-PT;pt-ST;pt-TL;rm;rn;ro;ro-MD;rof;ru;ru-BY;ru-KG;ru-KZ;ru-MD;ru-UA;rw;rwk;sah;saq;sbp;sd;sd-Deva;se;se-FI;se-SE;seh;ses;sg;shi;" +
                    "shi-Latn;si;sk;sl;smn;sn;so;so-DJ;so-ET;so-KE;sq;sq-MK;sq-XK;sr;sr-Cyrl-BA;sr-Cyrl-ME;sr-Cyrl-XK;sr-Latn;sr-Latn-BA;sr-Latn-ME;sr-Latn-XK;sv;sv-FI;sw;sw-CD;sw-KE;" +
                    "sw-UG;ta;ta-LK;ta-MY;ta-SG;te;teo;teo-KE;tg;th;ti;ti-ER;tk;to;tr;tr-CY;tt;twq;tzm;ug;uk;ur;ur-IN;uz;uz-Arab;uz-Cyrl;vai;vai-Latn;vi;vo;vun;wae;wo;xh;xog;yav;yi;yo;" +
                    "yo-BJ;zgh;zh;zh-Hans-HK;zh-Hans-MO;zh-Hant;zu").Split(';').ToList();
        }
    }
}