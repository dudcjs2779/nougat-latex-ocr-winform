from flask import Flask, jsonify, request
import image_to_ratex
import os

app = Flask(__name__)

# 처음 서버 실행 시 모델 로드
model, tokenizer, latex_processor = image_to_ratex.load_model()

@app.route("/")
def dummy_server():
    return "<h1>Dummy Server</h1>"

@app.route("/<username>")
def hell_user(username):
    return f"<h1>Hello, {username}!</h1>"

@app.route('/Generate', methods=['POST'])
def Generate():
    try:
        # get file from request
        image_file = request.files['image']

        # convert image to byte data
        image_data = image_file.read()

        # Generate Latex from image
        result = image_to_ratex.run_nougat_latex(model, tokenizer, latex_processor, image_data)

        return jsonify(result)

    except Exception as e:
        return jsonify({'error': str(e)})


def shutdown_server():
    os._exit(0);
    
@app.route('/shutdown', methods=['POST'])
def shutdown():
    shutdown_server()
    return 'Server shutting down...'

if __name__ == '__main__':
    app.run(debug=True)