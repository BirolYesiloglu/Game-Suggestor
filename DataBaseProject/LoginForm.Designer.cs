using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;";

        public LoginForm()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            Login = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();

            // Set properties for each control
            textBox1.Location = new System.Drawing.Point(100, 50);
            textBox1.Size = new System.Drawing.Size(150, 20);

            textBox3.Location = new System.Drawing.Point(100, 80);
            textBox3.Size = new System.Drawing.Size(150, 20);
            textBox3.PasswordChar = '*';

            Login.Location = new System.Drawing.Point(100, 110);
            Login.Size = new System.Drawing.Size(75, 23);
            Login.Text = "Login";
            Login.Click += Login_Click;

            label1.Location = new System.Drawing.Point(20, 50);
            label1.Text = "Username:";

            label2.Location = new System.Drawing.Point(20, 80);
            label2.Text = "Password:";

            label4.Location = new System.Drawing.Point(20, 10);
            label4.Text = "Login Form";

            // Add controls to the form's Controls collection
            this.Controls.Add(textBox1);
            this.Controls.Add(textBox3);
            this.Controls.Add(Login);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(label4);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Retrieve username and password from the textboxes
            string username = textBox1.Text;
            string password = textBox3.Text;

            // Validate user credentials (replace with your authentication logic)
            if (ValidateUser(username, password))
            {
                // Successful login, open the main form
                GameSuggestor mainForm = new GameSuggestor();
                mainForm.Show();

                // Close the login form
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    connection.Close();

                    return count > 0;
                }
            }
        }

        private Label label4;
        private Button Login;
        private Label label2;
        private TextBox textBox3;
        private Label label1;
        private TextBox textBox1;
    }
}
