using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CustomerMaintenance
{
    public partial class frmAddModifyCustomer : Form
    {
        public frmAddModifyCustomer()
        {
            InitializeComponent();
        }

        public bool addCustomer;
        public Customer customer;
        private IQueryable<State> comboBoxItems;

        private void frmAddModifyCustomer_Load(object sender, EventArgs e)
        {
            this.LoadComboBox();
            if (addCustomer)
            {
                this.Text = "Add Customer";
                cboStates.SelectedIndex = -1;
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomerData();
            }
        }

        private void LoadComboBox()
        {
            try
            {
                // Code a query to retrieve the required information from
                // the States table, and sort the results by state name.
                // Bind the State combo box to the query results.

                comboBoxItems =
                  from states in MMABooksEntity.mmaBooks.States
                  select states;

                foreach (var item in comboBoxItems)
                {
                    cboStates.Items.Add(item.StateName);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void DisplayCustomerData()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtCity.Text = customer.City;
            cboStates.Text = customer.State.StateName;
            txtZipCode.Text = customer.ZipCode;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addCustomer)
                {
                    customer = new Customer();
                    this.PutCustomerData(customer);
                    
                    // Add the new vendor to the collection of vendors.

                    try
                    {
                        this.PutCustomerData(customer);
                        customer = MMABooksEntity.mmaBooks.Customers.Add(customer);
                        MMABooksEntity.mmaBooks.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        ex.Entries.Single().Reload();
                        if (MMABooksEntity.mmaBooks.Entry(customer).State == EntityState.Detached)
                        {
                            MessageBox.Show("Another user has deleted " + "that customer.", "Concurrency Error");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Another user has updated " + "that customer.", "Concurrency Error");
                            DisplayCustomerData();

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    this.PutCustomerData(customer);
                    try
                    {

                        // Update the database.
                        MMABooksEntity.mmaBooks.SaveChanges();
                        this.DialogResult = DialogResult.OK;
                    }
                    catch(DbUpdateConcurrencyException ex)
                    {
                        ex.Entries.Single().Reload();
                        if (MMABooksEntity.mmaBooks.Entry(customer).State == EntityState.Detached)
                        {
                            MessageBox.Show("Another user has deleted " + "that customer.", "Concurrency Error");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Another user has updated " + "that customer.", "Concurrency Error");
                            DisplayCustomerData();

                        }

                    }
                    // Add concurrency error handling.
                    // Place the catch block before the one for a generic exception.

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }

        private bool IsValidData()
        {
             return Validator.IsPresent(txtName) &&
                    Validator.IsPresent(txtAddress) &&
                    Validator.IsPresent(txtCity) &&
                    Validator.IsPresent(cboStates) &&
                    Validator.IsPresent(txtZipCode) &&
                    Validator.IsInt32(txtZipCode);
        }

        private void PutCustomerData(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            customer.City = txtCity.Text;

            string selectedStateName = cboStates.Items[cboStates.SelectedIndex].ToString();
            // get selected item according to State table we retreived in cboStates IQuerable variable
            State selectedItem = (from item in comboBoxItems
            where item.StateName == selectedStateName
            select item).Single();
            // update state code in the customer object
            customer.StateCode = selectedItem.StateCode;
            customer.State = selectedItem;
            customer.ZipCode = txtZipCode.Text;
        }
    }
}
