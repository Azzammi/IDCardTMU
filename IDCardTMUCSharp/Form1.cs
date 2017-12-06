using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCardTMUCSharp.Model;

namespace IDCardTMUCSharp
{
    public partial class Form1 : Form
    {
        #region Declaration
        private cardModel model;

        private bool isPrinted;
        private int orient;
        private int pwdt;
        private int phgt;
        #endregion
        public Form1()
        {
            InitializeComponent();

            model = new cardModel
            {
                name = "Luthfi",
                nik = 09909013,
                companyName = "PT ESSEI PERBAMA"
            };

            cardModelBindingSource.Add(model);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void potraitBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void browseNmColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    model.nameForeColor = colorDialog.Color;
                }
            }

            cardModelBindingSource.ResetCurrentItem();
        }

        private void browseNikColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    model.nikForeColor = colorDialog.Color;
                }
            }

            cardModelBindingSource.ResetCurrentItem();
        }

        private void browseCompanyColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    model.companyNameForeColor = colorDialog.Color;
                }
            }

            cardModelBindingSource.ResetCurrentItem();
        }

        private void browseRulesColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    model.listRulesForeColor = colorDialog.Color;
                }
            }

            cardModelBindingSource.ResetCurrentItem();
        }
    }
}
