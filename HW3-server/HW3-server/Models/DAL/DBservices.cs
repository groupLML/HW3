using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HW3_server.Models;
using System.Xml.Linq;


/// DBServices is a class created by me to provides some DataBase Services
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //constuctor
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //********** Flat **********//

    //--------------------------------------------------------------------------------------------------
    // This method inserts a flat to the flats table 
    //--------------------------------------------------------------------------------------------------
    public int InsertFlat(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertFlatCommandSP("spInsertFlat", con, flat);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the InsertFlat SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertFlatCommandSP(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        cmd.Parameters.AddWithValue("@city", flat.City);
        cmd.Parameters.AddWithValue("@address", flat.Address);
        cmd.Parameters.AddWithValue("@price", flat.Price);
        cmd.Parameters.AddWithValue("@numberOfRooms", flat.NumberOfRooms);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method Read flats from the flat table
    //--------------------------------------------------------------------------------------------------
    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadFlatsCommandSP("spReadFlats", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Flat> list = new List<Flat>();

            while (dataReader.Read())
            {
                Flat flat = new Flat();
                flat.Id = Convert.ToInt32(dataReader["Id"]);
                flat.City = dataReader["City"].ToString();
                flat.Address = dataReader["Address"].ToString();
                flat.Price = Convert.ToDouble(dataReader["Price"]);
                flat.NumberOfRooms = Convert.ToInt32(dataReader["NumberOfRooms"]);
                list.Add(flat);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the ReadFlat SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadFlatsCommandSP(String spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        return cmd;
    }




    //********** Vacation **********//

    //--------------------------------------------------------------------------------------------------
    // This method inserts a vacation to the vacations table 
    //--------------------------------------------------------------------------------------------------
    public int InsertVacation(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertVacationCommandSP("spInsertVacation", con, vacation);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the InsertVacation SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertVacationCommandSP(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        cmd.Parameters.AddWithValue("@userId", vacation.UserId);
        cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);
        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method Read orders from the vacation table
    //--------------------------------------------------------------------------------------------------
    public List<Vacation> ReadOrders()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadOrdersCommandSP("spReadVacations", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Vacation> list = new List<Vacation>();

            while (dataReader.Read())
            {
                Vacation vacation = new Vacation();
                vacation.Id = Convert.ToInt32(dataReader["Id"]);
                vacation.UserId = dataReader["UserId"].ToString();
                vacation.FlatId = Convert.ToInt32(dataReader["FlatId"]);
                vacation.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                vacation.EndDate = Convert.ToDateTime(dataReader["EndDate"]);
                list.Add(vacation);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the ReadOrders SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadOrdersCommandSP(String spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        return cmd;
    }





    //********** User **********//

    //--------------------------------------------------------------------------------------------------
    // This method Update a user in the userss table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUpdateUserCommandSP("spUpdateUser", con, user);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the UpdateUser SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateUpdateUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method Read users from the user table
    //--------------------------------------------------------------------------------------------------
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadUsersCommandSP("spReadUsers", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<User> list = new List<User>();

            while (dataReader.Read())
            {
                User user = new User();
                user.FirstName = dataReader["firstName"].ToString();
                user.LastName = dataReader["lastName"].ToString();
                user.Email = dataReader["email"].ToString();
                user.Password = dataReader["password"].ToString();
                list.Add(user);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Create the ReadUser SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateReadUsersCommandSP(String spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a user to the user table 
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertUserCommandSP("spInsertUser", con, user);

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //---------------------------------------------------------------------------------
    // Create the InsertUser SqlCommand ---STORED PROCEDURE
    //---------------------------------------------------------------------------------
    private SqlCommand CreateInsertUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }
}
