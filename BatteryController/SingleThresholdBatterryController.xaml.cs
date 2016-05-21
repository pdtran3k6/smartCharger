using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Net;
using System.IO;



namespace BatteryController
{
    /// <summary>
    /// Interaction logic for SingleThresholdBatterryController.xaml
    /// </summary>
    public partial class SingleThresholdBatterryController : Window
    {
        
        public SingleThresholdBatterryController()
        {
            InitializeComponent();
        }

        private int LowSelectedPercentage;
        private int HighSelectedPercentage;
        private int BatteryPercent;
        //private int count;
        //private int topcount;

        private string State = "Down";
        private bool GoingUp = false;


        //Timer to check the current battery level
        Timer timer = new Timer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Add from 1 to 99 to the dropdown list
            for (int i = 0; i < 100; i++)
            {
                cb_LowBatteryPercentageInput.Items.Add(i);
            }

            for (int i = 0; i < 100; i++)
            {
                cb_HighBatteryPercentageInput.Items.Add(i);
            }

            
        }

        private void btn_ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            PowerStatus p = SystemInformation.PowerStatus;
            BatteryPercent = (int)(p.BatteryLifePercent * 100);
            LowSelectedPercentage = (int)(cb_LowBatteryPercentageInput.SelectedItem);
            HighSelectedPercentage = (int)(cb_HighBatteryPercentageInput.SelectedItem);
            

            if (LowSelectedPercentage > HighSelectedPercentage)
            {
                System.Windows.MessageBox.Show("The inputs are invalid. Please retype the inputs.");
            }
            else
            {
                System.Windows.MessageBox.Show("The system will start charging when the battery drops to " 
                    + LowSelectedPercentage.ToString() + " % and stop charging when the battery reaches " + HighSelectedPercentage.ToString() + " %!");
            }

            //count = 1;
            //topcount = 0;
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();

            //System.Net.WebRequest request = System.Net.WebRequest.Create("http://192.168.1.16/turnOff/");
            System.Windows.MessageBox.Show("OFF!");
            /// missing line here
            State = "Down";
            GoingUp = false;
            //request.GetResponse();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            BatteryPercent = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            //if (BatteryPercent <= LowSelectedPercentage && State == "Down")
            if (BatteryPercent <= LowSelectedPercentage && !GoingUp)
                {
                //System.Net.WebRequest request = System.Net.WebRequest.Create("http://192.168.1.16/turnOn/");
                System.Windows.MessageBox.Show("ON!");
                //request.GetResponse();
                //State = "Up";
                GoingUp = true;
                // make this one below so that user respond slower than 5 seconds
                System.Windows.MessageBox.Show("ON!");
            }

            if (BatteryPercent >= HighSelectedPercentage && GoingUp)
            {
                //System.Net.WebRequest request = System.Net.WebRequest.Create("http://192.168.1.16/turnOff/");
                
                //request.GetResponse();
                //State = "Down";
                GoingUp = false;
                // make this one below so that user respond slower than 5 seconds                 
                System.Windows.MessageBox.Show("OFF!");
            }
            //If the current BatteryPercentage has reached 100%. If the battery has reached 100%, turn off
            //if (BatteryPercent == HighSelectedPercentage || BatteryPercent == 100 && topcount == 0)
            //{
            //    System.Net.WebRequest request = System.Net.WebRequest.Create("http://192.168.1.16/turnOff/");
            //    request.GetResponse();
            //    topcount++;
            //}

            
            //Check to see if the batteryPercentage is lower than SelectedPercentage. If it is lower, turn on
            //if (BatteryPercent < HighSelectedPercentage || BatteryPercent <= LowSelectedPercentage && count == 1)
            //{
            //    System.Net.WebRequest request = System.Net.WebRequest.Create("http://192.168.1.16/turnOn/");
            //    request.GetResponse();
            //    count--;
            //}

            //Reset mechanism so that after the system has been turned on, it is possible to turn off again and vice versa
            //if (count == 0)
            //{
            //    topcount = 0;
            //}

            //if (topcount == 1)
            //{
            //    count = 1;
            //}
        }
    }
}
