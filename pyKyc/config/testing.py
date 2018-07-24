from config.default import Config

class TestingConfig(Config):
    """Testing configuration
    
    Arguments:
        Config {obj} -- Default configuration
    """
    TESTING = True
    DEBUG = True
    ENV = 'testing'
