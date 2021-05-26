using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace dictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {

        }

        private void lbWordlist_SelectedValueChanged(object sender, EventArgs e)
        {
            Word selectedword = (Word)lbWordlist.SelectedItem;
            rtbPlural.Clear();
            rtbGender.Clear();
            rtbCzech.Enabled = true;
            rtbFrench.Enabled = true;
            rtbPoS.Enabled = true;
            rtbCzech.Text = selectedword.czech;
            rtbFrench.Text = selectedword.french;
            rtbPoS.Text = selectedword.partofspeech;
            foreach (Control item in this.Controls)
            {
                if ((string)item.Tag == "wdetails")
                {
                    item.Enabled = false;
                }
            }
            if (selectedword.partofspeech == "podstatné jméno" || selectedword.partofspeech == "přídavné jméno")
            {
                rtbPlural.Enabled = true;
                rtbPlural.Text = selectedword.plural;
                if (selectedword.partofspeech == "podstatné jméno")
                {
                    rtbGender.Enabled = true;
                    rtbGender.Text = selectedword.gender;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load("words/wordlist.xml");
            List<Word> wordlist = new List<Word>();
            for (int i = 1; i <= doc.DocumentElement.SelectNodes("/list/word").Count; i++)
            {
                string name = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/name").InnerText;
                string french = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/french").InnerText;
                string czech = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/czech").InnerText;
                string wt = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/partofspeech").InnerText;
                string plural = null;
                string gender = null;
                List<string> present = null;
                List<string> imperfect = null;
                List<string> future = null;
                List<string> pastsimple = null;
                List<string> subjunctive = null;
                if (wt == "noun" || wt == "adjective")
                {
                    plural = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/plural").InnerText;
                    if (wt == "noun")
                    {
                        gender = doc.DocumentElement.SelectSingleNode($"/list/word[{i}]/gender").InnerText;
                    }
                }
                Word newword = new Word(name, french, czech, wt, plural, gender);
                if (wt == "verb")
                {
                    try
                    {
                        string tenses = $"/list/word[{i}]/tenses";
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='present']").ChildNodes)
                        {
                            present.Add(item.InnerText);
                        }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='pastsimple']").ChildNodes)
                        {
                            pastsimple.Add(item.InnerText);
                        }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='imperfect']").ChildNodes)
                        {
                            imperfect.Add(item.InnerText);
                        }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='future']").ChildNodes)
                        {
                            future.Add(item.InnerText);
                        }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='subjunctive']").ChildNodes)
                        {
                            subjunctive.Add(item.InnerText);
                        }
                        rtbTest.Text = present[0];
                        foreach (XmlNode item in doc.DocumentElement.SelectNodes($"/list/word[name='bois[v]']/tenses/tense[@type='present']/wordform"))
                        {
                            rtbTest.Text += $@"{item.InnerText}
";
                        }
                    }
                    catch (Exception)
                    {

                    }
                    


                }
                    wordlist.Add(newword);
            }
            wordlist.Sort((x, y) => string.Compare(x.french, y.french));
            foreach (Word item in wordlist)
            {
                lbWordlist.Items.Add(item);
            }
        }
    }
}
