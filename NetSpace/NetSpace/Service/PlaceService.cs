using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;
using Newtonsoft.Json;

namespace NetSpace.Service
{
	public class PlaceService : Connection, ICRUD<Place>
	{
        private readonly string INSERT = "INSERT INTO places (business_id, place_name, description, dimensions, capacity, amenities, restrooms, manager, policy, rating) VALUES (@business_id, @place_name, @description, @dimensions, @capacity, @amenities, @restrooms, @manager, @policy, @rating);";
        private readonly string UPDATE = "UPDATE places SET business_id = @business_id, place_name = @place_name, description = @description, dimensions = @dimensions, capacity = @capacity, amenities = @amenities, image = @image, restrooms = @restrooms, manager = @manager, policy = @policy, rating = @rating WHERE (place_id = @id);";
        private readonly string DELETE = "DELETE FROM places WHERE (place_id = @id);";
        private readonly string READ = "SELECT * FROM places p, business b, users u, policies po WHERE p.business_id = b.business_id AND p.manager = u.user_id AND p.policy = po.policy_id;";
        private readonly string READSPECIFIC = "SELECT * FROM places p, business b, users u, policies po WHERE p.business_id = b.business_id AND p.manager = u.user_id AND p.policy = po.policy_id AND p.business_id = @business_id;";
        private readonly string FINDBYID = "SELECT * FROM places WHERE place_id = @id;";
        private readonly string BESTPLACES = "SELECT * FROM places WHERE business_id = @business_id ORDER BY rating desc LIMIT 3;";
        private readonly string FINDBYNAMEANDDESC = "SELECT place_id FROM places WHERE place_name = @name AND description = @desc;";

