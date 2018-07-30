"""Application entry module
"""


from subscriber import app as application


def main():
    """Module main entry function
    """
    application.run(host='0.0.0.0')


if __name__ == '__main__':
    main()
