# -*- coding:utf-8 -*-
# create: @time: 10/8/23 11:47
import sys

import torch
from PIL import Image
from io import BytesIO
from transformers import VisionEncoderDecoderModel
from transformers.models.nougat import NougatTokenizerFast
from nougat_latex.util import process_raw_latex_code
from nougat_latex import NougatLaTexProcessor

import os

device = torch.device("cuda:0")
model_name = 'Norm/nougat-latex-base'

def print_os_path():
    # 현재 스크립트 파일의 절대 경로
    script_path = os.path.abspath(__file__)

    # 현재 스크립트 파일의 디렉토리 경로
    script_directory = os.path.dirname(script_path)

    print("현재 스크립트 파일의 디렉토리:", script_directory)
    print("현재 스크립트 파일의 경로", script_path)
    
def load_model():
    model = VisionEncoderDecoderModel.from_pretrained(model_name).to(device)
    print("모델이 로드되었습니다.")

    tokenizer = NougatTokenizerFast.from_pretrained(model_name)
    print("토크나이저가 로드되었습니다.")

    latex_processor = NougatLaTexProcessor.from_pretrained(model_name)
    print("라텍스 프로세서가 로드되었습니다.")
    
    return model, tokenizer, latex_processor

def run_nougat_latex(model, tokenizer, latex_processor, image_data):
    
    # 바이트 배열을 이미지로 변환
    image = Image.open(BytesIO(image_data))

    if not image.mode == "RGB":
        image = image.convert('RGB')

    pixel_values = latex_processor(image, return_tensors="pt").pixel_values
    task_prompt = tokenizer.bos_token
    decoder_input_ids = tokenizer(task_prompt, add_special_tokens=False,
                                  return_tensors="pt").input_ids
    with torch.no_grad():
        outputs = model.generate(
            pixel_values.to(device),
            decoder_input_ids=decoder_input_ids.to(device),
            max_length=model.decoder.config.max_length,
            early_stopping=True,
            pad_token_id=tokenizer.pad_token_id,
            eos_token_id=tokenizer.eos_token_id,
            use_cache=True,
            num_beams=1,
            bad_words_ids=[[tokenizer.unk_token_id]],
            return_dict_in_generate=True,
        )
    sequence = tokenizer.batch_decode(outputs.sequences)[0]
    sequence = sequence.replace(tokenizer.eos_token, "").replace(tokenizer.pad_token, "").replace(tokenizer.bos_token,
                                                                                                  "")
    sequence = process_raw_latex_code(sequence)
    print(sequence)
    
    return sequence;


# if __name__ == '__main__':
#     run_nougat_latex()
