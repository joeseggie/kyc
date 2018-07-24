"""Individual subscriber tasks handler
"""

import pyodbc
from subscriber import app

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
        return pyodbc.connect('DRIVER={ODBC DRIVER 17 for SQL Server}' + 
        ';SERVER=' + app.config['DATABASE_CONFIG']['HOST'] + 
        ';DATABASE=' + app.config['DATABASE_CONFIG']['DB'] +
        ';UID=' + app.config['DATABASE_CONFIG']['USER'] + 
        ';PWD=' + app.config['DATABASE_CONFIG']['PASSWORD'])

    def register_subscriber(self, subscriber):
        """Registers subscriber
        
        Arguments:
            subscriber {obj} -- Subscriber details for registration
        """
        with self.cnxn as connection:
            cursor = connection.cursor()
            cursor = cursor.execute('SELECT @@version;')
            row = cursor.fetchone()

            while row:
                sqlserver_version = row[0]
                row = cursor.fetchone()
        return sqlserver_version
    
    def face_upload(self, face_image):
        """Subscriber face image upload.
        
        Arguments:
            face_image {str} -- Base64 string of the subscribers face image
        """
        pass
