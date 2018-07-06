using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Models;

namespace UgandaTelecom.Kyc.Core.Services
{
    /// <summary>
    /// Subscriber handling service.
    /// </summary>
    public class SubscriberService : ISubscriberService
    {
        private readonly ISqlDatabaseServer _sqlDatabaseServer;

        public SubscriberService(ISqlDatabaseServer sqlDatabaseServer)
        {
            _sqlDatabaseServer = sqlDatabaseServer;
        }

        /// <summary>
        /// Archive subscriber service.
        /// </summary>
        /// <param name="msisdn">Subscriber MSISDN</param>
        /// <returns>Result from operation indicating whether it was a success.</returns>
        public Task<OperationResult> ArchiveAsync(string msisdn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get subscriber given their MSISDN.
        /// </summary>
        /// <param name="msisdn">Subscriber MSISDN.</param>
        /// <returns>Subscriber details.</returns>
        public Task<Subscriber> GetAsync(string msisdn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register subscriber.
        /// </summary>
        /// <param name="subscriber">Subscriber to be registered.</param>
        /// <returns>Operation result indicating if it was a success.</returns>
        public async Task<OperationResult> RegisterAsync(Subscriber subscriber)
        {
            using (SqlConnection db = (SqlConnection)_sqlDatabaseServer.Connection)
            {
                subscriber.RegistrationDate = DateTime.Now;
                subscriber.RegistrationTime = new TimeSpan(subscriber.RegistrationDate.Hour, subscriber.RegistrationDate.Minute, subscriber.RegistrationDate.Second);
                await db.OpenAsync();

                var registrationInsetQuery = $@"INSERT INTO SimAppMain (Surname, GivenName, Gender, DateOfBirth, IdentificationNumber, Msisdn, IdentificationType, Village, District, FaceImg, IdFrontimg, IdBackimg, AgentMsisdn, RegistrationDate, RegistrationTime, Mode, Verified, VerificationRequest, NiraValidation, OtherNames, IdCardNumber, VisaExpiry) VALUES (@Surname, @GivenName, @Gender, @DateOfBirth, @IdentificationNumber, @Msisdn, @IdentificationType, @Village, @District, @FaceImg, @IdFrontimg, @IdBackimg, @AgentMsisdn, @RegistrationDate, @RegistrationTime, @Mode, @Verified, @VerificationRequest, @NiraValidation, @OtherNames, @IdCardNumber, @VisaExpiry);";

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.CommandText = registrationInsetQuery;

                        command.Parameters.AddWithValue("@Surname", subscriber.Surname);
                        command.Parameters.AddWithValue("@GivenName", subscriber.GivenName);
                        if (subscriber.Gender == null)
                            command.Parameters.AddWithValue("@Gender", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Gender", subscriber.Gender);
                        if (subscriber.DateOfBirth == null)
                            command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@DateOfBirth", subscriber.DateOfBirth);
                        command.Parameters.AddWithValue("@IdentificationNumber", subscriber.IdentificationNumber);
                        command.Parameters.AddWithValue("@Msisdn", subscriber.Msisdn);
                        command.Parameters.AddWithValue("@IdentificationType", subscriber.IdentificationType);
                        if (subscriber.Village == null)
                            command.Parameters.AddWithValue("@Village", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Village", subscriber.Village);
                        if (subscriber.District == null)
                            command.Parameters.AddWithValue("@District", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@District", subscriber.District);
                        if (subscriber.FaceImg == null)
                            command.Parameters.AddWithValue("@FaceImg", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@FaceImg", subscriber.FaceImg);
                        if (subscriber.IdFrontimg == null)
                            command.Parameters.AddWithValue("@IdFrontimg", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdFrontimg", subscriber.IdFrontimg);
                        if (subscriber.IdBackimg == null)
                            command.Parameters.AddWithValue("@IdBackimg", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdBackimg", subscriber.IdBackimg);
                        if (subscriber.AgentMsisdn == null)
                            command.Parameters.AddWithValue("@AgentMsisdn", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@AgentMsisdn", subscriber.AgentMsisdn);
                        command.Parameters.AddWithValue("@RegistrationDate", subscriber.RegistrationDate);
                        command.Parameters.AddWithValue("@RegistrationTime", subscriber.RegistrationTime);
                        command.Parameters.AddWithValue("@Mode", subscriber.Mode);
                        command.Parameters.AddWithValue("@Verified", subscriber.Verified); ;
                        command.Parameters.AddWithValue("@VerificationRequest", subscriber.VerificationRequest);
                        if (subscriber.NiraValidation == null)
                            command.Parameters.AddWithValue("@NiraValidation", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@NiraValidation", subscriber.NiraValidation);
                        if (subscriber.OtherNames == null)
                            command.Parameters.AddWithValue("@OtherNames", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@OtherNames", subscriber.OtherNames);
                        if (subscriber.IdCardNumber == null)
                            command.Parameters.AddWithValue("@IdCarNumber", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdCardNumber", subscriber.IdCardNumber);
                        if (subscriber.VisaExpiry == null)
                            command.Parameters.AddWithValue("@VisaExpiry", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@VisaExpiry", subscriber.VisaExpiry);

                        await command.ExecuteNonQueryAsync();

                        transaction.Commit();

                        return new OperationResult { Success = true, Message = subscriber.Msisdn };
                    }
                    catch (SqlException) { transaction.Rollback(); throw; } 
                }
            }
        }

        /// <summary>
        /// Search subscribers registrations given a keyword(s)
        /// </summary>
        /// <param name="keyword">Search keyword(s)</param>
        /// <returns>List of subscriber details that match the search keyword.</returns>
        public Task<IEnumerable<Subscriber>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update subscriber details.
        /// </summary>
        /// <param name="subscriber">Subscriber details update.</param>
        /// <returns>Operation result indicating if it was a success or not.</returns>
        public Task<OperationResult> UpdateAsync(Subscriber subscriber)
        {
            throw new NotImplementedException();
        }
    }
}
