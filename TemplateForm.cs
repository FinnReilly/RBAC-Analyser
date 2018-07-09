using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBACS
{
    public partial class TemplateForm : Form
    {
        public TemplateForm(string RecommendationString, string TitleString)
        {          
            InitializeComponent();
            this.richTextBox1.Text = RecommendationString;
            this.Text = $"Recommended Template: {TitleString}";
        }
    }
}
