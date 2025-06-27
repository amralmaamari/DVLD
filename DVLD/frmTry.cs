using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DVLD
{

   
    public partial class frmTry : Form
    {
        public frmTry()
        {
            InitializeComponent();
        }
        public class clsCarsSubscriber
        {
            public void Subscribe(clsNewCarReleases NewCarReleases)
            {
                NewCarReleases.NewCarReleasesed += HandleNewNews;
            }


            public void UnSubscribe(clsNewCarReleases NewCarReleases)
            {
                NewCarReleases.NewCarReleasesed -= HandleNewNews;
            }

            public void HandleNewNews(object sender, clsCar car)
            {
                MessageBox.Show($"Model: {car.Model}");


            }
        }

        clsNewCarReleases newCarReleases = new clsNewCarReleases();
        clsCarsSubscriber subscriber1 = new clsCarsSubscriber();
        clsCar car=new clsCar();

        

        private void button1_Click(object sender, EventArgs e)
        {
            subscriber1.Subscribe(newCarReleases);
        }


        static Func<int, int> Red= CarID;
        private void button3_Click(object sender, EventArgs e)
        {
            car.Save();
            newCarReleases.CreateCar(car);
            
          

        }

         static int CarID(int CarID)
        {
            return CarID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subscriber1.UnSubscribe(newCarReleases);

            int number = Red(200);
            MessageBox.Show(number.ToString());
        }

        
    }
}
