"""Individual subscriber tasks handler
"""


import pyodbc
from subscriber import app
from datetime import datetime


class IndividualSubscriberHandler():
    """Object to handle all operations for individual subscribers
    """

    def __init__(self):
        """Constructor
        """
        self.cnxn = self.db_connection()


    def db_connection(self):
        """Gets connection to the database

        Returns: pyodbc connect object
        """
        return pyodbc.connect(
            r'DRIVER={ODBC DRIVER 17 for SQL Server};'
            r'SERVER=' + app.config['DATABASE_CONFIG']['HOST'] +';'
            r'DATABASE=' + app.config['DATABASE_CONFIG']['DB'] + ';'
            r'UID=' + app.config['DATABASE_CONFIG']['USER'] + ';'
            r'PWD=' + app.config['DATABASE_CONFIG']['PASSWORD'] + ';'
        )


    def register_subscriber(self, subscriber : dict):
        """Registers subscriber
        
        Parameters
        ----------
        subscriber : dict
            Subscriber details for registration.
        """
        try:
            with self.cnxn as connection:
                query = 'INSERT INTO SimAppMain (Surname, GivenName, Gender, DateOfBirth, IdentificationNumber, Msisdn, IdentificationType, Village, District, FaceImg, IdFrontimg, IdBackimg, AgentMsisdn, RegistrationDate, RegistrationTime, Mode, Verified, VerificationRequest, NiraValidation, OtherNames, IdCardNumber, VisaExpiry) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);'

                transaction_logged = datetime.now()
                registration_date = transaction_logged
                regitration_time = transaction_logged.time()

                agent_msisdn = subscriber['AgentMsisdn'].upper()
                msisdn = subscriber['Msisdn'].upper()
                date_of_birth = datetime.strptime(subscriber['DateOfBirth'], '%Y-%m-%d')
                id_card_number = subscriber['IdCardNumber'].upper()
                given_name = subscriber['GivenName'].upper()
                identification_number = subscriber['IdentificationNumber'].upper()
                identification_type = subscriber['IdentificationType'].upper()
                mode = subscriber['Mode'].upper()
                verified = subscriber['Verified']
                verification_request = 'RETURNED'
                other_names = subscriber['OtherNames'].upper()
                surname = subscriber['Surname'].upper()
                face_image = None
                id_front_image = None
                id_back_image = None
                village = subscriber['Village'].upper()
                district = subscriber['District'].upper()
                gender = subscriber['Gender'].upper()
                nira_validation = None
                visa_expiry = None

                cursor = connection.cursor()
                cursor.execute(
                    query,
                    surname,
                    given_name,
                    gender,
                    date_of_birth,
                    identification_number,
                    msisdn,
                    identification_type,
                    village,
                    district,
                    face_image,
                    id_front_image,
                    id_back_image,
                    agent_msisdn,
                    registration_date,
                    regitration_time,
                    mode,
                    verified,
                    verification_request,
                    nira_validation,
                    other_names,
                    id_card_number,
                    visa_expiry
                )
                cursor.commit()

                operationResult = {'success': True, 'taskResult': msisdn }
        except pyodbc.IntegrityError:
            operationResult = { 'success': False, 'taskResult': f'Subscriber MSISDN {subscriber["Msisdn"]} already registered.' }
        except pyodbc.DatabaseError:
            operationResult = { 'success': False, 'taskResult': f'Subscriber registration for MSISDN {subscriber["Msisdn"]} failed.' }
        
        return operationResult


    def face_upload(self, msisdn : str, face_image : str):
        """Subscriber face image upload.
        
        Parameters
        ----------
        msisdn : str
            MSISDN of subscriber whose image is being uploaded.

        face_image : str
            Base64 string of the subscribers face image
        """
        try:
            with self.cnxn as connection:
                query = 'UPDATE SimAppMain SET FaceImg = ? WHERE Msisdn = ?;'
                cursor = connection.cursor()
                cursor.execute(
                    query,
                    face_image,
                    msisdn)
                cursor.commit()

                operationResult = { 'success': True, 'taskResult': msisdn }
        except pyodbc.DatabaseError:
            operationResult = { 'success': False, 'taskResult': f'Subscriber registration update for MSISDN {msisdn} failed.' }
        
        return operationResult
    

    def idfront_upload(self, id_front : str):
        """Upload ID front image to the registration database.

        Parameters
        ----------
        id_front : str
            Base64 string of the ID front image
        """
        pass
    

    def idback_upload(self, id_back : str):
        """Upload ID back image to the registration database.

        Parameters
        ----------
        id_back : str
            Base64 string of the ID back image
        """
        pass
    
    def update_registration(self, subscriber : dict):
        """Update subscriber registration.

        Parameters
        ----------
        subscriber : dict
            Subscriber details to be updated.
        """
        try:
            with self.cnxn as connection:
                query = 'UPDATE SimAppMain SET Surname = ?, GivenName = ?, Gender = ?, DateOfBirth = ?, Village = ?, District = ?, OtherNames = ?, IdCardNumber = ? WHERE Msisdn = ?;'

                msisdn = subscriber['Msisdn']
                date_of_birth = datetime.strptime(subscriber['DateOfBirth'], '%Y-%m-%d')
                id_card_number = subscriber['IdCardNumber'].upper()
                given_name = subscriber['GivenName'].upper()
                other_names = subscriber['OtherNames'].upper()
                surname = subscriber['Surname'].upper()
                village = subscriber['Village'].upper()
                district = subscriber['District'].upper()
                gender = subscriber['Gender'].upper()

                cursor = connection.cursor()
                cursor.execute(
                    query,
                    surname,
                    given_name,
                    gender,
                    date_of_birth,
                    village,
                    district,
                    other_names,
                    id_card_number,
                    msisdn)
                cursor.commit()

                operationResult = {'success': True, 'taskResult': msisdn }
        except pyodbc.DatabaseError:
            operationResult = { 'success': False, 'taskResult': f'Subscriber registration update for MSISDN {subscriber["Msisdn"]} failed.' }
        
        return operationResult
