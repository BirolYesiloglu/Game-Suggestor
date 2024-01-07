using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class UserRate : Form
    {
        private string connectionString = "Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;";
        private int gameID;
        private int userID;

        public UserRate(int gameID, int userID)
        {
            InitializeComponent();
            this.gameID = gameID;
            this.userID = userID;
            LoadGameTitles();
        }

        private void LoadGameTitles()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Title FROM Games";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader.GetString("Title"));
                        }
                    }
                }

                connection.Close();
            }
        }

        private bool SaveUserRating(int userID, int gameID, int rating)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Rating FROM UserGamePreferences WHERE UserID = @UserID AND GameID = @GameID";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@GameID", gameID);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        int existingRating = Convert.ToInt32(result);

                        if (existingRating == rating)
                        {
                            MessageBox.Show("You cannot give the same rating for the same game.", "Error");
                            return false;
                        }
                        else
                        {
                            query = "UPDATE UserGamePreferences SET Rating = @Rating WHERE UserID = @UserID AND GameID = @GameID";
                        }
                    }
                    else
                    {
                        query = "INSERT INTO UserGamePreferences (UserID, GameID, Rating) VALUES (@UserID, @GameID, @Rating)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@GameID", gameID);
                        cmd.Parameters.AddWithValue("@Rating", rating);

                        cmd.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGameTitle = comboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedGameTitle))
            {
                int selectedGameID = GetGameIDByTitle(selectedGameTitle);

                // Store the selected game ID in a class variable for later use
                this.gameID = selectedGameID;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int rating) && rating >= 1 && rating <= 5)
            {
                bool ratingChanged = SaveUserRating(userID, gameID, rating);

                if (ratingChanged)
                {
                    // Close the form with OK result to indicate a successful rating change
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number between 1 and 5.", "Error");
            }
        }


        private int GetGameIDByTitle(string gameTitle)
        {
            int gameID = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT GameID FROM Games WHERE Title = @Title";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", gameTitle);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        gameID = Convert.ToInt32(result);
                    }
                }

                connection.Close();
            }

            return gameID;
        }
    }
}
