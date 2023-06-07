using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hackaton
    
{

    public partial class Form1 : Form {
   
        firstUC first;
        secondUC second;

        public Form1()
        {
            InitializeComponent();
          // // first = new firstUC();
            second = new secondUC();


            // Add firstUC to the form
            //first.Location = new Point(0, 0);
            this.firstuc1.Visible = false;
            //Controls.Add(first);

            // Add secondUC to the form
            //second.Location = new Point(0, 0);
            this.seconduc1.Visible = false;
          // Controls.Add(second);

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.seconduc1.Visible = false;
            this.firstuc1.Visible = true;
            button1.Visible = false;
        }



        private void firstuc1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void seconduc2_Load(object sender, EventArgs e)
        {

        }
    }
    
}