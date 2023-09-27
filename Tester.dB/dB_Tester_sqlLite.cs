

using System.Data.SQLite;
using System.Data.SqlTypes;

namespace Tester.dB
{
    public class dB_Tester_sqlLite : IdB_Tester
    {
        private const string DB_NAME = "MyDatabase.sqlite";
        private const string CONNECTION_STRING = "Data Source=MyDatabase.sqlite;Version=3;";
        private const string TABLE_NAME = "highscores";

        public void AnExample()
        {
            // This creates a zero-byte file

            if (!File.Exists(DB_NAME))
            {
                SQLiteConnection.CreateFile(DB_NAME);
            }
            
            
            SQLiteConnection m_dbConnection = new SQLiteConnection(CONNECTION_STRING);
            m_dbConnection.Open();

            // varchar will likely be handled internally as TEXT
            // the (20) will be ignored
            // see https://www.sqlite.org/datatype3.html

            String sql = String.Empty;
            SQLiteCommand? command;
            if (!TableExists(m_dbConnection, TABLE_NAME))
            {
                sql = $"Create Table {TABLE_NAME} (name varchar (20), score int)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }

            // you could also write sql = "CREATE TABLE IF NOT EXISTS highscores ..."
            

            sql = $"Insert into {TABLE_NAME} (name, score) values ('Me', {GetNewHighScore(m_dbConnection)})";
                command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();

        }

        public bool TableExists(SQLiteConnection connection, string tableName)
        {
            string query = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        public int GetNewHighScore(SQLiteConnection connection)
        {
            int score = 0;
            string query = "select max(score) from highscores; ";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                score = Convert.ToInt32(command.ExecuteScalar());
                score++;
            }
            return score;
        }
    }
}