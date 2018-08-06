"""Individual routes module
"""


from flask import Blueprint, jsonify, request
from subscriber.api.individual.handlers import IndividualSubscriberHandler


INDIVIDUAL_BLUEPRINT = Blueprint('individual', __name__)


@INDIVIDUAL_BLUEPRINT.route('/register', methods=['POST'])
def register():
    """Register individual subscriber
    """
    request_data = request.get_json()
    handler = IndividualSubscriberHandler()
    task_result = handler.register_subscriber(request_data)
    if task_result['success']:
        http_status_code = 201
    else:
        http_status_code = 400
    return jsonify(task_result), http_status_code


@INDIVIDUAL_BLUEPRINT.route('/face', methods=['POST'])
def face_upload():
    """Route to upload individual subscriber's face image.
    """
    request_data = request.get_json()
    handler = IndividualSubscriberHandler()
    task_result = handler.face_upload(request_data['Msisdn'], request_data['FaceImg'])
    if task_result['success']:
        http_status_code = 201
    else:
        http_status_code = 400
    return jsonify(task_result), http_status_code


@INDIVIDUAL_BLUEPRINT.route('/idfront', methods=['POST'])
def idfront_upload():
    """Route to upload individual subscriber's ID front image.
    """
    request_data = request.get_json()
    handler = IndividualSubscriberHandler()
    task_result = handler.idfront_upload(request_data['Msisdn'], request_data['IdFrontimg'])
    if task_result['success']:
        http_status_code = 201
    else:
        http_status_code = 400
    return jsonify(task_result), http_status_code


@INDIVIDUAL_BLUEPRINT.route('/validate/<msisdn>')
def validate(msisdn):
    """Validates MSISDN if it's a UTL number and not already registered.

    Arguments:
        msisdn {str} -- UTL network number.
    """
    pass


@INDIVIDUAL_BLUEPRINT.route('/get/<msisdn>')
def get(msisdn):
    """Queries for the MSISDN from the subscribers database.

    Arguments:
        msisdn {str} -- UTL network number
    """
    return jsonify({'MSISDN': msisdn})


@INDIVIDUAL_BLUEPRINT.route('/search/<keyword>')
def search(keyword):
    """Searches database using the keyword.

    Arguments:
        keyword {string} -- Search term or word used to search database.
    """
    pass
