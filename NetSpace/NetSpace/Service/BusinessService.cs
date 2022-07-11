using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class BusinessService : Connection, ICRUD<Business>
    {
        private readonly string INSERT = "INSERT INTO business (business_name, type, place) VALUES (@business_name, @type, @place);";
        private readonly string UPDATE = "UPDATE business SET business_name = @business_name, type = @type, place = @place WHERE (business_id = @id);";
        private readonly string DELETE = "DELETE FROM business WHERE (business_id = @id);";
        private readonly string READ = "SELECT * FROM business b, places p WHERE b.place = p.place_id;";
        private readonly string FINDBYID = "SELECT * FROM business WHERE business_id = @id;";

        public bool insert(Business item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@business_name", item.business_name);
                cmd.Parameters.AddWithValue("@type", item.type);
                cmd.Parameters.AddWithValue("@place", item.place.place_id);
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

        public bool update(Business item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@business_name", item.business_name);
                cmd.Parameters.AddWithValue("@type", item.type);
                cmd.Parameters.AddWithValue("@place", item.place.place_id);
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
                        b.place = placeService.findById(rdr.GetInt32("place"));
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
                MySqlDataReader rdr = cmd.ExecuteReader();
                cmd.Parameters.AddWithValue("@id", id);
                PlaceService placeService = new PlaceService();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        business.business_id = rdr.GetInt32("business_id");
                        business.business_name = rdr.GetString("business_name");
                        business.type = rdr.GetInt32("type");
                        business.place = placeService.findById(rdr.GetInt32("place"));
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
    }
}

