using EContact.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
            txtName.Focus();
        }

        static string myConnectionString = ConfigurationManager.ConnectionStrings["EcontactEntities"].ConnectionString;
        ContactClass model = new ContactClass();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear();

            //Application.Exit();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            model.Firstname = txtName.Text;
            model.Lastname = txtLastname.Text;
            model.Phone = txtNumber.Text;
            model.Address = txtAddress.Text;
            model.Gender = cboGender.Text;

            bool success = model.Insert(model);
            if(success==true)
            {
                MessageBox.Show("New contact successfully Inserted.", "Create Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to create new Contact", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            // Load data on Data GridView
            DataTable dataTable = model.Select();
            dataGridView.DataSource = dataTable;
        }

        private void Clear()
        {
            txtAddress.Clear();
            txtContactId.Clear();
            txtLastname.Clear();
            txtName.Clear();
            txtSearch.Clear();
            txtNumber.Clear();
            txtName.Focus();
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dataTable = model.Select();
            dataGridView.DataSource = dataTable;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            // Get the data from the textbox.
            model.ContactId = int.Parse(txtContactId.Text);
            model.Firstname = txtName.Text;
            model.Lastname = txtLastname.Text;
            model.Phone = txtNumber.Text;
            model.Address = txtAddress.Text;
            model.Gender = cboGender.Text;

            //update data in database
            bool isSuccess = model.Update(model);

            if (isSuccess==true)
            {
                MessageBox.Show("Contact has ben successfully updated.", "Contact Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataTable dataTable = model.Select();
                dataGridView.DataSource = dataTable;

                Clear();
            }
            else
            {
                MessageBox.Show("Failed to update Contact, please try again.", "Updated Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get Data from Grid View and Load it to the textbox
            //identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            txtContactId.Text = dataGridView.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastname.Text = dataGridView.Rows[rowIndex].Cells[2].Value.ToString();
            txtNumber.Text = dataGridView.Rows[rowIndex].Cells[3].Value.ToString();
            cboGender.Text = dataGridView.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dataGridView.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            // Get data from the grid view
            model.ContactId = Convert.ToInt32(txtContactId.Text);

            bool isSuccess = model.Delete(model);

            if (true)
            {
                MessageBox.Show("Contact successsfully delete.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DataTable dataTable = model.Select();
                dataGridView.DataSource = dataTable;
                Clear();
            }
            else
            {
                MessageBox.Show("Contact was not deleted", "Delete Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            SqlConnection connection = new SqlConnection(myConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Contact WHERE Firstname LIKE'%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'", connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;
        }
    }
}
