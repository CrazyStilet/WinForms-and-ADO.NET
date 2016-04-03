using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MobileStore
{
    public partial class MobileStore : Form
    {
        public MobileStore()
        {
            InitializeComponent();
            lstPhone.SelectionMode = SelectionMode.One;
            lstOptions.SelectionMode = SelectionMode.None;
        }
        private void MobileStore_Load(object sender, EventArgs e)
        {
            List<PhoneOption> allPhoneOptions = new List<PhoneOption>();
            try
            {
                using(FileStream file = new FileStream("ListPhoneOptions.xml", FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<PhoneOption>));
                    allPhoneOptions = (List<PhoneOption>)xmlFormat.Deserialize(file);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            chlstOptions.DataSource = allPhoneOptions;

            List<Phone> Phones;

            try
            {
                using(FileStream file = new FileStream("Phones.xml", FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Phone>));
                    Phones = (List<Phone>)xmlFormat.Deserialize(file);
                }

                lstPhone.DataSource = Phones;
                pct.ImageLocation = Path.Combine("pic\\Samsung.jpeg");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public class Phone
        {
            public string Model{get;set;}
            public string OS { get; set; }
            public double Processor { get; set; }
            public string Pic {get;set;}
            public int Price { get; set; }
            public List<PhoneOption> PhoneOptions;
            public override string ToString()
            {
                return String.Format("{0}. {1} - {2}. Цена {3}", Model, OS, Processor, Price);
            }
            public Phone (string model,string os, double processor,string pic,int price,List<PhoneOption> po)
            {
                Model=model;
                OS=os;
                Processor=processor;
                Pic=pic;
                Price=price;
                PhoneOptions=po;
            }
            public Phone()
            {
            }
        }
        private void lstPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pathToFile = "pic\\";
            if (lstPhone.SelectedItem != null)
            {
                Phone selPhone = (Phone)lstPhone.SelectedItem;
                tbModel.Text = selPhone.Model;
                tbOS.Text = selPhone.OS;
                tbProcessor.Text = selPhone.Processor.ToString();
                try
                {
                    pct.Image = Image.FromFile(pathToFile + selPhone.Pic);
                    //pct.ImageLocation = Path.Combine(pathToFile, selPhone.Pic);
                }
                catch(Exception)
                {
                    pct.Image = Image.FromFile(pathToFile + "no_image.png");
                }
                tbPrice.Text = selPhone.Price.ToString();
                lstOptions.DataSource = selPhone.PhoneOptions;
                tbEditModel.Text = selPhone.Model;
                tbEditOS.Text = selPhone.OS;
                tbEditPic.Text = selPhone.Pic;
                tbEditProcessor.Text = selPhone.Processor.ToString();
                tbEditPrice.Text = selPhone.Price.ToString();
                for(int i = 0; i < chlstOptions.Items.Count; i++)
                {
                    chlstOptions.SetItemChecked(i, false);
                }
                for(int i = 0; i < selPhone.PhoneOptions.Count; i++)
                {
                    for(int j = 0; j < chlstOptions.Items.Count; j++)
                    {
                        if(chlstOptions.Items[j].ToString()==selPhone.PhoneOptions[i].Options)
                        {
                            chlstOptions.SetItemChecked(j, true);
                        }
                    }
                }
            }
        }
        public class PhoneOption
        {
            public string Options { get; set; }
            public override string ToString()
            {
                return String.Format("{0}", Options);
            }
            public PhoneOption(string optrion)
            {
                Options = optrion;
            }
            public PhoneOption()
            {
            }
        }
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if(lstPhone.SelectedItem != null)
            {
                List<Phone> phones;
                using(FileStream file=new FileStream("Phones.xml",FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Phone>));
                    phones = (List<Phone>)xml.Deserialize(file);
                }
                List<PhoneOption> po = new List<PhoneOption>();
                for(int i = 0; i < chlstOptions.Items.Count; i++)
                {
                    if(chlstOptions.GetItemChecked(i))
                    {
                        po.Add(new PhoneOption(chlstOptions.Items[i].ToString()));
                    }
                }
                phones[lstPhone.SelectedIndex].Model = tbEditModel.Text;
                phones[lstPhone.SelectedIndex].OS = tbEditOS.Text;
                phones[lstPhone.SelectedIndex].Pic = tbEditPic.Text;
                phones[lstPhone.SelectedIndex].PhoneOptions = po;
                phones[lstPhone.SelectedIndex].Price = Convert.ToInt32(tbEditPrice.Text);
                phones[lstPhone.SelectedIndex].Processor = Convert.ToDouble(tbEditProcessor.Text);
                using(FileStream file=new FileStream("Phones.xml",FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Phone>));
                    xml.Serialize(file, phones);
                }
                lstPhone.DataSource = phones;
            }
        }
        private void btnAddOption_Click(object sender, EventArgs e)
        {
            if(tbNameNewOption.Text != "")
            {
                List<PhoneOption> phoneOp = new List<PhoneOption>();
                foreach(PhoneOption po in chlstOptions.Items)
                {
                    if(po.Options==tbNameNewOption.Text)
                    {
                        MessageBox.Show("Такая опция уже существует", "Повтор", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    phoneOp.Add(new PhoneOption(po.Options));
                }
                phoneOp.Add(new PhoneOption(tbNameNewOption.Text));

                try
                {
                    using(FileStream file = new FileStream("ListPhoneOptions.xml", FileMode.Open))
                    {
                        XmlSerializer xmlFormat=new XmlSerializer(typeof(List<PhoneOption>));
                        xmlFormat.Serialize(file,phoneOp);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                chlstOptions.DataSource = phoneOp;
            }
            else
            {
                MessageBox.Show("Введите название опции", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteOption_Click(object sender, EventArgs e)
        {
            List<PhoneOption> phoneOp=new List<PhoneOption>();
            foreach (PhoneOption po in chlstOptions.Items)
	        {
                phoneOp.Add(po);
	        }
            phoneOp.RemoveAt(chlstOptions.SelectedIndex);

            try
            {
                using(FileStream file = new FileStream("ListPhoneOptions.xml", FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<PhoneOption>));
                    xmlFormat.Serialize(file, phoneOp);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            chlstOptions.DataSource = phoneOp;
        }
        private void btnDeletePhone_Click(object sender, EventArgs e)
        {
            List<Phone> phones = new List<Phone>();

            foreach (Phone p in lstPhone.Items)
            {
                phones.Add(new Phone(p.Model,p.OS,p.Processor,p.Pic,p.Price,p.PhoneOptions));
            }
            phones.RemoveAt(lstPhone.SelectedIndex);

            lstPhone.DataSource = phones;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            lstPhone.DataSource = null;
            lstPhone.Items.Clear();
            string pathToFile = "pic\\";
            pct.Image = Image.FromFile(pathToFile + "no_image.png");
            tbModel.Text = null;
            tbOS.Text = null;
            tbPrice.Text = null;
            tbProcessor.Text = null;
            lstOptions.DataSource = null;
            lstOptions.Items.Clear();
            tbEditModel.Text = null;
            tbEditOS.Text = null;
            tbEditPic.Text = null;
            tbEditPrice.Text = null;
            tbEditProcessor.Text = null;
            chlstOptions.DataSource = null;
            chlstOptions.Items.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Phone> phones = new List<Phone>();
            foreach(Phone p in lstPhone.Items)
            {
                phones.Add(new Phone(p.Model, p.OS, p.Processor, p.Pic, p.Price, p.PhoneOptions));
            }
            using(FileStream file = new FileStream("Phones.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Phone>));
                xml.Serialize(file, phones);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            List<Phone> phones = new List<Phone>();
            List<PhoneOption> allPhoneOptions = new List<PhoneOption>();
            try
            {
                using(FileStream file = new FileStream("Phones.xml", FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Phone>));
                    phones = (List<Phone>)xml.Deserialize(file);
                }
                
                using(FileStream file = new FileStream("ListPhoneOptions.xml", FileMode.Open))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<PhoneOption>));
                    allPhoneOptions = (List<PhoneOption>)xmlFormat.Deserialize(file);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lstPhone.DataSource = phones;
            chlstOptions.DataSource = allPhoneOptions;
            lstOptions.DataSource = phones[lstPhone.SelectedIndex].PhoneOptions;
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            tbEditModel.Text = null;
            tbEditOS.Text = null;
            tbEditPic.Text = null;
            tbEditPrice.Text = null;
            tbEditProcessor.Text = null;
            for(int i = 0; i < chlstOptions.Items.Count; i++)
            {
                chlstOptions.SetItemChecked(i, false);
            }
        }

        private void btnAddNewPhone_Click(object sender, EventArgs e)
        {
            List<Phone> phones = new List<Phone>();
            using(FileStream file=new FileStream("Phones.xml",FileMode.Open))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Phone>));
                phones = (List<Phone>)xml.Deserialize(file);
            }
            List<PhoneOption>po=new List<PhoneOption>();
            for (int i = 0; i < chlstOptions.Items.Count; i++)
			{
                if (chlstOptions.GetItemChecked(i))
	            {
                    po.Add(new PhoneOption(chlstOptions.Items[i].ToString()));
	            }
			}

            phones.Add(new Phone(tbEditModel.Text, tbEditOS.Text, Convert.ToDouble(tbEditProcessor.Text), tbEditPic.Text, Convert.ToInt32(tbEditPrice.Text), po));
            lstPhone.DataSource = phones;
        }
    }
}
