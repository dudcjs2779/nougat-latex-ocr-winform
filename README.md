# nougat-latex-ocr-winform
This project based on https://github.com/NormXU/nougat-latex-ocr<br>
Model: https://huggingface.co/Norm/nougat-latex-base
<br><br><br>
This program was created to easily convert images to Latex.<br>
This project is to build a GUI using C# WINFORM and get the execution result of the model from a Python file.<br>
I created this project to practice running a python file in C# to get the output of a model.
<br><br>
nougat-latex-ocr is model that convert image to Latex.
<br>
To make this process more convenient, I created this project.
I made this project to practice for 

## Uses
1. Move to path
2. Create a virtual environment.
   <br>Virtual environment name must be "venv"!
3. Activate virtual environment.

<pre>
  cd nougat-latex-ocr-winform\Image_to_Latex\bin\Debug\net6.0-windows
  python -m venv venv
  venv\Scripts\activate
</pre>

Download package files and [pytorch](https://pytorch.org/)
<pre>
  pip install -r requirements.txt
</pre>

Run "nougat-latex-ocr-winform\Image_to_Latex\bin\Debug\net6.0-windows\Image_to_Latex.exe" file.<br>
Copy and paste the clipboard into the program, then click Generate Latex.
<br><br>
<img src="https://github.com/dudcjs2779/nougat-latex-ocr-winform/assets/42354230/bc1d2ce5-89f3-4534-b623-f12d6ff35f00" alt="test_image" width="400">

