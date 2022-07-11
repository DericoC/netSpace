using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class TagsPlacesService : Connection, ICRUD<TagsPlaces>
    {
        private readonly string INSERT = "INSERT INTO tags_places (place_id, id_tag) VALUES (@place_id, @id_tag);";
        private readonly string DELETE = "DELETE FROM tags_places WHERE (place_id = @place_id) and (id_tag = @id_tag);";
        private readonly string READ = "SELECT * FROM tags;";

        public bool insert(TagsPlaces item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@id_tag", item.place.place_id);
                cmd.Parameters.AddWithValue("@id_tag", item.tag.tag_id);
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

        public bool update(TagsPlaces item)
        {
            throw new NotImplementedException("Update not possible. Please delete and insert new one.");
        }

        public bool delete(TagsPlaces item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
                cmd.Parameters.AddWithValue("@tag_id", item.tag.tag_id);
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

        public List<TagsPlaces> read()
        {
            MySqlCommand cmd;
            List<TagsPlaces> tagsPlaces = new List<TagsPlaces>();
            PlaceService placeService = new PlaceService();
            TagsService tagsService = new TagsService();

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        TagsPlaces tagPlace = new TagsPlaces();

                        tagPlace.place = placeService.findById(rdr.GetInt32("place_id"));
                        tagPlace.tag = tagsService.findById(rdr.GetInt32("id_tag"));
                        tagsPlaces.Add(tagPlace);
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

            return tagsPlaces;
        }
    }
}

