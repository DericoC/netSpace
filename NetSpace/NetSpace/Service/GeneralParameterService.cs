using System;
using System.Collections.Generic;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;

namespace NetSpace.Service
{
    public class GeneralParameterService : Connection, ICRUD<GeneralParameters>
    {
        private readonly string INSERT = "INSERT INTO general_parameters (general_param_name, description, value) VALUES (@general_param_name, @description, @value);";
        private readonly string UPDATE = "UPDATE general_parameters SET general_param_name = @general_param_name, description = @description, value = @value WHERE (general_parameter_id = @id);";
        private readonly string DELETE = "DELETE FROM general_parameters WHERE (general_parameter_id = @id);";
        private readonly string READ = "SELECT * FROM general_parameters;";
        private readonly string FINDBYGENERALPARAM = "SELECT * FROM general_parameters WHERE general_param_name = @param_name;";
        private readonly string FINDBYID = "SELECT * FROM general_parameters WHERE general_parameter_id = @id;";

        public bool insert(GeneralParameters item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@@general_param_name", item.general_param_name);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@value", item.value);
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

        public bool update(GeneralParameters item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@general_param_name", item.general_param_name);
                cmd.Parameters.AddWithValue("@description", item.description);
                cmd.Parameters.AddWithValue("@value", item.value);
                cmd.Parameters.AddWithValue("@id", item.general_parameter_id);
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

        public bool delete(GeneralParameters item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.general_parameter_id);
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


        public List<GeneralParameters> read()
        {
            List<GeneralParameters> parameters = new List<GeneralParameters>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        GeneralParameters gp = new GeneralParameters();
                        gp.general_parameter_id = rdr.GetInt32("general_parameter_id");
                        gp.general_param_name = rdr.GetString("general_param_name");
                        gp.description= rdr.GetString("description");
                        gp.value = rdr.GetString("value");
                        parameters.Add(gp);
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

            return parameters;
        }

        public GeneralParameters findById(int id)
        {
            GeneralParameters parameters = new GeneralParameters();
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
                        parameters.general_parameter_id = rdr.GetInt32("general_parameter_id");
                        parameters.general_param_name = rdr.GetString("general_param_name");
                        parameters.description = rdr.GetString("description");
                        parameters.value = rdr.GetString("value");
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

            return parameters;
        }

        public GeneralParameters findByGeneralParameter(string param)
        {
            GeneralParameters parameters = new GeneralParameters();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(FINDBYGENERALPARAM, this.getConnection());
                cmd.Parameters.AddWithValue("@param_name", param);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        parameters.general_parameter_id = rdr.GetInt32("general_parameter_id");
                        parameters.general_param_name = rdr.GetString("general_param_name");
                        parameters.description = rdr.GetString("description");
                        parameters.value = rdr.GetString("value");
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

            return parameters;
        }
    }
}

