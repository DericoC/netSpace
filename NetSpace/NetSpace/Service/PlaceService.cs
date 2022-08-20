using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;
using Newtonsoft.Json;
using Xamarin.Essentials;

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

        public void sendConfirmationMail(string to, string code, string name, string place, string day, string start, string end)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("netspacecr@gmail.com");
                mail.To.Add(to);
                mail.Subject = "NetSpace";
                mail.IsBodyHtml = true;
                mail.Body = "<!doctype html><html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\"><head> <title>Confirmación </title> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> <style type=\"text/css\"> #outlook a { padding: 0; } .ReadMsgBody { width: 100%; } .ExternalClass { width: 100%; } .ExternalClass * { line-height: 100%; } body { margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; } table, td { border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; } img { border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; } p { display: block; margin: 13px 0; } </style> <!--[if !mso]><!--> <style type=\"text/css\"> @media only screen and (max-width:480px) { @-ms-viewport { width: 320px; } @viewport { width: 320px; } } </style> <style type=\"text/css\"> @media only screen and (min-width:480px) { .mj-column-per-100 { width: 100% !important; } } </style> <style type=\"text/css\"> </style></head><body style=\"background-color:#f9f9f9;\"> <div style=\"background-color:#f9f9f9;\"> <div style=\"background:#f9f9f9;background-color:#f9f9f9;Margin:0px auto;max-width:600px;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"background:#f9f9f9;background-color:#f9f9f9;width:100%;\"> <tbody> <tr> <td style=\"border-bottom:#F9A007 solid 5px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;\"> </td> </tr> </tbody> </table> </div> <div style=\"background:#fff;background-color:#fff;Margin:0px auto;max-width:600px;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"background:#fff;background-color:#fff;width:100%;\"> <tbody> <tr> <td style=\"border:#dddddd solid 1px;border-top:0px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;\"> <div class=\"mj-column-per-100 outlook-group-fix\" style=\"font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"vertical-align:bottom;\" width=\"100%\"> <tr> <td align=\"center\" style=\"font-size:0px;padding:10px 25px;word-break:break-word;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"border-collapse:collapse;border-spacing:0px;\"> <tbody> <tr> <td style=\"width:120px;\"> <img height=\"auto\" src=\"http://190.7.210.101/imagesNetspace/no-image/netspace_logo.jpg\" style=\"border:0;display:block;outline:none;text-decoration:none;width:100%;\" width=\"64\" /> </td> </tr> </tbody> </table> </td> </tr> <tr> <td align=\"center\" style=\"font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:28px;font-weight:bold;line-height:1;text-align:center;color:#555;\"> Confirmación de Reserva <br/> " + place + " </div> </td> </tr> <tr> <td align=\"left\" style=\"font-size:0px;padding:10px 25px;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:16px;line-height:22px;text-align:left;color:#555;\"> <b>Hola, " + name + "!</b><br></br> Gracias por reservar con nosotros, aqui encontraras los detalles: </div> </td> </tr> <tr> <td align=\"left\" style=\"font-size:0px;padding:10px 25px;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:16px;line-height:22px;text-align:left;color:#555;\"> <b>Codigo:</b> " + code + " <br/> <b>Lugar:</b> " + place + " <br/> <b>Fecha:</b> " + day + " <br/> <b>Hora Inicio:</b> " + start + " <br/> <b>Hora Final:</b> " + end + " <br/> </div> </td> </tr> <tr> <td align=\"left\" style=\"font-size:0px;padding:10px 25px;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:14px;line-height:20px;text-align:left;color:#525252;\"> Saludos,<br><br>NetSpace<br> </div> </td> </tr> </table> </div> </td> </tr> </tbody> </table> </div> <div style=\"Margin:0px auto;max-width:600px;\"> <table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" style=\"width:100%;\"> <tbody> <tr> <td style=\"direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;\"> <div class=\"mj-column-per-100 outlook-group-fix\" style=\"font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" width=\"100%\"> <tbody> <tr> <td style=\"vertical-align:bottom;padding:0;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" role=\"presentation\" width=\"100%\"> <tr> <td align=\"center\" style=\"font-size:0px;padding:0;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:12px;font-weight:300;line-height:1;text-align:center;color:#575757;\"> San Pedro, San Jose, Costa Rica </div> </td> </tr> <tr> <td align=\"center\" style=\"font-size:0px;padding:10px;word-break:break-word;\"> <div style=\"font-family:'Helvetica Neue',Arial,sans-serif;font-size:12px;font-weight:300;line-height:1;text-align:center;color:#575757;\"> <a href=\"\" style=\"color:#575757\">Dejar de recibir</a> las confirmaciones </div> </td> </tr> </table> </td> </tr> </tbody> </table> </div> </td> </tr> </tbody> </table> </div> </div></body></html>";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("netspacecr@gmail.com", "ubwlkuxrdxryeslk");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

