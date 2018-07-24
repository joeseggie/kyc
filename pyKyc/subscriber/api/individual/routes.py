from flask import Blueprint
from handlers import IndividualSubscriberHandler

individual_blueprint = Blueprint('individual', __name__)

@individual_blueprint.route('/register', methods=['POST'])
def regiser():
    """Register individual subscriber
    """
    pass

@individual_blueprint.route('/update', methods=['POST'])
def update():
    """Updates individual subscriber's registered information
    """
    pass

@individual_blueprint.route('/validate/<msisdn>')
def validate(msisdn):
    """Validates MSISDN if it's a UTL number and not already registered.
    
    Arguments:
        msisdn {str} -- UTL network number.
    """
    pass

@individual_blueprint.route('/get/<msisdn>')
def get(msisdn):
    """Queries for the MSISDN from the subscribers database.
    
    Arguments:
        msisdn {str} -- UTL network number
    """
    pass

@individual_blueprint.route('/search/<keyword>')
def search(keyword):
    """Searches database using the keyword.
    
    Arguments:
        keyword {string} -- Search term or word used to search database.
    """
    pass
