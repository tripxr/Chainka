using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using NUnit;

namespace ProbaProgi1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public void button1_Click(object sender, EventArgs e)
        {

            int productId;
            int quantity;
            DateTime requestDate;
            string supplier;
            supplier = textBoxSupplier.Text;
            if (!int.TryParse(textBoxProductId.Text, out productId) ||
                !int.TryParse(textBoxQuantity.Text, out quantity) ||
                !DateTime.TryParse(dateTimePickerRequestDate.Text, out requestDate) ||
                string.IsNullOrWhiteSpace(textBoxSupplier.Text))
            {
                MessageBox.Show("Пожалуйста, введите действительные данные во все поля.", "Неверный Ввод", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=chaidbb;Integrated Security=True"))
            {
                connection.Open();

                string query = @"
                    INSERT INTO SupplyRequest (ProductID, Quantity, RequestDate, Supplier)
                    VALUES (@ProductID, @Quantity, @RequestDate, @Supplier)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@RequestDate", requestDate);
                    command.Parameters.AddWithValue("@Supplier", supplier);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Успешно добавлено в SupplyRequest.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        public void Form1_Load(object sender, EventArgs e)
        {

        }

        public void label5_Click(object sender, EventArgs e)
        {

        }

        public void textBoxProductId_TextChanged(object sender, EventArgs e)
        {

        }
        public string GetProductId()
        {
            return textBoxProductId.Text;
        }
    }
    }

