using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class UserService : Connection, ICRUD<User>
    {
        private readonly string INSERT = "INSERT INTO users (first_name, last_name, gender, mail, phone, address, password, role) VALUES (@first_name, @last_name, @gender, @mail, @phone, @address, @password, @role);";
        private readonly string UPDATE = "UPDATE users SET first_name = @first_name, last_name = @last_name, gender = @gender, mail = @mail, phone = @phone, address = @address, password = @password, role = @role WHERE (user_id = @id);";
        private readonly string DELETE = "DELETE FROM users WHERE (user_id = @id);";
        private readonly string READ = "SELECT * FROM users;";
        private readonly string FINDBYID = "SELECT * FROM users WHERE user_id = @id;";
        private readonly string LOGIN = "SELECT * FROM users WHERE mail = @mail AND password = @password;";

        public bool insert(User item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@first_name", item.first_name);
                cmd.Parameters.AddWithValue("@last_name", item.last_name);
                cmd.Parameters.AddWithValue("@gender", item.gender);
                cmd.Parameters.AddWithValue("@mail", item.mail);
                cmd.Parameters.AddWithValue("@phone", item.phone);
                cmd.Parameters.AddWithValue("@address", item.address);
                cmd.Parameters.AddWithValue("@password", item.password);
                cmd.Parameters.AddWithValue("@role", item.role);
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }
            return success;
        }

        public bool update(User item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@first_name", item.first_name);
                cmd.Parameters.AddWithValue("@last_name", item.last_name);
                cmd.Parameters.AddWithValue("@gender", item.gender);
                cmd.Parameters.AddWithValue("@mail", item.mail);
                cmd.Parameters.AddWithValue("@phone", item.phone);
                cmd.Parameters.AddWithValue("@address", item.address);
                cmd.Parameters.AddWithValue("@password", item.password);
                cmd.Parameters.AddWithValue("@role", item.role);
                cmd.Parameters.AddWithValue("@id", item.user_id);
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }
            return success;
        }

        public bool delete(User item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.user_id);
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }
            return success;
        }

        public List<User> read()
        {
            List<User> users = new List<User>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        User user = new User();
                        user.user_id = rdr.GetInt32("user_id");
                        user.first_name = rdr.GetString("first_name");
                        user.last_name = rdr.GetString("last_name");
                        user.gender = rdr.GetString("gender");
                        user.mail = rdr.GetString("mail");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                        users.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }

            return users;
        }

        public User findById(int id)
        {
            User user = new User();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(FINDBYID, this.getConnection());
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        user.user_id = rdr.GetInt32("user_id");
                        user.first_name = rdr.GetString("first_name");
                        user.last_name = rdr.GetString("last_name");
                        user.gender = rdr.GetString("gender");
                        user.mail = rdr.GetString("mail");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }

            return user;
        }

        public bool login(UserSession logUser)
        {
            bool success = false;
            MySqlCommand cmd;


            try
            {
                cmd = new MySqlCommand(LOGIN, this.getConnection());
                cmd.Parameters.AddWithValue("@mail", logUser.getUser().mail);
                cmd.Parameters.AddWithValue("@password", logUser.getUser().password);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        User userInfo = new User();
                        userInfo.user_id = rdr.GetInt32("user_id");
                        userInfo.first_name = rdr.GetString("first_name");
                        userInfo.last_name = rdr.GetString("last_name");
                        userInfo.gender = rdr.GetString("gender");
                        userInfo.mail = rdr.GetString("mail");
                        userInfo.phone = rdr.GetString("phone");
                        userInfo.address = rdr.GetString("address");
                        userInfo.password = rdr.GetString("password");
                        userInfo.role = rdr.GetString("role");
                        logUser.setUser(userInfo);
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                this.disconnect();
            }

            return success;
        }
    }
}

