from flask import Flask
app = Flask(__name__)

@app.route("/api",methods=['GET'])
def hello():
    return "Hello World Now Docker"

app.run(host='0.0.0.0',port=5000)



'''
if __name__ == '__main__':
    app.run(debug=True)
'''