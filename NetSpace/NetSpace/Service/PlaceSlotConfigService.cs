using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class PlaceSlotConfigService : Connection, ICRUD<PlaceSlotConfig>
    {
        private readonly string INSERT = "INSERT INTO place_slot_config(frequency, start_time, end_time, place_id) VALUES(@frequency, @start_time, @end_time, @place_id);";
        private readonly string UPDATE = "UPDATE place_slot_config SET frequency = @frequency, start_time = @start_time, end_time = @end_time, place_id = @place_id WHERE(place_slot_config_id = @id);";
        private readonly string DELETE = "DELETE FROM place_slot_config WHERE (place_slot_config_id = @id);";
        private readonly string READ = "SELECT * FROM place_slot_config;";
        private readonly string FINDBYID = "SELECT * FROM place_slot_config WHERE place_slot_config_id = @id;";

        public bool insert(PlaceSlotConfig item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@frequency", item.frequency);
                cmd.Parameters.AddWithValue("@start_time", item.start_time);
                cmd.Parameters.AddWithValue("@end_time", item.end_time);
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
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

        public bool update(PlaceSlotConfig item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@frequency", item.frequency);
                cmd.Parameters.AddWithValue("@start_time", item.start_time);
                cmd.Parameters.AddWithValue("@end_time", item.end_time);
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
                cmd.Parameters.AddWithValue("@id", item.place_slot_config_id);
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

        public bool delete(PlaceSlotConfig item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.place_slot_config_id);
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

        public List<PlaceSlotConfig> read()
        {
            MySqlCommand cmd;
            List<PlaceSlotConfig> slotsConfig = new List<PlaceSlotConfig>();
            PlaceService placeService = new PlaceService();
            GeneralParameterService generalService = new GeneralParameterService();

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        PlaceSlotConfig config = new PlaceSlotConfig();

                        config.place_slot_config_id = rdr.GetInt32("place_slot_config_id");
                        config.frequency = generalService.findById(rdr.GetInt32("frequency"));
                        config.start_time = rdr.GetInt32("start_time");
                        config.end_time = rdr.GetInt32("end_time");
                        config.place = placeService.findById(rdr.GetInt32("place_id"));
                        slotsConfig.Add(config);
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

            return slotsConfig;
        }

        public PlaceSlotConfig findById(int id)
        {
            MySqlCommand cmd;
            PlaceSlotConfig slotsConfig = new PlaceSlotConfig();
            PlaceService placeService = new PlaceService();
            GeneralParameterService generalService = new GeneralParameterService();

            try
            {
                cmd = new MySqlCommand(FINDBYID, this.getConnection());
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        PlaceSlotConfig config = new PlaceSlotConfig();

                        config.place_slot_config_id = rdr.GetInt32("place_slot_config_id");
                        config.frequency = generalService.findById(rdr.GetInt32("frequency"));
                        config.start_time = rdr.GetInt32("start_time");
                        config.end_time = rdr.GetInt32("end_time");
                        config.place = placeService.findById(rdr.GetInt32("place_id"));
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

            return slotsConfig;
        }
    }
}

