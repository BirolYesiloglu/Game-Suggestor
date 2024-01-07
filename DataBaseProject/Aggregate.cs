using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace DataBaseProject
{
    public partial class Aggregate : Form
    {
        private string connectionString = "Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;";

        public Aggregate()
        {
            InitializeComponent();
            LoadGameTitles();
        }

        private void LoadGameTitles()
        {
            // Clear existing items in comboBox1
            comboBox1.Items.Clear();

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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGameName = comboBox1.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedGameName))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT GameID FROM Games WHERE Title = @Title";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", selectedGameName);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int selectedGameID = Convert.ToInt32(reader["GameID"]);
                                MessageBox.Show($"Selected Game: {selectedGameName}, ID: {selectedGameID}");
                            }
                        }
                    }

                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Games";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    int gameCount = Convert.ToInt32(command.ExecuteScalar());
                    MessageBox.Show($"Total number of games: {gameCount}");
                }

                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedGameName = comboBox1.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedGameName))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string gameIDQuery = "SELECT GameID FROM Games WHERE Title = @Title";

                    using (MySqlCommand commandGameID = new MySqlCommand(gameIDQuery, connection))
                    {
                        commandGameID.Parameters.AddWithValue("@Title", selectedGameName);

                        int selectedGameID = 0;

                        using (MySqlDataReader readerGameID = commandGameID.ExecuteReader())
                        {
                            if (readerGameID.Read())
                            {
                                selectedGameID = Convert.ToInt32(readerGameID["GameID"]);
                            }
                            else
                            {
                                MessageBox.Show("Invalid game selection.");
                                return;
                            }
                        }

                        string query = "SELECT AVG(Rating) FROM UserGamePreferences WHERE GameID = @GameID";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@GameID", selectedGameID);

                            object result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                double averageRating = Convert.ToDouble(result);
                                MessageBox.Show($"Average rating of {selectedGameName} (ID {selectedGameID}): {averageRating}");
                            }
                            else
                            {
                                MessageBox.Show($"No ratings found for {selectedGameName} (ID {selectedGameID})");
                            }
                        }
                    }

                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a game.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedGameName = comboBox1.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedGameName))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string gameIDQuery = "SELECT GameID FROM Games WHERE Title = @Title";

                    using (MySqlCommand commandGameID = new MySqlCommand(gameIDQuery, connection))
                    {
                        commandGameID.Parameters.AddWithValue("@Title", selectedGameName);

                        int selectedGameID = 0;

                        using (MySqlDataReader readerGameID = commandGameID.ExecuteReader())
                        {
                            if (readerGameID.Read())
                            {
                                selectedGameID = Convert.ToInt32(readerGameID["GameID"]);
                            }
                            else
                            {
                                MessageBox.Show("Invalid game selection.");
                                return;
                            }
                        }

                        string query = "SELECT MAX(Rating) FROM UserGamePreferences WHERE GameID = @GameID";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@GameID", selectedGameID);

                            object result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                double maxRating = Convert.ToDouble(result);
                                MessageBox.Show($"Maximum rating of {selectedGameName} (ID {selectedGameID}): {maxRating}");
                            }
                            else
                            {
                                MessageBox.Show($"No ratings found for {selectedGameName} (ID {selectedGameID})");
                            }
                        }
                    }

                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a game.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedGameName = comboBox1.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedGameName))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string gameIDQuery = "SELECT GameID FROM Games WHERE Title = @Title";

                    using (MySqlCommand commandGameID = new MySqlCommand(gameIDQuery, connection))
                    {
                        commandGameID.Parameters.AddWithValue("@Title", selectedGameName);

                        int selectedGameID = 0;

                        using (MySqlDataReader readerGameID = commandGameID.ExecuteReader())
                        {
                            if (readerGameID.Read())
                            {
                                selectedGameID = Convert.ToInt32(readerGameID["GameID"]);
                            }
                            else
                            {
                                MessageBox.Show("Invalid game selection.");
                                return;
                            }
                        }

                        string query = "SELECT MIN(Rating) FROM UserGamePreferences WHERE GameID = @GameID";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@GameID", selectedGameID);

                            object result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                double minRating = Convert.ToDouble(result);
                                MessageBox.Show($"Minimum rating of {selectedGameName} (ID {selectedGameID}): {minRating}");
                            }
                            else
                            {
                                MessageBox.Show($"No ratings found for {selectedGameName} (ID {selectedGameID})");
                            }
                        }
                    }

                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select a game.");
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT GenreID, COUNT(*) FROM GameGenres GROUP BY GenreID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int genreID = Convert.ToInt32(reader["GenreID"]);
                            int gameCount = Convert.ToInt32(reader["COUNT(*)"]);
                            MessageBox.Show($"Number of games in Genre ID {genreID}: {gameCount}");
                        }
                    }
                }

                connection.Close();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT GenreID, COUNT(*) AS GameCount FROM GameGenres GROUP BY GenreID ORDER BY GameCount DESC LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int mostPopularGenreID = Convert.ToInt32(reader["GenreID"]);
                            int gameCount = Convert.ToInt32(reader["GameCount"]);
                            MessageBox.Show($"The most popular genre is ID {mostPopularGenreID} with {gameCount} games.");
                        }
                    }
                }

                connection.Close();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT GameID, AVG(Rating) AS AverageRating FROM UserGamePreferences GROUP BY GameID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int gameID = Convert.ToInt32(reader["GameID"]);
                            double averageRating = Convert.ToDouble(reader["AverageRating"]);
                            MessageBox.Show($"Average rating for game with ID {gameID}: {averageRating}");
                        }
                    }
                }

                connection.Close();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT GameID, COUNT(DISTINCT UserID) AS UserCount FROM UserGamePreferences GROUP BY GameID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int gameID = Convert.ToInt32(reader["GameID"]);
                            int userCount = Convert.ToInt32(reader["UserCount"]);
                            MessageBox.Show($"Number of users who rated game with ID {gameID}: {userCount}");
                        }
                    }
                }

                connection.Close();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT GameID, AVG(Rating) AS AverageRating FROM UserGamePreferences GROUP BY GameID ORDER BY AverageRating DESC LIMIT 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int highestRatedGameID = Convert.ToInt32(reader["GameID"]);
                            double averageRating = Convert.ToDouble(reader["AverageRating"]);
                            MessageBox.Show($"The game with the highest average rating is ID {highestRatedGameID} with an average rating of {averageRating}");
                        }
                    }
                }

                connection.Close();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;"))
            {
                connection.Open();

                string query = "SELECT UserID, COUNT(*) AS ActivityCount FROM UserLogs GROUP BY UserID";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userID = Convert.ToInt32(reader["UserID"]);
                            int activityCount = Convert.ToInt32(reader["ActivityCount"]);
                            MessageBox.Show($"User with ID {userID} has logged {activityCount} activities");
                        }
                    }
                }

                connection.Close();
            }
        }

    }
}
