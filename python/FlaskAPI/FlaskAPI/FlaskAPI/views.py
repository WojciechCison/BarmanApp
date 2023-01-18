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
        #r.headers['Access-Control-Allow-Origin'] = 'http://localhost:3000'
        resp_json = r.json()
        token = resp_json['token']['token']
        if resp >= 200 and resp < 300:
           return r
        else:
           return 'logowanie dupa '+str(resp)
    else:
        return 'login page'

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
           return r
        else:
           return 'rejestracja dupa '+str(resp)
    else:
        return 'register page'
    
@app.route('/coctails/<token>', methods=["POST", "GET"])
def coctails(token):
    """Renders the contact page."""
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/coctails/<token>', 
            json=json.loads(request.data),
            )
        resp = r.status_code
        #r.headers['Access-Control-Allow-Origin'] = 'http://localhost:3000'
        resp_json = r.json()
        if resp >= 200 and resp < 300:
           return 'logowanie pomyslne '+str(resp)
        else:
           return 'logowanie dupa '+str(resp)
    else:   
        r_coctails = requests.get(
            'http://localhost:5233/coctails/'+str(token)
            )

        r_ingridients = requests.get(
            'http://localhost:5233/ingridients/'+str(token)
            )
        #r_test = json.loads('[{"id": 1,"name": "Mamacita","ingridients": [{"id":1, "value":50},{"id":2, "value":50}]},{"id": 2,"name": "Wda z wd","ingridients": [{"id":1, "value": 100}]}]')
        #r_test2 = json.loads('[{"id": 1,"name": "Wdka","unit": "ml","coctailIngridients": [],"storagedIngridientEntity": null},{"id": 2,"name": "Sok malinowy","unit": "ml","coctailIngridients": [],"storagedIngridientEntity": null}]')
        r_coctails_json = r_coctails.json()        
        r_ingridients_json = r_ingridients.json()
        for i in r_coctails_json:
            for j in i['coctailIngridients']:
                ing_id = j['ingridientId']
                ing_data = next((item for item in r_ingridients_json if item['id'] == ing_id), None)
                j.update(ing_data)
                j.pop('ingridientId', None)
                j.pop('coctailIngridients', None)
                j.pop('coctailId', None)
                j.pop('storagedIngridientEntity', None)
                #storagedIngridientEntity

        return r_coctails_json

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


