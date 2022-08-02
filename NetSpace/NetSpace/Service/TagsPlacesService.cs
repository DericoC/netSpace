using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class TagsPlacesService : Connection, ICRUD<TagsPlaces>
    {
        private readonly string INSERT = "INSERT INTO tags_places (place_id, id_tag) VALUES (@place_id, @id_tag);";
        private readonly string UPDATE = "UPDATE tags_places SET id_tag = @tag_change WHERE (place_id = @place_id) and (id_tag = @tag_id);";
        private readonly string DELETE = "DELETE FROM tags_places WHERE (place_id = @place_id) and (id_tag = @id_tag);";
        private readonly string READ = "SELECT * FROM tags;";
        private readonly string FINDBYPLACE = "SELECT id_tag FROM tags_places WHERE place_id = @place_id;";
        private readonly string FINDALLTAGSOFPLACE = "SELECT t.* FROM places p, tags t, tags_places tp WHERE p.place_id = tp.place_id AND t.tag_id = tp.id_tag AND p.place_id = @place_id;";

        public bool insert(TagsPlaces item)
        {
            return false;
        }

        public async Task<bool> insert(TagsPlaces item, bool async)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
                cmd.Parameters.AddWithValue("@id_tag", item.tag.tag_id);
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

        public bool update(TagsPlaces item)
        {
            throw new NotImplementedException("Update not possible. Please delete and insert new one.");
        }

        public bool update(TagsPlaces item, int newvalue)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@tag_change", newvalue);
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

        public int findByPlaceId(int place_id)
        {
            MySqlCommand cmd;
            List<TagsPlaces> tagsPlaces = new List<TagsPlaces>();
            PlaceService placeService = new PlaceService();
            int oldTag = 0;

            try
            {
                cmd = new MySqlCommand(FINDBYPLACE, this.getConnection());
                cmd.Parameters.AddWithValue("@place_id", place_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        oldTag = rdr.GetInt32("id_tag");
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

            return oldTag;
        }

        public List<Tags> readTagsPlaces(int place_id)
        {
            MySqlCommand cmd;
            List<Tags> tagsPlaces = new List<Tags>();

            try
            {
                cmd = new MySqlCommand(FINDALLTAGSOFPLACE, this.getConnection());
                cmd.Parameters.AddWithValue("@place_id", place_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Tags tag = new Tags();

                        tag.tag_id = rdr.GetInt32("tag_id");
                        tag.name = rdr.GetString("name");
                        tag.value = rdr.GetString("value");
                        tagsPlaces.Add(tag);
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

