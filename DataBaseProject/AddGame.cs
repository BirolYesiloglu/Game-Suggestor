using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DataBaseProject
{
    public partial class AddGame : Form
    {
        private MySqlConnection connection;
        private List<string> selectedGenres = new List<string>();
        private GameSuggestor gameSuggestorForm;
        private Dictionary<CheckBox, int> checkBoxGenreMap = new Dictionary<CheckBox, int>();

        public AddGame(GameSuggestor gameSuggestorForm)
        {
            InitializeComponent();
            connection = new MySqlConnection("Server=localhost;Uid=root;Pwd=12345678;Database=GameSuggestor;");
            button1.Click += button1_Click;

            // Store the instance of GameSuggestor
            this.gameSuggestorForm = gameSuggestorForm;

            // Initialize the dictionary with CheckBox controls and corresponding genre IDs
            checkBoxGenreMap.Add(checkBox1, 2);
            checkBoxGenreMap.Add(checkBox2, 3);
            checkBoxGenreMap.Add(checkBox3, 4);
            checkBoxGenreMap.Add(checkBox4, 5);
            checkBoxGenreMap.Add(checkBox5, 6);
            checkBoxGenreMap.Add(checkBox6, 7);
            checkBoxGenreMap.Add(checkBox7, 8);
            checkBoxGenreMap.Add(checkBox8, 9);
            checkBoxGenreMap.Add(checkBox9, 10);
            checkBoxGenreMap.Add(checkBox10, 11);
            checkBoxGenreMap.Add(checkBox11, 12);
            checkBoxGenreMap.Add(checkBox12, 13);
            checkBoxGenreMap.Add(checkBox13, 14);
            checkBoxGenreMap.Add(checkBox14, 15);
            checkBoxGenreMap.Add(checkBox15, 16);
            checkBoxGenreMap.Add(checkBox16, 17);
            checkBoxGenreMap.Add(checkBox17, 18);
            checkBoxGenreMap.Add(checkBox18, 19);
            checkBoxGenreMap.Add(checkBox19, 20);
            checkBoxGenreMap.Add(checkBox20, 21);
            checkBoxGenreMap.Add(checkBox21, 22);
            checkBoxGenreMap.Add(checkBox22, 23);
            checkBoxGenreMap.Add(checkBox23, 24);
            checkBoxGenreMap.Add(checkBox24, 25);
            // Add mappings for other CheckBox controls accordingly

            // Attach the same event handler to all checkboxes using a loop
            foreach (var checkBox in checkBoxGenreMap.Keys)
            {
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleCheckBoxCheckedChanged(sender);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string gameName = textBox1.Text;

            if (!string.IsNullOrEmpty(gameName))
            {
                int newGameId = AddGameToDatabase(gameName, selectedGenres);

                // Close the form
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid game name.", "Error");
            }
        }

        private int AddGameToDatabase(string gameName, List<string> genres)
        {
            int newGameId = 0;

            try
            {
                connection.Open();

                // Insert the new game into the Games table and get the ID of the newly inserted game
                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO Games (Title) VALUES (@Title); SELECT LAST_INSERT_ID();", connection);
                insertCommand.Parameters.AddWithValue("@Title", gameName);
                newGameId = Convert.ToInt32(insertCommand.ExecuteScalar());

                // For each genre, insert a record into the GameGenres table
                foreach (string genreId in genres)
                {
                    MySqlCommand insertGenreCommand = new MySqlCommand("INSERT INTO GameGenres (GameID, GenreID) VALUES (@GameID, @GenreID)", connection);
                    insertGenreCommand.Parameters.AddWithValue("@GameID", newGameId);
                    insertGenreCommand.Parameters.AddWithValue("@GenreID", genreId);
                    insertGenreCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding game: {ex.Message}", "Error");
            }
            finally
            {
                connection.Close();
            }

            // Display success message with gameID
            MessageBox.Show($"Game added successfully with ID: {newGameId}", "Success");

            return newGameId;
        }

        private void HandleCheckBoxCheckedChanged(object sender)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                int genreId = checkBoxGenreMap[checkBox];

                if (checkBox.Checked)
                {
                    // The checkbox was checked, add the genre to the list
                    selectedGenres.Add(genreId.ToString());
                }
                else
                {
                    // The checkbox was unchecked, remove the genre from the list
                    selectedGenres.Remove(genreId.ToString());
                }
            }
        }

    }
}
