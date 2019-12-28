using CustomerCRUD.Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            // 'CENTC' 1번만 주문
            OrderRepository order = new OrderRepository();
            orderBindingSource.DataSource = order.find("CENTC");
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            OrderRepository order = new OrderRepository();
            orderBindingSource.DataSource = order.GetAll();
            // orderDetailBindingSource.DataSource;
        }



    }
}
