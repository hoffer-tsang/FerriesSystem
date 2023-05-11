/*==============================================================================
 *
 * Booking logic for customer to book a journey
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Configuration;
using System.Data.SqlClient;
using BusinessClass.Model;

namespace BusinessClass.Customer
{
    /// <summary>
    /// Contains add journey to booking, delete booking journey and adding booking contact details
    /// </summary>
    public class Booking
    {
        private string _ConnectionString = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// Add the journey book to the database
        /// </summary>
        /// <param name="data"> the data to be input into database </param>
        /// <returns> return booking id, if sql command fail return -1 </returns>
        public int AddBooking(InputBooking data)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Booking_Insert", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("UserId", System.Data.SqlDbType.NVarChar, 128).Value = data.UserId;
                cmd.Parameters.Add("BookingReference", System.Data.SqlDbType.NVarChar, 5).Value = data.BookingReference;
                cmd.Parameters.Add("Cars", System.Data.SqlDbType.Int).Value = data.Cars;
                cmd.Parameters.Add("Passengers", System.Data.SqlDbType.Int).Value = data.Passengers;
                cmd.Parameters.Add("Cost", System.Data.SqlDbType.Decimal).Value = data.Cost;
                cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar, 256).Value = data.CompanyName;
                cmd.Parameters.Add("FerryName", System.Data.SqlDbType.NVarChar, 256).Value = data.FerryName;
                cmd.Parameters.Add("DepartureDate", System.Data.SqlDbType.DateTime2).Value = data.DepartureDate;
                cmd.Parameters.Add("DepartureLocation", System.Data.SqlDbType.NVarChar, 256).Value = data.DepartureLocation;
                cmd.Parameters.Add("ArrivalDate", System.Data.SqlDbType.DateTime2).Value = data.ArrivalDate;
                cmd.Parameters.Add("ArrivalLocation", System.Data.SqlDbType.NVarChar, 256).Value = data.ArrivalLocation;
                conn.Open();
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (int)reader["BookingId"];
                        }
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
                return -1;
            }
        }

        /// <summary>
        /// Add the booking contact details to the database
        /// </summary>
        /// <param name="data"> data to be input </param>
        /// <returns> return 1 if success and 0 if fail </returns>
        public int AddBookingContact(InputBookingContact data)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_BookingContact_Insert", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("BookingId", System.Data.SqlDbType.Int).Value = data.BookingId;
                cmd.Parameters.Add("Name", System.Data.SqlDbType.NVarChar, 100).Value = data.Name;
                cmd.Parameters.Add("AddressLine1", System.Data.SqlDbType.NVarChar, 256).Value = data.AddressLine1;
                cmd.Parameters.Add("City", System.Data.SqlDbType.NVarChar, 256).Value = data.City;
                cmd.Parameters.Add("PostalCode", System.Data.SqlDbType.NVarChar, 20).Value = data.PostalCode;
                if(!String.IsNullOrWhiteSpace(data.AddressLine2))
                {
                    cmd.Parameters.Add("AddressLine2", System.Data.SqlDbType.NVarChar, 256).Value = data.AddressLine2;
                }
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    DeleteBookingContact(data.BookingId);
                    return 0;
                }
                return 1;
            }
        }

        /// <summary>
        /// delete the booking journey 
        /// </summary>
        /// <param name="bookingId"> booking id of the booking </param>
        /// <returns> return 1 if success, fail 0 </returns>
        public int DeleteBookingContact(int bookingId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Booking_Delete", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("BookingId", System.Data.SqlDbType.Int).Value = bookingId;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return 0;
                }
                return 1;
            }
        }
    }
}
