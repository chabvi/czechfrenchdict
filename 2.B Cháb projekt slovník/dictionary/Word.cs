using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionary
{
    class Word
    {
        internal string name;
        internal string french;
        internal string czech;
        internal string partofspeech;
        internal string plural;
        internal string gender;
        internal List<string> present;
        internal List<string> imperfect;
        internal List<string> future;
        internal List<string> pastsimple;
        internal List<string> subjunctive;
        public Word(string name, string fre, string cze, string wt,string pl = "" ,string gd = "", Tenses ts = null)
        {
            this.name = name;
            french = fre;
            czech = cze;
            switch (wt)
            {
                case "noun":
                    partofspeech = "podstatné jméno";
                    break;
                case "adjective":
                    partofspeech = "přídavné jméno";
                    break;
                case "pronoun":
                    partofspeech = "zájmeno";
                    break;
                case "numeral":
                    partofspeech = "číslovka";
                    break;
                case "verb":
                    partofspeech = "sloveso";
                    break;
                case "adverb":
                    partofspeech = "příslovce";
                    break;
                case "preposition":
                    partofspeech = "předložka";
                    break;
                case "conjunction":
                    partofspeech = "spojka";
                    break;
                case "particle":
                    partofspeech = "částice";
                    break;
                case "interjection":
                    partofspeech = "citoslovce";
                    break;
                default:
                    break;
            }
            if (gd == "f")
            {
                gender = "ženský";
            }
            else if (gd == "m")
            {
                gender = "mužský";
            }
            plural = pl;
            if (ts != null)
            {
                this.present = ts.present;
                this.imperfect = ts.imperfect;
                this.future = ts.future;
                this.pastsimple = ts.pastsimple;
                this.subjunctive = ts.subjunctive;
            }
        }
        public override string ToString()
        {
            return this.name;
        }
        internal class Tenses
        {
            internal List<string> present;
            internal List<string> imperfect;
            internal List<string> future;
            internal List<string> pastsimple;
            internal List<string> subjunctive;
            internal Tenses(string wt, List<string> pr, List<string> im, List<string> fu, List<string> ps, List<string> sub)
            {
                if (wt!= "verb")
                {
                    present = null;
                    imperfect = null;
                    future = null;
                    pastsimple = null;
                    subjunctive = null;
                }
                else
                {
                    present = pr;
                    imperfect = im;
                    future = fu;
                    pastsimple = ps;
                    subjunctive = sub;
                }
            }
        }
    }
}
