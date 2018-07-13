using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UgandaTelecom.Kyc.Core.Common.Extensions;
using UgandaTelecom.Kyc.Core.Common.OperationResults;
using UgandaTelecom.Kyc.Core.Data;
using UgandaTelecom.Kyc.Core.Models;

namespace UgandaTelecom.Kyc.Core.Services.Subscribers
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
        public Task<TaskOperationResult> ArchiveAsync(string msisdn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get subscriber given their MSISDN.
        /// </summary>
        /// <param name="msisdn">Subscriber MSISDN.</param>
        /// <returns>Subscriber details.</returns>
        public async Task<Subscriber> GetAsync(string msisdn)
        {
            using (var db = (SqlConnection)_sqlDatabaseServer.Connection)
            {
                await db.OpenAsync();
                var query = "SELECT TOP 1 SubscriberID, Surname, GivenName, Gender, DateOfBirth, IdentificationNumber, Msisdn, IdentificationType, Village, District, FaceImg, IdFrontimg, IdBackimg, AgentMsisdn, RegistrationDate, RegistrationTime, Mode, Verified, VerificationRequest, NiraValidation, OtherNames, IdCardNumber, VisaExpiry FROM SimAppMain WHERE Msisdn = @Msisdn;";

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(SetCommandParameter("@Msisdn", msisdn));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var subscriber = new Subscriber();

                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    subscriber.SubscriberId = reader.SafeGetGuid(0, null);
                                    subscriber.Surname = reader.SafeGetString(1, string.Empty);
                                    subscriber.GivenName = reader.SafeGetString(2, string.Empty);
                                    subscriber.Gender = reader.SafeGetString(3, string.Empty);
                                    subscriber.DateOfBirth = reader.SafeGetDateTime(4, null);
                                    subscriber.IdentificationNumber = reader.SafeGetString(5, string.Empty);
                                    subscriber.Msisdn = reader.SafeGetString(6, string.Empty);
                                    subscriber.IdentificationType = reader.SafeGetString(7, string.Empty);
                                    subscriber.Village = reader.SafeGetString(8, string.Empty);
                                    subscriber.District = reader.SafeGetString(9, string.Empty);
                                    subscriber.FaceImg = reader.SafeGetString(10, string.Empty);
                                    subscriber.IdFrontimg = reader.SafeGetString(11, string.Empty);
                                    subscriber.IdBackimg = reader.SafeGetString(12, string.Empty);
                                    subscriber.AgentMsisdn = reader.SafeGetString(13, string.Empty);
                                    subscriber.RegistrationDate = reader.SafeGetDateTime(14, null);
                                    subscriber.RegistrationTime = reader.SafeGetTimeSpan(15, null);
                                    subscriber.Mode = reader.SafeGetString(16, string.Empty);
                                    subscriber.Verified = reader.SafeGetBoolean(17, false);
                                    subscriber.VerificationRequest = reader.SafeGetString(18, string.Empty);
                                    subscriber.NiraValidation = reader.SafeGetString(19, string.Empty);
                                    subscriber.OtherNames = reader.SafeGetString(20, string.Empty);
                                    subscriber.IdCardNumber = reader.SafeGetString(21, string.Empty);
                                    subscriber.VisaExpiry = reader.SafeGetDateTime(22, null);
                                }

                                return subscriber;
                            }
                        }

                        return null;
                    }
                    catch (SqlException) { throw; }
                }
            }
        }

        /// <summary>
        /// Register subscriber.
        /// </summary>
        /// <param name="subscriber">Subscriber to be registered.</param>
        /// <returns>Operation result indicating if it was a success.</returns>
        public async Task<TaskOperationResult> RegisterAsync(Subscriber subscriber)
        {
            using (SqlConnection db = (SqlConnection)_sqlDatabaseServer.Connection)
            {
                var registrationDateTime = DateTime.Now;
                subscriber.RegistrationDate = registrationDateTime;
                subscriber.RegistrationTime = new TimeSpan(registrationDateTime.Hour, registrationDateTime.Minute, registrationDateTime.Second);
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

                        command.Parameters.AddWithValue("@Surname", subscriber.Surname.ToUpper());
                        command.Parameters.AddWithValue("@GivenName", subscriber.GivenName.ToUpper());
                        if (subscriber.Gender == null)
                            command.Parameters.AddWithValue("@Gender", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Gender", subscriber.Gender.ToUpper());
                        if (subscriber.DateOfBirth == null)
                            command.Parameters.AddWithValue("@DateOfBirth", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@DateOfBirth", subscriber.DateOfBirth);
                        command.Parameters.AddWithValue("@IdentificationNumber", subscriber.IdentificationNumber.ToUpper());
                        command.Parameters.AddWithValue("@Msisdn", subscriber.Msisdn);
                        command.Parameters.AddWithValue("@IdentificationType", subscriber.IdentificationType.ToUpper());
                        if (subscriber.Village == null)
                            command.Parameters.AddWithValue("@Village", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Village", subscriber.Village.ToUpper());
                        if (subscriber.District == null)
                            command.Parameters.AddWithValue("@District", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@District", subscriber.District.ToUpper());
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
                        command.Parameters.AddWithValue("@Mode", subscriber.Mode.ToUpper());
                        command.Parameters.AddWithValue("@Verified", subscriber.Verified); ;
                        command.Parameters.AddWithValue("@VerificationRequest", subscriber.VerificationRequest.ToUpper());
                        if (subscriber.NiraValidation == null)
                            command.Parameters.AddWithValue("@NiraValidation", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@NiraValidation", subscriber.NiraValidation);
                        if (subscriber.OtherNames == null)
                            command.Parameters.AddWithValue("@OtherNames", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@OtherNames", subscriber.OtherNames.ToUpper());
                        if (subscriber.IdCardNumber == null)
                            command.Parameters.AddWithValue("@IdCardNumber", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@IdCardNumber", subscriber.IdCardNumber.ToUpper());
                        if (subscriber.VisaExpiry == null)
                            command.Parameters.AddWithValue("@VisaExpiry", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@VisaExpiry", subscriber.VisaExpiry);

                        await command.ExecuteNonQueryAsync();

                        transaction.Commit();

                        return new TaskOperationResult { Success = true, TaskResult = subscriber.Msisdn };
                    }
                    catch (SqlException error) when ( error.Number == 2627 ) { transaction.Rollback(); return new TaskOperationResult { Success = false, TaskResult = "Can not insert duplicate record. Subscriber is already registered" }; }
                    catch (SqlException error) when (error.Number == 2601) { transaction.Rollback(); return new TaskOperationResult { Success = false, TaskResult = "Can not insert duplicate record. Subscriber is already registered" }; }
                    catch (SqlException error) when (error.Number == 547) { transaction.Rollback(); return new TaskOperationResult { Success = false, TaskResult = "Can not insert duplicate record. Subscriber is already registered" }; }
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
        public async Task<TaskOperationResult> UpdateAsync(Subscriber subscriber)
        {
            using (var db = (SqlConnection)_sqlDatabaseServer.Connection)
            {
                await db.OpenAsync();
                var updateQuery = $"Update SimAppMain SET Gender = @Gender, Village = @Village, District = @District, FaceImg = @FaceImg, IdFrontimg = @IdFrontimg, IdBackimg = @IdBackimg, NiraValidation = @NiraValidation, VisaExpiry = @VisaExpiry, IdCardNumber = @IdCardNumber WHERE Msisdn = @Msisdn";

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var command = db.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = updateQuery;
                        command.CommandType = CommandType.Text;

                        command.Parameters.Add(SetCommandParameter("@Gender", subscriber.Gender.ToUpper()));
                        command.Parameters.Add(SetCommandParameter("@Village", subscriber.Village.ToUpper()));
                        command.Parameters.Add(SetCommandParameter("@District", subscriber.District.ToUpper()));
                        command.Parameters.Add(SetCommandParameter("@FaceImg", subscriber.FaceImg));
                        command.Parameters.Add(SetCommandParameter("@IdFrontimg", subscriber.IdFrontimg));
                        command.Parameters.Add(SetCommandParameter("@IdBackimg", subscriber.IdBackimg));
                        command.Parameters.Add(SetCommandParameter("@NiraValidation", subscriber.NiraValidation));
                        command.Parameters.Add(SetCommandParameter("@VisaExpiry", subscriber.VisaExpiry));
                        command.Parameters.Add(SetCommandParameter("@Msisdn", subscriber.Msisdn));
                        command.Parameters.Add(SetCommandParameter("@IdCardNumber", subscriber.IdCardNumber));

                        await command.ExecuteNonQueryAsync();

                        transaction.Commit();

                        return new TaskOperationResult { Success = true, TaskResult = subscriber.Msisdn };
                    }
                    catch (SqlException) { transaction.Rollback(); throw; }
                }
            }
        }

        /// <summary>
        /// Setup the parameters for a command.
        /// </summary>
        /// <param name="parameterName">Name of parameter</param>
        /// <param name="value">Parameter value.</param>
        /// <returns>SqlParameter</returns>
        public SqlParameter SetCommandParameter(string parameterName, object value)
        {
            SqlParameter returnParameter;

            if (value == null)
                returnParameter = new SqlParameter(parameterName, DBNull.Value);
            else
                returnParameter = new SqlParameter(parameterName, value);

            return returnParameter;
        }

        /// <summary>
        /// Checks if MSISDN is a valid UTL number
        /// </summary>
        /// <param name="msisdn">MSISDN to validate.</param>
        /// <returns>True if MSISDN is a valid UTL number</returns>
        public async Task<TaskOperationResult> ValidateMsidnAsync(string msisdn)
        {
            return await Task.FromResult(new TaskOperationResult { Success = Regex.IsMatch(msisdn, "^(71|4[0-9])[0-9]{7}$"), TaskResult = msisdn });
        }

        /// <summary>
        /// Checks if msisdn is registered.
        /// </summary>
        /// <param name="msisdn">Msisdn to check</param>
        /// <returns>True if MSISDN is registered.</returns>
        public async Task<TaskOperationResult> IsRegisteredAsync(string msisdn)
        {
            if ((await GetAsync(msisdn)) == null)
                return new TaskOperationResult { Success = false, TaskResult = msisdn };
            else
                return
                    new TaskOperationResult { Success = true, TaskResult = msisdn };
        }
    }
}
