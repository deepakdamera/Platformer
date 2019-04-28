/* Template from Code Project
 * URL: https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Platformer
{
    public class ConnectDB
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        public ConnectDB()
        {

        }



        //Initialize values
        public void Initialize()
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

        public bool createAccount(string username, string password)
        {
            string query = "INSERT INTO player VALUES('" + username + "','" + password + "',0);";
            string query2 = "SELECT username FROM player WHERE username='" + username + "';"; // checks to see if the username already exists
            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                //Execute command
                MySqlDataReader myReader;
                myReader = cmd2.ExecuteReader();
                // Always call Read before accessing data.
                if (myReader.Read())
                {
                    if ((string)myReader[0] == username)
                    {
                        Console.WriteLine("Username already exists");
                        CloseConnection();
                        myReader.Close();
                        return false;
                    }
                }
                myReader.Close();
                if ((username.Length < 3 || username.Length > 40) && (password.Length < 8 || password.Length > 20))
                {
                    Console.WriteLine("Username must be 3-40 characters long. Password must be 8-20 characters long");

                }
                else if (username.Length < 3 || username.Length > 40)
                {
                    Console.WriteLine("Username must be 3-40 characters long.");
                }
                else if (password.Length < 8 || password.Length > 20)
                {
                    Console.WriteLine("Password must be 8-20 characters long");
                }
                else
                {
                    cmd.ExecuteNonQuery();
                    
                }
                //close connection
                CloseConnection();
                return true;
            }
            return true;
        }

        public bool login(string username, string password)
        {
            string query = "SELECT Username FROM player WHERE Username='" + username + "' AND Password='" + password + "';";
            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                if (myReader.Read())
                {
                    if((string)myReader[0] == username)
                    {
                        Console.WriteLine("Hello " + username);
                        CloseConnection();
                        return true;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Incorrect login information");
                    //myReader.Close();
                    CloseConnection();
                    return false;
                }
                // always call Close when done reading.
                myReader.Close();
            }
            CloseConnection();

            return false;
        }

        public List<string>[] viewLeaderboards(int level)
        {
            string query = "SELECT user, ranking, score FROM leaderboard WHERE level=" + level + " ORDER BY score DESC;";
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    list[0].Add(myReader[0] + "");
                    list[1].Add(myReader[1] + "");
                    list[2].Add(myReader[2] + "");
                    //Console.WriteLine(list[1].ToString() + ", "  + list[0].ToString() + ", " + list[2].ToString());
                    
                }
                // always call Close when done reading.

                
                myReader.Close();
                CloseConnection();
                /* for (int i = 0; i < list.Length; i++)
                 {
                     for (int j = 0; j < list[i].; j++)
                     {
                         Console.WriteLine(list[i][j].ToString());
                     }
                 }*/
                Console.WriteLine(list[0][0]);
                //Console.WriteLine(list[0][0].ToString());
                return list;
            }
            else
            {
                return list;
            }
        }

        public void continueGame(string user)
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
                    Console.WriteLine(myReader[0]);
                }
                // always call Close when done reading.
                myReader.Close();
            }
            CloseConnection();
        }

        public void completeLevelForFirstTime(int level, string user, int score)
        {
            string query = "SET @rank := (SELECT COUNT(*)+1 FROM leaderboard WHERE score > " + score + " AND level = " + level + "); INSERT INTO leaderboard VALUES(" + level + ", @rank, '" + user + "', " + score + "); UPDATE leaderboard SET ranking = CASE WHEN score < " + score + " THEN ranking+1 ELSE ranking END WHERE level = " + level + ";";
            string query2 = "SELECT user FROM leaderboard WHERE user='" + user + "' AND level=" + level + ";";
            //Open connection
            if (OpenConnection() == true)
            {
                MySqlCommand myCommand = new MySqlCommand(query2, connection);
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (myReader.Read())
                {
                    if ((string) myReader[0] == user)
                    {
                        myReader.Close();
                        CloseConnection();
                        updateHighScore(user, level, score);
                        return;
                    }
                }
                // always call Close when done reading.
                myReader.Close();
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

        public void updateHighScore(string user, int level, int score)
        {
            
            string query = "SET @rank := (SELECT COUNT(*)+1 FROM leaderboard WHERE score>" + score + " AND level=" + level + "); " +
                             "SET @old := (SELECT ranking FROM leaderboard WHERE user='" + user + "'); " +
                             "UPDATE leaderboard SET score=" + score + ", ranking=@rank WHERE user='" + user + "' AND level=" + level + "; " +
                             "UPDATE leaderboard SET ranking=" +
                             "CASE WHEN score<" + score + " THEN ranking+1 " +
                             "ELSE ranking " +
                             "END " +
                             "WHERE level=" + level + "; " +
                             "SET @new := (SELECT ranking FROM leaderboard WHERE user='" + user + "'); " +
                             "UPDATE leaderboard SET ranking=CASE WHEN @old=@new THEN ranking-1 END WHERE level=" + level + " AND ranking>@old;";
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

        public void saveGame(string user, int level)
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
