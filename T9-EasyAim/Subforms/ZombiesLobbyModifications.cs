﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T9_EasyAim.Subforms
{
    public partial class ZombiesLobbyModifications : Form
    {
        Memory.ImportProtection protection;
        public ZombiesLobbyModifications()
        {
            InitializeComponent();
            protection = new Memory.ImportProtection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (protection.IsMemorySafe()) 
            {

            }
        }
    }
}
