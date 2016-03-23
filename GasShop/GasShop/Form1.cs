using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace GasShop
{
    public partial class GasShop : Form
    {
        private Word.Application wordapp;
        private double allSum = 0;
        public GasShop()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string str="Общая выручка за день: "+allSum.ToString()+"\nВы уверены, что хотите выйти?";
            if(MessageBox.Show(str, "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void chkHotDog_Click(object sender, EventArgs e)
        {
            double Summa = 0;
            try
            {
                if(chkHotDog.Checked)
                {
                    tbQuantityHotDog.Enabled = true;
                    Summa += Convert.ToDouble(tbQuantityHotDog.Text) * Convert.ToDouble(tbCostHotDog.Text);
                }
                else
                {
                    tbQuantityHotDog.Enabled = false;
                }
                if(chkGamburger.Checked)
                {
                    tbQuantityGamburger.Enabled = true;
                    Summa += Convert.ToDouble(tbQuantityGamburger.Text) * Convert.ToDouble(tbCostGamburger.Text);
                }
                else
                {
                    tbQuantityGamburger.Enabled = false;
                }
                if(chkChizburger.Checked)
                {
                    tbQuantityChizburger.Enabled = true;
                    Summa += Convert.ToDouble(tbQuantityChizburger.Text) * Convert.ToDouble(tbCostChizburger.Text);
                }
                else
                {
                    tbQuantityChizburger.Enabled = false;
                }
                if(chkCocaCola.Checked)
                {
                    tbQuantityCocaCola.Enabled = true;
                    Summa += Convert.ToDouble(tbQuantityCocaCola.Text) * Convert.ToDouble(tbCostCocaCola.Text);
                }
                else
                {
                    tbQuantityCocaCola.Enabled = false;
                }

                tbPayMiniCafe.Text = Summa.ToString();

                if (radCount.Checked)
	            {
                    Summa = Convert.ToDouble(tbPayMiniCafe.Text) + Convert.ToDouble(tbPayGas.Text);
	            }
                else
                {
                    Summa = Convert.ToDouble(tbPayMiniCafe.Text) + Convert.ToDouble(tbSumCostGas.Text);
                }
                
                tbCalculate.Text = Summa.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void GasShop_Load(object sender, EventArgs e)
        {
            List<Subject> Subjects = new List<Subject>(){
                new Subject(){Num=11000,Name="A-95"},
                new Subject(){Num=10000,Name="A-92"},
                new Subject(){Num=12000,Name="ДТ"}
            };
            cmbGas.DataSource = Subjects;
            cmbGas.DisplayMember = "Name";
        }

        private void cmbGas_SelectedIndexChanged(object sender, EventArgs e)
        {
            double Summa = 0;

            try
            {
                if(cmbGas.SelectedItem != null)
                {
                    Subject selSubject = (Subject)cmbGas.SelectedItem;
                    tbCostGas.Text = selSubject.Num.ToString();
                    if(radCount.Checked)
                    {
                        groupBox3.Text = "К оплате";
                        tbSumCostGas.Enabled = false;
                        tbQuantityGas.Enabled = true;
                        Summa = Convert.ToDouble(tbQuantityGas.Text) * Convert.ToDouble(tbCostGas.Text);
                        tbPayGas.Text = Summa.ToString();
                        Summa = Convert.ToDouble(tbPayMiniCafe.Text) + Convert.ToDouble(tbPayGas.Text);
                        
                    }
                    if(radSum.Checked)
                    {
                        groupBox3.Text = "К выдаче";
                        tbQuantityGas.Enabled = false;
                        tbSumCostGas.Enabled = true;
                        Summa = (Convert.ToDouble(tbSumCostGas.Text) / Convert.ToDouble(tbCostGas.Text));
                        tbPayGas.Text = Summa.ToString();
                        Summa = Convert.ToDouble(tbPayMiniCafe.Text) + Convert.ToDouble(tbSumCostGas.Text);
                    }
                }
                tbCalculate.Text = Summa.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(tbCalculate.Text) == 0)
            {
                MessageBox.Show("Невозможно выбить пустой чек","Внимание",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            wordapp = new Word.Application();
            wordapp.Visible = true;
            Word.Paragraph wordParagraph;
            Word.Document doc = new Word.Document();
            object MyTemplate = Type.Missing;
            object NewTemplate = false;
            object DocumentType = Word.WdNewDocumentType.wdNewBlankDocument;
            object Visible = true;
            doc = wordapp.Documents.Add(ref MyTemplate, ref NewTemplate, ref DocumentType, ref Visible);

            object pargf = Type.Missing;
            wordParagraph = doc.Content.Paragraphs.Add(ref pargf);
            Microsoft.Office.Interop.Word.Table tbl1 = null;
            object DefaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;
            
            string[] ListGas = new string[] { grpGas.Text, label2.Text, radCount.Text, radSum.Text };
            string[] List;
            if((Convert.ToInt32(tbQuantityGas.Text) != 0) || (Convert.ToInt32(tbSumCostGas.Text) != 0))
            {
                if(radCount.Checked)
                {
                    List = new string[] { cmbGas.Text, tbCostGas.Text, tbQuantityGas.Text, tbPayGas.Text };
                }
                else
                {
                    List = new string[] { cmbGas.Text, tbCostGas.Text, tbPayGas.Text, tbSumCostGas.Text };
                }
                tbl1 = doc.Content.Tables.Add(wordParagraph.Range, ListGas.Count(), 2, ref DefaultTableBehavior, ref AutoFitBehavior);
                tbl1.Borders.Enable = 1;
                for(int i = 0; i < ListGas.Count(); i++)
                {
                    tbl1.Cell(i + 1, 1).Range.Text = ListGas[i];
                    tbl1.Cell(i + 1, 2).Range.Text = List[i];
                }
            }

            string[] ListMag = new string[4];
            string[] ListQuantityMag = new string[4];
            string[] ListCostMag = new string[4];
            int count = 0, sum = 0;
            if((Convert.ToInt32(tbQuantityHotDog.Text) != 0) && chkHotDog.Checked)
            {
                ListMag[count] += chkHotDog.Text;
                ListQuantityMag[count] = tbQuantityHotDog.Text;
                ListCostMag[count] = tbCostHotDog.Text;
                count++;
                sum += (Convert.ToInt32(tbQuantityHotDog.Text) * Convert.ToInt32(tbCostHotDog.Text));
            }
            if((Convert.ToInt32(tbQuantityGamburger.Text) != 0) && chkGamburger.Checked)
            {
                ListMag[count] = chkGamburger.Text;
                ListQuantityMag[count] = tbQuantityGamburger.Text;
                ListCostMag[count] = tbCostGamburger.Text;
                count++;
                sum += (Convert.ToInt32(tbQuantityGamburger.Text) * Convert.ToInt32(tbCostGamburger.Text));
            }
            if((Convert.ToInt32(tbQuantityChizburger.Text) != 0) && chkChizburger.Checked)
            {
                ListMag[count] = chkChizburger.Text;
                ListQuantityMag[count] = tbQuantityChizburger.Text;
                ListCostMag[count] = tbCostChizburger.Text;
                count++;
                sum += (Convert.ToInt32(tbQuantityChizburger.Text) * Convert.ToInt32(tbCostChizburger.Text));
            }
            if((Convert.ToInt32(tbQuantityCocaCola.Text) != 0) && chkCocaCola.Checked)
            {
                ListMag[count] = chkCocaCola.Text;
                ListQuantityMag[count] = tbQuantityCocaCola.Text;
                ListCostMag[count] = tbCostCocaCola.Text;
                count++;
                sum += (Convert.ToInt32(tbQuantityCocaCola.Text) * Convert.ToInt32(tbCostCocaCola.Text));
            }
            if(count != 0)
            {
                if(tbl1 != null)
                {
                    wordParagraph.Range.Font.Size = 1;
                    wordParagraph.Range.InsertParagraphAfter();
                    wordParagraph.Range.Font.Size = 11;
                }
                tbl1 = doc.Content.Tables.Add(wordParagraph.Range, count + 1, 3, ref DefaultTableBehavior, ref AutoFitBehavior);
                tbl1.Borders.Enable = 1;
                for(int i = 0; i < count; i++)
                {
                    tbl1.Cell(i + 1, 1).Range.Text = ListMag[i];
                    tbl1.Cell(i + 1, 2).Range.Text = ListQuantityMag[i];
                    tbl1.Cell(i + 1, 3).Range.Text = ListCostMag[i];
                }
                tbl1.Cell(count + 1, 1).Range.Text = "Всего";
                tbl1.Cell(count + 1, 3).Range.Text = sum.ToString();
            }

            wordParagraph.Range.Bold = 2;
            wordParagraph.Range.Text = label12.Text + " " + tbCalculate.Text;
            allSum += Convert.ToDouble(tbCalculate.Text);
            tbQuantityGas.Text = tbSumCostGas.Text = tbQuantityHotDog.Text = tbQuantityGamburger.Text = tbQuantityChizburger.Text = tbQuantityCocaCola.Text = "0";
            chkHotDog.Checked = chkGamburger.Checked = chkChizburger.Checked = chkCocaCola.Checked = false;
            tbQuantityHotDog.Enabled = tbQuantityGamburger.Enabled = tbQuantityChizburger.Enabled = tbQuantityCocaCola.Enabled = false;
            radCount.Checked = true;
        }
    }
    class Subject
    {
        public int Num
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
