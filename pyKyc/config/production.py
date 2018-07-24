from config.default import Config

class ProductionConfig(Config):
    """Production configuration
    
    Arguments:
        Config {obj} -- Default configuraiton
    """
    DEBUG = False
