from flask_cors import CORS
from flask import Flask

app = Flask(__name__)
CORS(app, origins=['http://localhost:3000'])
import FlaskAPI.views