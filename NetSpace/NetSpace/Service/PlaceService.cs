using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
	public class PlaceService : Connection, ICRUD<Place>
	{
        private readonly string INSERT = "INSERT INTO places (place_name, description, dimensions, capacity, amenities, image, restrooms, manager, policy, rating) VALUES (@place_name, @description, @dimensions, @capacity, @amenities, @image, @restrooms, @manager, @policy, @rating);";
        private readonly string UPDATE = "UPDATE places SET place_name = @place_name, description = @description, dimensions = @dimensions, capacity = @capacity, amenities = @amenities, image = @image, restrooms = @restrooms, manager = @manager, policy = @policy, rating = @rating WHERE (place_id = @id);";
        private readonly string DELETE = "DELETE FROM places WHERE (place_id = @id);";
        private readonly string READ = "SELECT * FROM places;";
        private readonly string FINDBYID = "SELECT * FROM places WHERE place_id = @id;";
        private readonly string BESTPLACES = "SELECT * FROM places ORDER BY rating desc LIMIT 3;";

        public bool insert(Place item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@place_name", item.place_name);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@dimensions", item.dimensions);
                cmd.Parameters.AddWithValue("@capacity", item.capacity);
                cmd.Parameters.AddWithValue("@amenities", item.amenities);
                cmd.Parameters.AddWithValue("@image", item.image);
                cmd.Parameters.AddWithValue("@restrooms", item.restrooms);
                cmd.Parameters.AddWithValue("@manager", item.manager.user_id);
                cmd.Parameters.AddWithValue("@policy", item.policy.policy_id);
                cmd.Parameters.AddWithValue("@rating", item.rating);
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

        public bool update(Place item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@place_name", item.place_name);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@dimensions", item.dimensions);
                cmd.Parameters.AddWithValue("@capacity", item.capacity);
                cmd.Parameters.AddWithValue("@amenities", item.amenities);
                cmd.Parameters.AddWithValue("@image", item.image);
                cmd.Parameters.AddWithValue("@restrooms", item.restrooms);
                cmd.Parameters.AddWithValue("@manager", item.manager.user_id);
                cmd.Parameters.AddWithValue("@policy", item.policy.policy_id);
                cmd.Parameters.AddWithValue("@rating", item.rating);
                cmd.Parameters.AddWithValue("@id", item.place_id);
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

        public bool delete(Place item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.place_id);
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

        public List<Place> read()
        {
            MySqlCommand cmd;
            List<Place> places = new List<Place>();
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Place place = new Place();
                        Policy policy = new Policy();
                        User user = new User();

                        place.place_id = rdr.GetInt32("place_id");
                        place.place_name = rdr.GetString("place_name");
                        place.description = rdr.GetString("description");
                        place.dimensions = rdr.GetString("dimensions");
                        place.capacity = rdr.GetInt32("capacity");
                        place.amenities = rdr.GetString("amenities");
                        place.image = rdr.GetString("image");
                        place.restrooms = rdr.GetBoolean("restrooms");
                        place.rating = rdr.GetInt32("rating");
                        place.manager = userService.findById(rdr.GetInt32("manager"));
                        place.policy = policyService.findById(rdr.GetInt32("policy"));

                        places.Add(place);
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

            return places;
        }

        public Place findById(int id)
        {
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();
            Place place = new Place();
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
                        place.place_id = rdr.GetInt32("place_id");
                        place.place_name = rdr.GetString("place_name");
                        place.description = rdr.GetString("description");
                        place.dimensions = rdr.GetString("dimensions");
                        place.capacity = rdr.GetInt32("capacity");
                        place.amenities = rdr.GetString("amenities");
                        place.image = rdr.GetString("image");
                        place.restrooms = rdr.GetBoolean("restrooms");
                        place.rating = rdr.GetInt32("rating");
                        place.manager = userService.findById(rdr.GetInt32("manager"));
                        place.policy = policyService.findById(rdr.GetInt32("policy"));
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

            return place;
        }

        public List<Place> topPlaces()
        {
            MySqlCommand cmd;
            List<Place> places = new List<Place>();
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();

            try
            {
                cmd = new MySqlCommand(BESTPLACES, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Place place = new Place();
                        Policy policy = new Policy();
                        User user = new User();

                        place.place_id = rdr.GetInt32("place_id");
                        place.place_name = rdr.GetString("place_name");
                        place.description = rdr.GetString("description");
                        place.dimensions = rdr.GetString("dimensions");
                        place.capacity = rdr.GetInt32("capacity");
                        place.amenities = rdr.GetString("amenities");
                        place.image = rdr.GetString("image");
                        place.restrooms = rdr.GetBoolean("restrooms");
                        place.rating = rdr.GetInt32("rating");
                        place.manager = userService.findById(rdr.GetInt32("manager"));
                        place.policy = policyService.findById(rdr.GetInt32("policy"));

                        places.Add(place);
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

            return places;
        }
    }
}

