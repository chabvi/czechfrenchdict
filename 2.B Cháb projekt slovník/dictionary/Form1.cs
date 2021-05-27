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
using static dictionary.Word;

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
            foreach (Control item in panelConjunction.Controls)
            {
                if (item is Panel)
                {
                    foreach (Control itemrtb in item.Controls)
                    {
                        if (itemrtb is RichTextBox)
                        {
                            itemrtb.Text = "";
                        }
                    }
                }
            }
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
            if (selectedword.partofspeech == "sloveso")
            {
                panelConjunction.Enabled = true;
                for (int i = 0; i <= 5; i++)
                {
                    foreach (Control item in panelPresent.Controls)
                    {
                        if ((string)item.Tag == $"tense{i}")
                        {
                            item.Text = selectedword.present[i];
                        }
                    }
                    foreach (Control item in panelFuture.Controls)
                    {
                        if ((string)item.Tag == $"tense{i}")
                        {
                            item.Text = selectedword.future[i];
                        }
                    }
                    foreach (Control item in panelPast.Controls)
                    {
                        if ((string)item.Tag == $"tense{i}")
                        {
                            item.Text = selectedword.pastsimple[i];
                        }
                    }
                    foreach (Control item in panelImperfect.Controls)
                    {
                        if ((string)item.Tag == $"tense{i}")
                        {
                            item.Text = selectedword.imperfect[i];
                        }
                    }
                    foreach (Control item in panelSubjunctive.Controls)
                    {
                        if ((string)item.Tag == $"tense{i}")
                        {
                            item.Text = selectedword.subjunctive[i];
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
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

                if (wt == "verb")
                {
                        present = new List<string>();
                        imperfect = new List<string>();
                        future = new List<string>();
                        pastsimple = new List<string>();
                        subjunctive = new List<string>();
                        string tenses = $"/list/word[{i}]/tenses";
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='present']").ChildNodes)
                        {
                        string tense = item.InnerText;
                        present.Add(tense);
                        }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='pastsimple']").ChildNodes)
                        {
                        string tense = item.InnerText;
                        pastsimple.Add(tense);
                    }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='imperfect']").ChildNodes)
                        {
                        string tense = item.InnerText;
                        imperfect.Add(tense);
                    }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='future']").ChildNodes)
                        {
                        string tense = item.InnerText;
                        future.Add(tense);
                    }
                        foreach (XmlNode item in doc.DocumentElement.SelectSingleNode(tenses + "/tense[@type='subjunctive']").ChildNodes)
                        {
                        string tense = item.InnerText;
                        subjunctive.Add(tense);
                    }
                }
                Tenses wordtenses = new Tenses(wt, present, imperfect, future, pastsimple, subjunctive);
                Word newword = new Word(name, french, czech, wt, plural, gender, wordtenses);
                wordlist.Add(newword);
            }
            wordlist.Sort((x, y) => string.Compare(x.french, y.french));
            foreach (Word item in wordlist)
            {
                lbWordlist.Items.Add(item);
            }
        }

        private void ButSearch_Click(object sender, EventArgs e)
        {
            FormSearch formSearch = new FormSearch();
            formSearch.ShowDialog();
        }
    }
}
