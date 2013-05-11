using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace YodleTriangle {
    /// <summary>
    /// Calculates the sum of the adjacent nodes in subsequent rows starting at the root node
    /// </summary>
    public partial class YodleTriangle : Form {
        public YodleTriangle() {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e) {
            try {
                //get the file name from the textbox in case the user changed a path
                string fileLocation = tboxFileLocation.Text.Trim();
                //check that the file exists
                if(File.Exists(fileLocation)) {
                    int runningSum = CalculateSum(fileLocation);
                    MessageBox.Show(String.Format("Sum: {0}", runningSum), "Running Sum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("File does not exist");
                }
            } catch(Exception exc) {
                MessageBox.Show(exc.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Calculates the sum of the adjacent nodes in subsequent rows of the tree starting from the root node
        /// </summary>
        /// <param name="fileLocation"></param>
        private int CalculateSum(String fileLocation) {
            // if an exception occurs, let it bubble up. Since we're checking for file existence in the calling method,
            // going to skip checking for it here

            //read the file line by line
            int counter = 0;
            string currentLine;
            int runningSum = 0;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(fileLocation);
            while((currentLine = file.ReadLine()) != null) {
                String[] splitString = currentLine.Trim().Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                int middle = 0;
                int numerator = (splitString.Length - 1); // since using integer, it will round up if it's inbetween (e.g. 3.5)
                
                if(numerator > 1) { // check for greater than one so that we get the correct location for rows 0 and 1 (root and next row)
                    middle = numerator / 2;
                } else if(numerator == 1) { // for the case where there are 2 elements, we want the one in position 0
                    middle = 0;
                } else { // for the case where there is only one element, the root
                    middle = 0;
                }

                runningSum += int.Parse(splitString[middle]);
                counter++;
            }

            file.Close();

            return runningSum;
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                tboxFileLocation.Text = openFileDialog1.FileName;
            }
        }
    }
}
