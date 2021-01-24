using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogPencereleri
{

    public partial class Form1 : Form
    {
        MyFirstERP_DBEntities db = new MyFirstERP_DBEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void KaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = saveFileDialog1.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Metin Belgesi (*.txt)|*.txt|Zengin Metin Biçimi(*.rtf)|*.rtf|Tüm Dosyalar(*.*)|*.*";
            //saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "Metin Belgesi (*.txt)|*.txt|Zengin Metin Biçimi(*.rtf)|*.rtf";
            toolStripTextBox1.Text = richTextBox1.Font.Name;
            FontDoldur();
        }


        private void AcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.LoadFile(openFileDialog1.FileName,RichTextBoxStreamType.RichText);
                    
                }
                catch
                {
                    try
                    {
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    }
                    catch
                    {
                        MessageBox.Show(openFileDialog1.FileName + "Bu dosya açılmıyor.", "Dosya bozuk");
                    }
                }
            }
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                //richTextBox1.Font = fontDialog1.Font;
                richTextBox1.SelectionFont = fontDialog1.Font;
                toolStripTextBox1.Text = fontDialog1.Font.Name;
                toolStripComboBox1.SelectedIndex = toolStripComboBox1.Items.IndexOf(richTextBox1.SelectionFont.Name);
            }
        }


        private void FontDoldur()
        {
            foreach (FontFamily item in FontFamily.Families)
            {
                toolStripComboBox1.Items.Add(item.Name);
            }
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secili = toolStripComboBox1.Text;
            FontFamily fnt = FontFamily.Families.FirstOrDefault(s => s.Name == secili);
            richTextBox1.SelectionFont = new Font(fnt, richTextBox1.SelectionFont.Size);
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color renk = colorDialog1.Color;
                toolStripButton5.BackColor = renk;
                richTextBox1.SelectionColor = renk;
            }
        }

        private void RichTextBox1_FontChanged(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = richTextBox1.SelectionFont.Name;
            toolStripComboBox1.SelectedIndex = toolStripComboBox1.Items.IndexOf(richTextBox1.SelectionFont.Name);
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            RichTextBoxTabloOlusturma();
        }
        private void RichTextBoxTabloOlusturma()
        {
            StringBuilder strTablo = new StringBuilder();
            strTablo.Append(@"{\rtf1 ");
            List<AltKategoriler> alt = db.AltKategoriler.ToList();
            for (int i = 0; i < alt.Count; i++)
            {
                strTablo.Append(@"\trowd");
                strTablo.Append(@"\cellx1000 " + alt[i].ID);
                //strTablo.Append(@"\intbl\cell");
                //strTablo.Append(@"\cellx3000 " + alt[i].Ad);
                strTablo.Append(@"\intbl\cell\row");
            }
            for (int i = 0; i < alt.Count; i++)
            {
                strTablo.Append(@"\trowd");
                //strTablo.Append(@"\cellx1000 " + alt[i].ID);
                ////strTablo.Append(@"\intbl\cell");
                strTablo.Append(@"\cellx3000 " + alt[i].Ad);
                strTablo.Append(@"\intbl\cell\row");
            }
            strTablo.Append(@"\pard ");
            strTablo.Append(@"} ");

            richTextBox1.Rtf = strTablo.ToString();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            Color renk = colorDialog1.Color;
            MessageBox.Show(renk.Name);
            
        }
    }
}
