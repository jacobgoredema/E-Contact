using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContact.Data.Models
{
    public class ContactClass
    {
        public int ContactId { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string Gender { get; set; }

        static string myConnectionString = ConfigurationManager.ConnectionStrings["EcontactEntities"].ConnectionString;

        //select data from database
        public DataTable Select()
        {
            SqlConnection connection = new SqlConnection(myConnectionString);
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "SELECT * from Contact";
                SqlCommand command = new SqlCommand(sql,connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(dataTable);


            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        //inserting data into Database
        public bool Insert(ContactClass contactClass)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(myConnectionString);
            try
            {
                string sql = "INSERT INTO contact(Firstname,Lastname,ContactNumber,Address,Gender) VALUES(@Firstname,@Lastname,@ContactNumber,@Address,@Gender)";
                SqlCommand command = new SqlCommand(sql,connection);
                command.Parameters.AddWithValue("@Firstname", contactClass.Firstname);
                command.Parameters.AddWithValue("@Lastname", contactClass.Lastname);
                command.Parameters.AddWithValue("@ContactNumber", contactClass.Phone);
                command.Parameters.AddWithValue("@Address", contactClass.Address);
                command.Parameters.AddWithValue("@Gender", contactClass.Gender);

                connection.Open();
                int rows = command.ExecuteNonQuery();

                // if the query uns successfully then the value of rows will be grater than zero
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }

        public bool Update(ContactClass model)
        {
            bool isSuccess = false;

            SqlConnection connection = new SqlConnection(myConnectionString);

            try
            {
                string sql= "UPDATE Contact SET Firstname =@Firstname,Lastname= @Lastname,ContactNumber=@ContactNumber,Address=@Address,Gender=@Gender WHERE ContactId=@ContactId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Firstname", model.Firstname);
                command.Parameters.AddWithValue("@Lastname", model.Lastname);
                command.Parameters.AddWithValue("@ContactNumber", model.Phone);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@ContactId", model.ContactId);

                connection.Open();
                int rows = command.ExecuteNonQuery();
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }

        public bool Delete(ContactClass model)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(myConnectionString);
            try
            {
                string sql = "DELETE FROM Contact WHERE ContactId=@ContactId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ContactId", model.ContactId);

                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }
    }
}
