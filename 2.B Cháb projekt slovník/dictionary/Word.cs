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
        public Word(string name, string fre, string cze, string wt,string pl = "" ,string gd = "")
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
        }
        public override string ToString()
        {
            return this.name;
        }
        internal class Tenses
        {
            List<string> present;
            List<string> imperfect;
            List<string> future;
            List<string> pastsimple;
            List<string> subjunctive;
            internal Tenses(List<string> pr, List<string> im, List<string> fu, List<string> ps, List<string> sub, string wt)
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
