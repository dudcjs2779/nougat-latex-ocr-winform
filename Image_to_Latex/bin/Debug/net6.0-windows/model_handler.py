from flask import Flask, jsonify, request, render_template, send_file
import image_to_ratex
import os
import time
from io import BytesIO

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
        # 이미지 데이터 추출
        image_file = request.files['image']

        # 이미지 파일을 바이트로 읽어오기
        image_data = image_file.read()

        # 여기서 이미지 데이터를 원하는 방식으로 처리할 수 있습니다.
        # 예를 들어, 이미지를 파일로 저장하거나 다른 이미지 처리 작업을 수행할 수 있습니다.
        
        result = image_to_ratex.run_nougat_latex(model, tokenizer, latex_processor, image_data)

        # 처리가 완료된 결과
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