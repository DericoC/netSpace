using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;
using Newtonsoft.Json;

namespace NetSpace.Service
{
    public class UserService : Connection, ICRUD<User>
    {
        private readonly string INSERT = "INSERT INTO users (first_name, last_name, gender, mail, phone, address, password, role, provider) VALUES (@first_name, @last_name, @gender, @mail, @phone, @address, @password, @role, @provider);";
        private readonly string UPDATE = "UPDATE users SET first_name = @first_name, last_name = @last_name, gender = @gender, mail = @mail, phone = @phone, address = @address, password = @password, role = @role, image = @image, provider = @provider WHERE (user_id = @id);";
        private readonly string DELETE = "DELETE FROM users WHERE (user_id = @id);";
        private readonly string READ = "SELECT * FROM users;";
        private readonly string READSPECIFIC = "SELECT * FROM users WHERE provider = @provider;";
        private readonly string BUSINESSMANAGER = "SELECT * FROM users WHERE role = 'Manager' AND provider = @business_id;";
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
                cmd.Parameters.AddWithValue("@provider", item.provider);
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
                cmd.Parameters.AddWithValue("@image", item.image);
                cmd.Parameters.AddWithValue("@provider", item.provider);
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
                        user.image = rdr.GetString("image");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                        user.provider = rdr.GetInt32("provider");
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

        public List<User> readSpecific(int business_id)
        {
            List<User> users = new List<User>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READSPECIFIC, this.getConnection());
                cmd.Parameters.AddWithValue("@provider", business_id);
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
                        user.image = rdr.GetString("image");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                        user.provider = rdr.GetInt32("provider");
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

        public List<User> businessManagers(int business_id)
        {
            List<User> users = new List<User>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(BUSINESSMANAGER, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", business_id);
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
                        user.image = rdr.GetString("image");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                        user.provider = rdr.GetInt32("provider");
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
                        user.image = rdr.GetString("image");
                        user.phone = rdr.GetString("phone");
                        user.address = rdr.GetString("address");
                        user.password = rdr.GetString("password");
                        user.role = rdr.GetString("role");
                        user.provider = rdr.GetInt32("provider");
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
                        userInfo.image = rdr.GetString("image");
                        userInfo.phone = rdr.GetString("phone");
                        userInfo.address = rdr.GetString("address");
                        userInfo.password = rdr.GetString("password");
                        userInfo.role = rdr.GetString("role");
                        userInfo.provider = rdr.GetInt32("provider");
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

        public async Task<string[]> upload(byte[] image, int clientId, string client_name)
        {
            string[] serverResponse = {"Nothing",""};

            try
            {
                Uri uri = new Uri("http://190.7.210.101/netspace/client/uploadProfilePic/" + clientId);
                //Uri uri = new Uri("http://localhost:8080/netspace-api/client/uploadProfilePic/" + clientId);

                var client = new HttpClient();
                var formData = new MultipartFormDataContent();

                formData.Add(new ByteArrayContent(image), "multipartFile", client_name + ".jpg");
                var response = await client.PostAsync(uri, formData);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();                    
                    serverResponse = JsonConvert.DeserializeObject<string[]>(content);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return serverResponse;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

