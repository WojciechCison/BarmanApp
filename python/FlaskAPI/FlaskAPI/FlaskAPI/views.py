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

@app.route('/users/Github', methods=["GET"])
def github_login():
    if request.method == "GET":
        r = requests.get(
            'http://localhost:5233/users/Github', 
            #json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)
    else:
        return 'login page'

@app.route('/users/register', methods=["POST", "GET"])
def register():
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/users/Register', 
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)
    else:
        return 'register page'
    
@app.route('/coctails', methods=["POST"])
def coctails_add():
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/coctails', 
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)


@app.route('/coctails/<token>', methods=["GET"])
def coctails(token): 
        r_coctails = requests.get(
            f'http://localhost:5233/coctails/{token}',
            #json=json.loads(request.data),
            )

        r_ingridients = requests.get(
            f'http://localhost:5233/ingridients/{token}',
            #json=json.loads(request.data),
            )
        r_comments = requests.get(
            f'http://localhost:5233/Comments/{token}',
            #json=json.loads(request.data),
            )
        r_coctails_json = r_coctails.json()        
        r_ingridients_json = r_ingridients.json()
        r_comments_json = r_comments.json()
        for i in r_coctails_json:
            i['creatable'] = 1                      
            for j in i['coctailIngridients']:
                ing_id = j['ingridientId']           
                ing_data = next((item for item in r_ingridients_json if item['id'] == ing_id), None)
                j.update(ing_data)
                quantity = j['quantity']
                dose = j['dose']
                if quantity < dose:
                    i['creatable'] = 0
                j.pop('ingridientId', None)
                j.pop('coctailIngridients', None)
                j.pop('coctailId', None)
                j.pop('storagedIngridientEntity', None)   
            i['comments'] = [] 
            for k in r_comments_json:
                #comment = next((item for item in r_comments_json if item['coctailId'] == i['id']), None)
                if k['coctailId'] == i['id']:
                    i['comments'].append(k)
        return r_coctails_json

@app.route('/coctails/<id>', methods=["DELETE"])
def delete_coctail(id):
    if request.method == "DELETE":
        r = requests.delete(
            f'http://localhost:5233/coctails/{id}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)

@app.route('/ingridients', methods=["POST"])
def ingridients_add():
    """Renders the contact page."""
    if request.method == "POST":
        r = requests.post(
            'http://localhost:5233/ingridients/', 
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)

@app.route('/ingridients/<token>', methods=["GET"])
def ingridients(token):   
        r_ingridients = requests.get(
            f'http://localhost:5233/ingridients/{token}',
            )
        #r_coctails_json = r_coctails.json()        
        r_ingridients_json = r_ingridients.json()
        return r_ingridients_json

@app.route('/ingridients/<id>', methods=["DELETE"])
def delete_ingridient(id):
    if request.method == "DELETE":
        r = requests.delete(
            f'http://localhost:5233/ingridients/{id}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)

@app.route('/Comments/<token>', methods=["POST", "GET"])
def comment_add(token):
    if request.method == "POST":
        r = requests.post(
            f'http://localhost:5233/Comments/{token}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)
    if request.method == "GET":
        r = requests.get(
            f'http://localhost:5233/Comments/{token}',
            #json=json.loads(request.data),
            )
        r_json = r.json()
        return r_json


@app.route('/Comments/<token>/<commentId>', methods=["DELETE", "PUT"])
def comment_ed(token, commentId):
    if request.method == "PUT":
        r = requests.put(
            f'http://localhost:5233/Comments/{token}/{commentId}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)
    if request.method == "DELETE":
        r = requests.delete(
            f'http://localhost:5233/Comments/{token}/{commentId}',
            #json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)


@app.route('/Storage/<id>/Add/<dose>', methods=["PUT"])
def add_to_storage(id, dose):
    if request.method == "PUT":
        r = requests.put(
            f'http://localhost:5233/Storage/{id}/Add/{dose}',
            json=json.loads(request.data)
            )
        return jsonify(status=r.status_code, error=r.reason)

@app.route('/Storage/<id>/Remove/<dose>', methods=["PUT"])
def remove_from_storage(id, dose):
    if request.method == "PUT":
        r = requests.put(
            f'http://localhost:5233/Storage/{id}/Remove/{dose}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)

@app.route('/users/Coctails/<user_id>/Add/<coctail_id>', methods=["PUT"])
def add_user_coctail(user_id, coctail_id):
    if request.method == "PUT":
          r = requests.put(
            f'http://localhost:5233/users/Coctails/{user_id}/Add/{coctail_id}',
            json=json.loads(request.data),
            )    
          return jsonify(status=r.status_code, error=r.reason)

                   
@app.route('/users/Coctails/<user_id>/Remove/<coctail_id>', methods=["PUT"])
def remove_user_coctail(user_id, coctail_id):
    if request.method == "PUT":
        r = requests.put(
            f'http://localhost:5233/users/Coctails/{user_id}/Remove/{coctail_id}',
            json=json.loads(request.data),
            )
        return jsonify(status=r.status_code, error=r.reason)


