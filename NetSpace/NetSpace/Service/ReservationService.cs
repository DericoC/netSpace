using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class ReservationService : Connection, ICRUD<Reservation>
    {
        private readonly string INSERT = "INSERT INTO reservations(user_id, place_id, business_id, qr, date_slot, time_start, time_end) VALUES(@user_id, @place_id, @business_id, @qr, @date_slot, @time_start, @time_end);";
        private readonly string UPDATE = "UPDATE reservations SET user_id = @user_id, place_id = @place_id, business_id = @business_id, qr = @qr, date_slot = @date_slot, time_start = @time_start, time_end = @time_end WHERE(reservation_id = @id);";
        private readonly string DELETE = "DELETE FROM reservations WHERE(reservation_id = @id);";
        private readonly string READ = "SELECT * FROM reservations;";
        private readonly string READSPECIFIC = "SELECT * FROM reservations WHERE business_id = @business_id;";
        private readonly string READCLIENTSPECIFIC = "SELECT * FROM reservations WHERE user_id = @client_id;";
        private readonly string FINDBYID = "SELECT * FROM reservations WHERE reservation_id = @id;";
        private readonly string NEARESTRESERVATIONS = "SELECT * FROM reservations WHERE business_id = @business_id AND date_slot > NOW() ORDER BY 6 asc LIMIT 3;";

        public bool insert(Reservation item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@user_id", item.user.user_id);
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
                cmd.Parameters.AddWithValue("@business_id", item.business.business_id);
                cmd.Parameters.AddWithValue("@qr", item.qr);
                cmd.Parameters.AddWithValue("@date_slot", item.date_slot);
                cmd.Parameters.AddWithValue("@time_start", (item.time_start.Hours * 60) + item.time_start.Minutes);
                cmd.Parameters.AddWithValue("@time_end", (item.time_end.Hours * 60) + item.time_end.Minutes);
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

        public bool update(Reservation item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@user_id", item.user.user_id);
                cmd.Parameters.AddWithValue("@place_id", item.place.place_id);
                cmd.Parameters.AddWithValue("@business_id", item.business.business_id);
                cmd.Parameters.AddWithValue("@qr", item.qr);
                cmd.Parameters.AddWithValue("@date_slot", item.date_slot);
                cmd.Parameters.AddWithValue("@time_start", item.time_start);
                cmd.Parameters.AddWithValue("@time_end", item.time_end);
                cmd.Parameters.AddWithValue("@id", item.reservation_id);
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

        public bool delete(Reservation item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.reservation_id);
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

        public List<Reservation> read()
        {
            MySqlCommand cmd;
            List<Reservation> reservations = new List<Reservation>();
            UserService userService = new UserService();
            PlaceService placeService = new PlaceService();
            BusinessService businessService = new BusinessService();
            TimeConverter timeConverter = new TimeConverter();

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Reservation r = new Reservation();
                        r.reservation_id = rdr.GetInt32("reservation_id");
                        r.user = userService.findById(rdr.GetInt32("user_id"));
                        r.place = placeService.findById(rdr.GetInt32("place_id"));
                        r.business = businessService.findById(rdr.GetInt32("business_id"));
                        r.qr = rdr.GetString("qr");
                        r.date_slot = rdr.GetDateTime("date_slot");
                        r.time_start = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_start"));
                        r.time_end = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_end"));

                        reservations.Add(r);
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

            return reservations;
        }

        public List<Reservation> readSpecific(int business_id)
        {
            MySqlCommand cmd;
            List<Reservation> reservations = new List<Reservation>();
            UserService userService = new UserService();
            PlaceService placeService = new PlaceService();
            BusinessService businessService = new BusinessService();
            TimeConverter timeConverter = new TimeConverter();

            try
            {
                cmd = new MySqlCommand(READSPECIFIC, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", business_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Reservation r = new Reservation();
                        r.reservation_id = rdr.GetInt32("reservation_id");
                        r.user = userService.findById(rdr.GetInt32("user_id"));
                        r.place = placeService.findById(rdr.GetInt32("place_id"));
                        r.business = businessService.findById(rdr.GetInt32("business_id"));
                        r.qr = rdr.GetString("qr");
                        r.date_slot = rdr.GetDateTime("date_slot");
                        r.time_start = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_start"));
                        r.time_end = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_end"));

                        reservations.Add(r);
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

            return reservations;
        }

        public List<Reservation> readClient(int client_id)
        {
            MySqlCommand cmd;
            List<Reservation> reservations = new List<Reservation>();
            UserService userService = new UserService();
            PlaceService placeService = new PlaceService();
            BusinessService businessService = new BusinessService();
            TimeConverter timeConverter = new TimeConverter();

            try
            {
                cmd = new MySqlCommand(READCLIENTSPECIFIC, this.getConnection());
                cmd.Parameters.AddWithValue("@client_id", client_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Reservation r = new Reservation();
                        r.reservation_id = rdr.GetInt32("reservation_id");
                        r.user = userService.findById(rdr.GetInt32("user_id"));
                        r.place = placeService.findById(rdr.GetInt32("place_id"));
                        r.business = businessService.findById(rdr.GetInt32("business_id"));
                        r.qr = rdr.GetString("qr");
                        r.date_slot = rdr.GetDateTime("date_slot");
                        r.time_start = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_start"));
                        r.time_end = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_end"));

                        reservations.Add(r);
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

            return reservations;
        }

        public Reservation findById(int id)
        {
            UserService userService = new UserService();
            PlaceService placeService = new PlaceService();
            BusinessService businessService = new BusinessService();
            Reservation reservation = new Reservation();
            TimeConverter timeConverter = new TimeConverter();
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
                        reservation.reservation_id = rdr.GetInt32("place_id");
                        reservation.user = userService.findById(rdr.GetInt32("place_name"));
                        reservation.place = placeService.findById(rdr.GetInt32("description"));
                        reservation.business = businessService.findById(rdr.GetInt32("dimensions"));
                        reservation.qr = rdr.GetString("qr");
                        reservation.date_slot = rdr.GetDateTime("date_slot");
                        reservation.time_start = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_start"));
                        reservation.time_end = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_end"));
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

            return reservation;
        }

        public List<Reservation> nearestReservations(int business_id)
        {
            MySqlCommand cmd;
            List<Reservation> reservations = new List<Reservation>();
            UserService userService = new UserService();
            PlaceService placeService = new PlaceService();
            BusinessService businessService = new BusinessService();
            TimeConverter timeConverter = new TimeConverter();

            try
            {
                cmd = new MySqlCommand(NEARESTRESERVATIONS, this.getConnection());
                cmd.Parameters.AddWithValue("@bussiness_id", business_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Reservation r = new Reservation();
                        r.reservation_id = rdr.GetInt32("reservation_id");
                        r.user = userService.findById(rdr.GetInt32("user_id"));
                        r.place = placeService.findById(rdr.GetInt32("place_id"));
                        r.business = businessService.findById(rdr.GetInt32("business_id"));
                        r.qr = rdr.GetString("qr");
                        r.date_slot = rdr.GetDateTime("date_slot");
                        r.time_start = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_start"));
                        r.time_end = timeConverter.getTimeFromMinutes(rdr.GetInt32("time_end"));

                        reservations.Add(r);
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

            return reservations;
        }
    }
}