        public bool insert(Place item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", item.business.business_id);
                cmd.Parameters.AddWithValue("@place_name", item.place_name);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@dimensions", item.dimensions);
                cmd.Parameters.AddWithValue("@capacity", item.capacity);
                cmd.Parameters.AddWithValue("@amenities", item.amenities);
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
                cmd.Parameters.AddWithValue("@business_id", item.business.business_id);
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
            BusinessService businessService = new BusinessService();
            TagsPlacesService tagsPlacesService = new TagsPlacesService();
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
                        place.business = new Business();
                        place.business.business_id = rdr.GetInt32("business_id");
                        place.business.business_name = rdr.GetString("business_name");
                        place.business.type = rdr.GetInt32("type");
                        place.place_id = rdr.GetInt32("place_id");
                        place.place_name = rdr.GetString("place_name");
                        place.description = rdr.GetString("description");
                        place.dimensions = rdr.GetString("dimensions");
                        place.capacity = rdr.GetInt32("capacity");
                        place.amenities = rdr.GetString("amenities");
                        place.image = rdr.GetString("image");
                        place.restrooms = rdr.GetBoolean("restrooms");
                        place.rating = rdr.GetInt32("rating");
                        place.manager = new User();
                        place.manager.user_id = rdr.GetInt32("user_id");
                        place.manager.first_name = rdr.GetString("first_name");
                        place.manager.last_name = rdr.GetString("last_name");
                        place.manager.gender = rdr.GetString("gender");
                        place.manager.mail = rdr.GetString("mail");
                        place.manager.image = rdr.GetString("image");
                        place.manager.phone = rdr.GetString("phone");
                        place.manager.address = rdr.GetString("address");
                        place.manager.password = rdr.GetString("password");
                        place.manager.role = rdr.GetString("role");
                        place.manager.provider = rdr.GetInt32("provider");
                        place.policy = new Policy();
                        place.policy.policy_id = rdr.GetInt32("policy_id");
                        place.policy.policy_business_id = rdr.GetInt32("policy_business_id");
                        place.policy.policy_name = rdr.GetString("policy_name");
                        place.policy.age_restriction = rdr.GetInt32("age_restriction");
                        place.policy.deposit = rdr.GetDouble("deposit");
                        place.policy.price = rdr.GetDouble("price");
                        place.tags = tagsPlacesService.readTagsPlaces(place.place_id);
                        place.tagsString = tagsToCommaSeparated(place.tags);
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

        public List<Place> readSpecificBusiness(int business_id)
        {
            MySqlCommand cmd;
            List<Place> places = new List<Place>();
            UserService userService = new UserService();
            BusinessService businessService = new BusinessService();
            PolicyService policyService = new PolicyService();
            TagsPlacesService tagsPlacesService = new TagsPlacesService();

            try
            {
                cmd = new MySqlCommand(READSPECIFIC, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", business_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Place place = new Place();
                        place.business = new Business();
                        place.business.business_id = rdr.GetInt32("business_id");
                        place.business.business_name = rdr.GetString("business_name");
                        place.business.type = rdr.GetInt32("type");
                        place.place_id = rdr.GetInt32("place_id");
                        place.place_name = rdr.GetString("place_name");
                        place.description = rdr.GetString("description");
                        place.dimensions = rdr.GetString("dimensions");
                        place.capacity = rdr.GetInt32("capacity");
                        place.amenities = rdr.GetString("amenities");
                        place.image = rdr.GetString("image");
                        place.restrooms = rdr.GetBoolean("restrooms");
                        place.rating = rdr.GetInt32("rating");
                        place.manager = new User();
                        place.manager.user_id = rdr.GetInt32("user_id");
                        place.manager.first_name = rdr.GetString("first_name");
                        place.manager.last_name = rdr.GetString("last_name");
                        place.manager.gender = rdr.GetString("gender");
                        place.manager.mail = rdr.GetString("mail");
                        place.manager.image = rdr.GetString("image");
                        place.manager.phone = rdr.GetString("phone");
                        place.manager.address = rdr.GetString("address");
                        place.manager.password = rdr.GetString("password");
                        place.manager.role = rdr.GetString("role");
                        place.manager.provider = rdr.GetInt32("provider");
                        place.policy = new Policy();
                        place.policy.policy_id = rdr.GetInt32("policy_id");
                        place.policy.policy_business_id = rdr.GetInt32("policy_business_id");
                        place.policy.policy_name = rdr.GetString("policy_name");
                        place.policy.age_restriction = rdr.GetInt32("age_restriction");
                        place.policy.deposit = rdr.GetDouble("deposit");
                        place.policy.price = rdr.GetDouble("price");
                        place.tags = tagsPlacesService.readTagsPlaces(place.place_id);
                        place.tagsString = tagsToCommaSeparated(place.tags);
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
            BusinessService businessService = new BusinessService();
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();
            TagsPlacesService tagsPlacesService = new TagsPlacesService();
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
                        place.business = businessService.findById(rdr.GetInt32("business_id"));
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
                        place.tags = tagsPlacesService.readTagsPlaces(place.place_id);
                        place.tagsString = tagsToCommaSeparated(place.tags);
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

        public int findByNameAndDescription(string name, string desc)
        {
            BusinessService businessService = new BusinessService();
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();
            int placeId = 0;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(FINDBYNAMEANDDESC, this.getConnection());
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@desc", desc);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        placeId = rdr.GetInt32("place_id");
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

            return placeId;
        }

        public List<Place> topPlaces(int business_id)
        {
            MySqlCommand cmd;
            List<Place> places = new List<Place>();
            BusinessService businessService = new BusinessService();
            UserService userService = new UserService();
            PolicyService policyService = new PolicyService();
            TagsPlacesService tagsPlacesService = new TagsPlacesService();

            try
            {
                cmd = new MySqlCommand(BESTPLACES, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", business_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Place place = new Place();
                        Policy policy = new Policy();
                        User user = new User();
                        place.business = businessService.findById(rdr.GetInt32("business_id"));
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
                        place.tags = tagsPlacesService.readTagsPlaces(place.place_id);
                        place.tagsString = tagsToCommaSeparated(place.tags);
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

        public async Task<string[]> upload(byte[] image, int placeId, string place_name)
        {
            string[] serverResponse = { "Nothing", "" };

            try
            {
                Uri uri = new Uri("http://190.7.210.101/netspace/place/uploadPlaceImage/" + placeId);
                //Uri uri = new Uri("http://localhost:8080/netspace-api/client/uploadPlaceImage/" + placeId);

                var client = new HttpClient();
                var formData = new MultipartFormDataContent();

                formData.Add(new ByteArrayContent(image), "multipartFile", place_name + ".jpg");
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

        private string tagsToCommaSeparated(List<Tags> tags)
        {
            string tagsOnString = "";

            if (tags.Count != 0)
            {
                Tags lastTag = tags.Last();

                foreach (var item in tags)
                {
                    if (item != lastTag)
                    {
                        tagsOnString = tagsOnString + item.name + ", ";
                    }
                    else
                    {
                        tagsOnString = tagsOnString + item.name;
                    }
                }
            }

            return tagsOnString;
        }

        public bool updatePlaceAndSlotAndDeleteTags(Place place, PlaceSlotConfig config)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand();
                cmd.Connection = this.getConnection();
                cmd.CommandText = "updatePlace";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OBJ_business_id", place.business.business_id);
                cmd.Parameters.AddWithValue("@OBJ_place_name", place.place_name);
                cmd.Parameters.AddWithValue("@OBJ_description", place.description);
                cmd.Parameters.AddWithValue("@OBJ_dimensions", place.dimensions);
                cmd.Parameters.AddWithValue("@OBJ_capacity", place.capacity);
                cmd.Parameters.AddWithValue("@OBJ_amenities", place.amenities);
                cmd.Parameters.AddWithValue("@OBJ_image", place.image);
                cmd.Parameters.AddWithValue("@OBJ_restrooms", place.restrooms);
                cmd.Parameters.AddWithValue("@OBJ_manager", place.manager.user_id);
                cmd.Parameters.AddWithValue("@OBJ_policy", place.policy.policy_id);
                cmd.Parameters.AddWithValue("@OBJ_rating", place.rating);
                cmd.Parameters.AddWithValue("@OBJ_place_id", place.place_id);
                cmd.Parameters.AddWithValue("@OBJ_frequency", config.frequency.general_parameter_id);
                cmd.Parameters.AddWithValue("@OBJ_start_time", config.start_time);
                cmd.Parameters.AddWithValue("@OBJ_end_time", config.end_time);
                cmd.Parameters.AddWithValue("@OBJ_place_slot_config_id", config.place_slot_config_id);
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
    }
}

