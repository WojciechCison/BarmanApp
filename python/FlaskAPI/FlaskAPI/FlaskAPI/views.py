"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template, jsonify, request, json
from FlaskAPI import app
import requests


@app.route('/')
def home():
    return 'home'

@app.route('/users/login', methods=["POST", "GET"])
def login():
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/users/Login', 
            json=json.loads(request.data),
            )
        r_json = r.json()
        return r_json
    else:
        return 'login page'

@app.route('/users/register', methods=["POST", "GET"])
def register():
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/users/Register', 
            json=json.loads(request.data),
            )
        return r.status_code
    else:
        return 'register page'
    
@app.route('/coctails/<token>', methods=["POST", "GET"])
def coctails(token):
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/coctails/'+str(token), 
            json=json.loads(request.data),
            )
        return r
    else:   
        r_coctails = requests.get(
            'http://localhost:5233/coctails/'+str(token)
            )

        r_ingridients = requests.get(
            'http://localhost:5233/ingridients/'+str(token)
            )
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
        return r_coctails_json

@app.route('/coctails/<id>', methods=["DELETE"])
def delete_coctail(id):
    if request.method == "DELETE":
        r = requests.delete(
            'http://localhost:5233/coctails/'+str(id)
            )
        return r.status_code

@app.route('/ingridients/<token>', methods=["POST", "GET"])
def ingridients(token):
    """Renders the contact page."""
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/ingridients/'+str(token), 
            json=json.loads(request.data),
            )
        return r
    else:   
        r_ingridients = requests.get(
            'http://localhost:5233/ingridients/'+str(token)
            )
        #r_coctails_json = r_coctails.json()        
        r_ingridients_json = r_ingridients.json()
        return r_ingridients_json

@app.route('/ingridients/<id>', methods=["DELETE"])
def delete_ingridient(id):
    if request.method == "DELETE":
        r = requests.delete(
            'http://localhost:5233/ingridients/'+str(id)
            )
        return r.status_code

@app.route('/Storage<id>/Add/<dose>', methods=["PUT"])
def add_to_storage(id, dose):
    if request.method == "PUT":
        r = requests.put(
            'http://localhost:5233/Storage'+str(id)+'/Add/'+str(dose),
            json=json.loads(request.data)
            )
        return r.status_code

@app.route('/Storage<id>/Remove/<dose>', methods=["PUT"])
def remove_from_storage(id, dose):
    if request.method == "PUT":
        r = requests.put(
            'http://localhost:5233/Storage'+str(id)+'/Remove/'+str(dose),
            json=json.loads(request.data),
            )
        return r.status_code

@app.route('/users/Coctails/<user_id>/Add<coctail_id>', methods=["PUT"])
def add_user_coctail(user_id, coctail_id):
    if request.method == "PUT":
        r = requests.put(
            'http://localhost:5233/users/Coctails/'+str(user_id)+'/Add'+str(coctail_id),
            json=json.loads(request.data),
            )
        return r.status_code

@app.route('/users/Coctails/<user_id>/Remove<coctail_id>', methods=["PUT"])
def remove_user_coctail(user_id, coctail_id):
    if request.method == "PUT":
        r = requests.put(
            'http://localhost:5233/users/Coctails/'+str(user_id)+'/Remove'+str(coctail_id),
            json=json.loads(request.data),
            )
        return r.status_code


