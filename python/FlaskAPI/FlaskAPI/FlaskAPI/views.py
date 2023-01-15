"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, jsonify, request, json
from FlaskAPI import app
import requests


@app.route('/')
def home():
    """Renders the home page."""
    return 'home'

@app.route('/users/login', methods=["POST", "GET"])
def login():
    """Renders the login page."""
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/users/Login', 
            json=json.loads(request.data),
            )
        resp = r.status_code
        resp_json = r.json()
        token = resp_json['token']['token']
        if resp >= 200 and resp < 300:
        #return redirect(url_for())
           return 'logowanie pomyslne '+str(resp)
           #Przekierowanie na stronê z koktailami
           #return redirect(url_for("coctails"))
        else:
           return 'logowanie dupa '+str(resp)
    else:
        return 'register page'

@app.route('/users/register', methods=["POST", "GET"])
def register():
    """Renders the register page."""
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/users/Register', 
            json=json.loads(request.data),
            )
        resp = r.status_code
        if resp >= 200 and resp < 300:
        #return redirect(url_for())
           return 'rejestracja pomyslna '+str(resp)
           #Przekierowanie na stronê z logowaniem
           #return redirect(url_for("login"))
        else:
           return 'rejestracja dupa '+str(resp)
    else:
        return 'register page'
    
@app.route('/coctails/<token>', methods=["POST", "GET"])
def coctails():
    """Renders the contact page."""
    if request.method == "POST":
        return 'add coctail'
    else:   
        koktaile = []
        return jsonify({'koktaile' : koktaile})

@app.route('/coctails/<id>')
def delete_coctail():
    """Renders the about page."""
    return 'delete coctail'

@app.route('/ingridients/<token>', methods=["POST", "GET"])
def ingridients():
    """Renders the contact page."""
    if request.method == "POST":
        return 'add ingridients'
    else:   
        koktaile = []
        return jsonify({'koktaile' : koktaile})

@app.route('/ingridients/<id>')
def delete_ingridient():
    """Renders the about page."""
    return 'delete ingridient'


