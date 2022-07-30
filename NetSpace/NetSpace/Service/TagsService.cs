using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class TagsService : Connection, ICRUD<Tags>
    {
        private readonly string INSERT = "INSERT INTO tags(name, value) VALUES(@name, @value);";
        private readonly string UPDATE = "UPDATE tags SET name = @name, value = @value WHERE(tag_id = @id);";
        private readonly string DELETE = "DELETE FROM tags WHERE(tag_id = @id);";
        private readonly string READ = "SELECT * FROM tags;";
        private readonly string FINDBYID = "SELECT * FROM tags WHERE tag_id = @id;";

        public bool insert(Tags item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@name", item.name);
                cmd.Parameters.AddWithValue("@values", item.value);
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

        public bool update(Tags item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@name", item.name);
                cmd.Parameters.AddWithValue("@value", item.value);
                cmd.Parameters.AddWithValue("@id", item.tag_id);
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

        public bool delete(Tags item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.tag_id);
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

        public List<Tags> read()
        {
            MySqlCommand cmd;
            List<Tags> tags = new List<Tags>();

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Tags tag = new Tags();

                        tag.tag_id = rdr.GetInt32("tag_id");
                        tag.name = rdr.GetString("name");
                        tag.value = rdr.GetString("value");
                        tags.Add(tag);
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

            return tags;
        }

        public Tags findById(int id)
        {
            MySqlCommand cmd;
            Tags tag = new Tags();

            try
            {
                cmd = new MySqlCommand(FINDBYID, this.getConnection());
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {

                        tag.tag_id = rdr.GetInt32("tag_id");
                        tag.value = rdr.GetString("value");
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

            return tag;
        }
    }
}

