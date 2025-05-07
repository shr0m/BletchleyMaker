using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker.Forms
{
    public partial class Automation : Form
    {
        List<string> Rules = new List<string>();
        public Automation()
        {
            InitializeComponent();
        }

        // Cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Submit button
        private void button1_Click(object sender, EventArgs e)
        {
            string[] rules = textBox1.Text.Replace(" ", "").Split(",");

            if (!ValidateRule(rules))
            {
                MessageBox.Show("Invalid rule format. Please use the format: 'rule1,rule2,...'");
                return;
            }
            else
            {
                Rules = rules.ToList();
            }
        }

        private bool ValidateRule(string[] rules)
        {
            string[] availableRules = { "X", "U1", "D1", "L1", "R1", "U2", "U3", "U4", "U5", "L2", "L3", "L4", "L5", "D2", "D3", "D4", "D5", "R2", "R3", "R4", "R5" };

            foreach (string rule in rules)
            {
                if (!availableRules.Contains(rule))
                {
                    return false;
                }
            }
            return true;
        }

        public List<string> GetRules()
        {
            return Rules;
        }
    }
}
