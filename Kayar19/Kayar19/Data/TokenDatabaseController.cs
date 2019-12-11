using Kayar19.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kayar19.Data
{
    public class TokenDatabaseController
    {
        static object locker = new object();
        SQLiteConnection database;
        public TokenDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Token>();
        }
        public Token GetToken()
        {
            lock (locker)
            {
                if (database.Table<Token>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<Token>().First();
                }
            }
        }
        public int SaveToken(Token token)
        {
            lock (locker)
            {
                if (Token.Id != 0)
                {
                    database.Update(token);
                    return Token.Id;
                }
                else
                {
                    return database.Insert(token);
                }
            }
        }
        public int DeleteToken<Token>(int Id)
        {
            lock (locker)
            {
                return database.Delete<Token>(Id);
            }
        }
    }
}
