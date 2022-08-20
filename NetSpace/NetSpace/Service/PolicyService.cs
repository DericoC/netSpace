using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MySqlConnector;
using NetSpace.Model;
using NetSpace.Util;
using Newtonsoft.Json;

namespace NetSpace.Service
{
	public class PolicyService : Connection, ICRUD<Policy>
	{
        private readonly string INSERT = "INSERT INTO policies (policy_name, policy_business_id, age_restriction, deposit, price) VALUES (@policy_name, @policy_business_id, @age_restriction, @deposit, @price);";
        private readonly string UPDATE = "UPDATE policies SET policy_name = @policy_name, business_id = @business_id, age_restriction = @age_restriction, deposit = @deposit, price = @price WHERE (policy_id = @id);";
        private readonly string DELETE = "DELETE FROM policies WHERE (policy_id = @id);";
        private readonly string READ = "SELECT * FROM policies;";
        private readonly string READBUSINESSPOLICIES = "SELECT * FROM policies WHERE policy_business_id = @business_id;";
        private readonly string FINDBYID = "SELECT * FROM policies WHERE policy_id = @ID;";

        public bool insert(Policy item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(INSERT, this.getConnection());
                cmd.Parameters.AddWithValue("@policy_name", item.policy_name);
                cmd.Parameters.AddWithValue("@policy_business_id", item.policy_business_id);
                cmd.Parameters.AddWithValue("@age_restriction", item.age_restriction);
                cmd.Parameters.AddWithValue("@deposit", item.deposit);
                cmd.Parameters.AddWithValue("@price", item.price);
                cmd.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            } finally
            {
                this.disconnect();
            }
            return success;
        }

        public bool update(Policy item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(UPDATE, this.getConnection());
                cmd.Parameters.AddWithValue("@policy_name", item.policy_name);
                cmd.Parameters.AddWithValue("@business_id", item.policy_business_id);
                cmd.Parameters.AddWithValue("@age_restriction", item.age_restriction);
                cmd.Parameters.AddWithValue("@deposit", item.deposit);
                cmd.Parameters.AddWithValue("@price", item.price);
                cmd.Parameters.AddWithValue("@id", item.policy_id);
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

        public bool delete(Policy item)
        {
            bool success = false;
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(DELETE, this.getConnection());
                cmd.Parameters.AddWithValue("@id", item.policy_id);
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

        public List<Policy> read()
        {
            List<Policy> policies = new List<Policy>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READ, this.getConnection());
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Policy policy = new Policy();
                        policy.policy_id = rdr.GetInt32("policy_id");
                        policy.policy_name = rdr.GetString("policy_name");
                        policy.age_restriction = rdr.GetInt32("age_restriction");
                        policy.deposit = rdr.GetDouble("deposit");
                        policy.price = rdr.GetDouble("price");
                        policies.Add(policy);
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

            return policies;
        }

        public List<Policy> businessPolicies(int business_id)
        {
            List<Policy> policies = new List<Policy>();
            MySqlCommand cmd;

            try
            {
                cmd = new MySqlCommand(READBUSINESSPOLICIES, this.getConnection());
                cmd.Parameters.AddWithValue("@business_id", business_id);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        Policy policy = new Policy();
                        policy.policy_id = rdr.GetInt32("policy_id");
                        policy.policy_name = rdr.GetString("policy_name");
                        policy.age_restriction = rdr.GetInt32("age_restriction");
                        policy.deposit = rdr.GetDouble("deposit");
                        policy.price = rdr.GetDouble("price");
                        policies.Add(policy);
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

            return policies;
        }

        public Policy findById(int id)
        {
            Policy policy = new Policy();
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
                        policy.policy_id = rdr.GetInt32("policy_id");
                        policy.policy_name = rdr.GetString("policy_name");
                        policy.age_restriction = rdr.GetInt32("age_restriction");
                        policy.deposit = rdr.GetDouble("deposit");
                        policy.price = rdr.GetDouble("price");
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

            return policy;
        }
    }
}

