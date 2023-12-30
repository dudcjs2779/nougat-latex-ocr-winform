using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace Image_to_Latex
{
    public partial class Form1 : Form
    {
        int btnPasteImg_h = 40;
        int textBox_h = 100;
        int label_h = 60;
        int offset_h = 50;

        bool isServerRunning = false;

        private Timer? timer;
        private int currentNumber;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            btnPasteImg.Width = flowLayoutPanel1.Width / 2 - 10;
            btnPasteImg.Height = btnPasteImg_h;
            btnGenerate.Width = flowLayoutPanel1.Width / 2 - 10;
            btnGenerate.Height = btnPasteImg_h;

            pictureBox1.Width = flowLayoutPanel1.Width;
            pictureBox1.Height = flowLayoutPanel1.Height - btnPasteImg_h - textBox_h - label_h - offset_h;

            textBox1.Width = flowLayoutPanel1.Width;
            textBox1.Height = textBox_h;

            label1.Width = flowLayoutPanel1.Width;
            label1.Height = label_h;

            await ExcutePythonServer();
        }

        private void btnPasteImg_Click(object sender, EventArgs e)
        {
            InputClipboardImage();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                InputClipboardImage();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            btnPasteImg.Width = flowLayoutPanel1.Width / 2 - 10;
            btnGenerate.Width = flowLayoutPanel1.Width / 2 - 10;

            pictureBox1.Width = flowLayoutPanel1.Width;
            pictureBox1.Height = flowLayoutPanel1.Height - btnPasteImg_h - textBox_h - label_h - offset_h;

            textBox1.Width = flowLayoutPanel1.Width;

            label1.Width = flowLayoutPanel1.Width;
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Image is Empty.");
                return;
            }
            await GenerateAsync();
        }

        // excute python script to start local server
        // This local server runs to maintain the model's data memory.
        async Task ExcutePythonServer()
        {
            Console.WriteLine("ExcutePythonServer");
            label1.Text = "Server is loading...";

            // Directory Path
            string currentDirectory = Environment.CurrentDirectory;

            string pyScriptPath = "model_handler.py";   // Python file name
            string pythonPath = "venv/Scripts/python.exe";  // Python name

            // Absolute Path
            string absol_PyScriptPath = Path.Combine(currentDirectory, pyScriptPath);
            string absol_PythonPath = Path.Combine(currentDirectory, pythonPath);

            using (Process process = new Process())
            {
                process.StartInfo.FileName = absol_PythonPath;
                process.StartInfo.Arguments = absol_PyScriptPath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                // For async processing
                Task<string> outputTask = process.StandardOutput.ReadToEndAsync();

                await Task.WhenAll(outputTask, process.WaitForExitAsync());
                string output = await outputTask;
                Console.WriteLine("Python script output:\n" + output);

                Task<string> errorTask = process.StandardError.ReadToEndAsync();

                await Task.WhenAll(errorTask, process.WaitForExitAsync());
                output = await errorTask;
                Console.WriteLine("Python script error:\n" + output);
            }
        }

        // sent request to quit server
        void ShutdownFlaskServer()
        {
            string flaskServerUrl = "http://localhost:5000/shutdown";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.PostAsync(flaskServerUrl, new StringContent("")).Result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        async Task GenerateAsync()
        {
            // /Generate is flask EndPoint to generate text
            string flaskServerUrl = "http://localhost:5000/Generate";
            label1.Text = "Generating Latex";

            using (HttpClient client = new HttpClient())
            {
                // PictureBox image convert to array data
                byte[] imageData = ImageToByteArray(pictureBox1.Image);

                // create MultipartFormDataContent for send image data
                using (var content = new MultipartFormDataContent())
                {
                    // add image data
                    var imageContent = new ByteArrayContent(imageData);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    content.Add(imageContent, "image", "image.png"); // "image" is name of key to get data at python, "image.jpg" is filename

                    // POST request
                    try
                    {
                        HttpResponseMessage response = await client.PostAsync(flaskServerUrl, content);

                        // check response
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Generate Latex");
                            Console.WriteLine(response);

                            // get data from python server's response
                            string responseData = await response.Content.ReadAsStringAsync();

                            // data process
                            responseData = responseData.Replace("\"", "");
                            responseData = responseData.Replace("\\\\", "\\");

                            // responseData is Latex data converted from an image
                            Console.WriteLine($"Generate successful. Result: {responseData}");
                            SetTextBox(responseData);
                        }
                        else
                        {
                            Console.WriteLine($"Generate failed. Status code: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Model file is not yet loaded.");
                        Console.WriteLine(ex);
                    }

                }
            }
        }

        // check server loading is done
        async Task<string> CheckServer()
        {
            // Local url
            string serverUrl = "http://localhost:5000";
            string message = "";

            // create HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // GET request
                    HttpResponseMessage response = await client.GetAsync(serverUrl);

                    // get response
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Server response:\n" + content);
                        message = "Server is Loaded!";
                        isServerRunning = true;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        message = "Loading server";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    message = "Loading server";
                }
            }

            return message;
        }

        // For display server loading
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1초마다 호출
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            // Display Server Loading
            if (!isServerRunning)
            {
                string dot = ".";
                if (currentNumber % 3 == 0)
                    dot = ".";
                else if (currentNumber % 3 == 1)
                    dot = "..";
                else
                    dot = "...";

                string message = await CheckServer();
                label1.Text = message + dot;
                currentNumber++;
            }
            else
            {
                string message = await CheckServer();
                label1.Text = message;
                timer?.Stop();
            }
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("closing");
            ShutdownFlaskServer();
        }

        // for debug
        private void button1_Click(object sender, EventArgs e)
        {
            ShutdownFlaskServer();
        }

        // for debug
        private async void button2_Click(object sender, EventArgs e)
        {
            await CheckServer();
        }

        void SetTextBox(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                textBox1.Text = text;
                Clipboard.SetText(text);
                label1.Text = "Generate is done! Text is copied to clipboard!";
            }
        }


        public byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // convert image to array data
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        void InputClipboardImage()
        {
            label1.Text = "";

            // get image from clipboard
            if (Clipboard.ContainsImage())
            {
                Image clipboardImage = Clipboard.GetImage();

                // set image to pictureBox
                pictureBox1.Image = clipboardImage;
            }
            else
            {
                MessageBox.Show("Clipboard has no image.");
            }
        }
    }
}