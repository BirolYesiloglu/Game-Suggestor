using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class LoginForm : Form
    {
        private void Login_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox3.Text;

            // Connect to the database and validate the credentials
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        // Login successful
                        MessageBox.Show("Login successful.", "Success");
                        
                        // Create and show the GameSuggestor form as a modal dialog
                        using (GameSuggestor form1 = new GameSuggestor())
                        {
                            form1.ShowDialog();
                            // Close the login form
                            this.Close();
                        }  
                    }
                    else
                    {
                        // Check if the username exists in the database
                        query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                        command.CommandText = query;
                        int usernameCount = Convert.ToInt32(command.ExecuteScalar());

                        if (usernameCount > 0)
                        {
                            // Username exists, but password is invalid
                            MessageBox.Show("Invalid password.", "Error");
                        }
                        else
                        {
                            // Username is invalid
                            MessageBox.Show("Invalid username.", "Error");
                        }
                    }
                }
                connection.Close();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(293, 185);
            this.Name = "LoginForm";
            this.ResumeLayout(false);

        }
    }
}
