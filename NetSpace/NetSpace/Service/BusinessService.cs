using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class BusinessService : Connection, ICRUD<Business>
    {
        private readonly string INSERT = "INSERT INTO business (business_name, type) VALUES (@business_name, @type);";
        private readonly string UPDATE = "UPDATE business SET business_name = @business_name, type = @type WHERE (business_id = @id);";
        private readonly string DELETE = "DELETE FROM business WHERE (business_id = @id);";
        private readonly string READ = "SELECT * FROM business b, general_parameters g WHERE b.type = g.general_parameter_id;";
        private readonly string FINDBYID = "SELECT * FROM business b, general_parameters g WHERE b.type = g.general_parameter_id AND b.business_id = @id;";
        private readonly string FINDINSERTED = "SELECT business_id FROM business WHERE business_name = @name AND type = @type";

        public bool insert(Business item)
        {
            return false;
        }

        public async Task<bool> insertAsync(Business item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@business_name", item.business_name);
                cmd.Parameters.AddWithValue("@type", item.typeObject.general_parameter_id);
                await cmd.ExecuteNonQueryAsync();
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

        public bool update(Business item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@business_name", item.business_name);
                cmd.Parameters.AddWithValue("@type", item.typeObject.general_parameter_id);
                cmd.Parameters.AddWithValue("@id", item.business_id);
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

        public bool delete(Business item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.business_id);
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

        public List<Business> read()
        {
            List<Business> business = new List<Business>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();
                PlaceService placeService = new PlaceService();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Business b = new Business();
                        b.business_id = rdr.GetInt32("business_id");
                        b.business_name = rdr.GetString("business_name");
                        b.type = rdr.GetInt32("type");
                        b.typeObject.general_parameter_id = rdr.GetInt32("type");
                        b.typeObject.general_param_name = rdr.GetString("general_param_name");
                        b.typeObject.description = rdr.GetString("description");
                        b.typeObject.value = rdr.GetString("value");
                        business.Add(b);
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

            return business;
        }

        public Business findById(int id)
        {
            Business business = new Business();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(FINDBYID, this.getConnection());
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();
                PlaceService placeService = new PlaceService();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        business.business_id = rdr.GetInt32("business_id");
                        business.business_name = rdr.GetString("business_name");
                        business.type = rdr.GetInt32("type");
                        business.typeObject.general_parameter_id = rdr.GetInt32("type");
                        business.typeObject.general_param_name = rdr.GetString("general_param_name");
                        business.typeObject.description = rdr.GetString("description");
                        business.typeObject.value = rdr.GetString("value");
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

            return business;
        }

        public int findLatestId(string name, int type)
        {
            int business_id = 0;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(FINDINSERTED, this.getConnection());
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@type", type);
                MySqlDataReader rdr = cmd.ExecuteReader();
                PlaceService placeService = new PlaceService();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        business_id = rdr.GetInt32("business_id");
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

            return business_id;
        }
    }
}

