using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UnitTestProject1
{
    public class ConnectDB
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;


        public static void Main(string[] args)
        {

            Initialize();

            // function calls to queries go here   
        }

        //Initialize values
        private static void Initialize()
        {
            server = "sql181.main-hosting.eu";
            database = "u628890082_theo";
            uid = "u628890082_theo";
            password = "6sEHJuOgsfTr";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Allow User Variables=True;";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.Write("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public static void createAccount(string username, string email, string password)
        {
            string query = "INSERT INTO player VALUES('" + username + "','" + email + "','" + password + "',0);";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        public static void resetPassword(string username, string email, string newPassword)
        {
            string query = "UPDATE player SET password='" + newPassword + "' WHERE user='" + username + "';";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        public static void login(string username, string password)
        {
            string query = "SELECT username FROM player WHERE username='" + username + "' AND password='" + password + "';";
            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    Console.WriteLine(myReader[0]);
                }
                // always call Close when done reading.
                myReader.Close();
            }
            CloseConnection();
        }

        public static void viewLeaderboards(int level)
        {
            string query = "SELECT user, ranking, score FROM leaderboard WHERE level=" + level + " ORDER BY score DESC;";
            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    Console.WriteLine(myReader[1] + ", "  + myReader[0] + ", " + myReader[2]);
                }
                // always call Close when done reading.
                myReader.Close();
            }
            CloseConnection();
        }

        public static void continueGame(string user)
        {
            string query = "SELECT last_level_completed FROM player WHERE username='" + user + "';";
            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    Console.WriteLine(myReader[0] + ", " + myReader[1] + ", " + myReader[2] + ", " + myReader[3]);
                }
                // always call Close when done reading.
                myReader.Close();
            }
            CloseConnection();
        }

        public static void completeLevelForFirstTime(int level, string user, int score)
        {
            string query = "SET @rank := (SELECT COUNT(*)+1 FROM leaderboard WHERE score > " + score + " AND level = " + level + "); INSERT INTO leaderboard VALUES(" + level + ", @rank, '" + user + "', " + score + "); UPDATE leaderboard SET ranking = CASE WHEN score < " + score + " THEN ranking+1 ELSE ranking END WHERE level = " + level + ";";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        public static void updateHighScore(string user, int level, int score)
        {
            string query = "SET @rank := (SELECT COUNT(*)+1 FROM leaderboard WHERE score > " + score + " AND level = " + level + "); UPDATE leaderboard SET score=" + score + ", ranking=@rank WHERE user='" + user + "' AND level=" + level + "; UPDATE leaderboard SET ranking = CASE WHEN score < " + score + " THEN ranking+1 ELSE ranking END WHERE level = " + level + ";";
            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        public static void saveGame(string user, int level)
        {
            string query = "UPDATE player SET last_level_completed=" + level + " WHERE username='" + user + "';";
            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }
    }
}
