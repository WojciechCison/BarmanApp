"""
This script runs the FlaskAPI application using a development server.
"""

from os import environ
from FlaskAPI import app

if __name__ == '__main__':
    HOST = 'localhost'
    PORT = 5555
    app.run(HOST, PORT)
