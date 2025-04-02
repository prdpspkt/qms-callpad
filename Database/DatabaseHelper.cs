using SQLite;

namespace QmsCallPad.Database
{
    internal class DatabaseHelper
    {
        private readonly SQLiteConnection _connection;

        public DatabaseHelper()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "callpad.db");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Setting>();
        }

        public Setting FirstRecord()
        {
            return _connection.Table<Setting>().FirstOrDefault();
        }


        public int SaveSetting(Setting setting)
        {
            if (setting.Id != 0)
            {
                return _connection.Update(setting);
            }
            else
            {
                return _connection.Insert(setting);
            }
        }

    }
}
