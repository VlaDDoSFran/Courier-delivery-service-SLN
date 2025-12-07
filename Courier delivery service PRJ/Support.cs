using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Net.Security;

namespace Courier_delivery_service_PRJ
{
    public partial class Support : Form
    {
        private readonly HttpClient _httpClient;
        private string _clientId = "019af560-7572-7281-bf22-c2e06dc96aa4";
        private string _clientSecret = "5996a4d5-3028-45fd-9f2b-9dfc1051cb24";
        private string _accessToken = "";
        private DateTime _tokenExpiry = DateTime.MinValue;
        private bool _aiAvailable = false;
        public Form form;

        private const string GIGACHAT_AUTH_URL = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        private const string GIGACHAT_CHAT_URL = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";

        public Support(Form form)
        {
            InitializeComponent();

            if (form is ClientForm)
            {
                this.form = (ClientForm)form;
            }
            else if (form is CourierForm)
            {
                this.form = (CourierForm)form;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (cert.Subject.Contains("sberbank.ru") ||
                    cert.Subject.Contains("devices.sberbank.ru"))
                    return true;

                return sslPolicyErrors == SslPolicyErrors.None;
            };

            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (cert.Subject.Contains("sberbank.ru") ||
                        cert.Subject.Contains("devices.sberbank.ru"))
                        return true;

                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            })
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            SetupUIComponents();
            AddWelcomeMessage();

            UpdateStatus("Нажмите 'Проверить API' для подключения", Color.White);
            CheckAPIOnLoad();
        }

        public Support()
        {
            InitializeComponent();

            if (form is ClientForm)
            {
                this.form = (ClientForm)form;
            }
            else if (form is CourierForm)
            {
                this.form = (CourierForm)form;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                if (cert.Subject.Contains("sberbank.ru") ||
                    cert.Subject.Contains("devices.sberbank.ru"))
                    return true;

                return sslPolicyErrors == SslPolicyErrors.None;
            };

            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (cert.Subject.Contains("sberbank.ru") ||
                        cert.Subject.Contains("devices.sberbank.ru"))
                        return true;

                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            })
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            SetupUIComponents();
            AddWelcomeMessage();

            UpdateStatus("Нажмите 'Проверить API' для подключения", Color.White);
            CheckAPIOnLoad();
        }

        private void SetupUIComponents()
        {
            this.Text = "🤖 Поддержка службы доставки";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            panelTop.BackColor = Color.FromArgb(255, 204, 0);
            labelTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            labelTitle.ForeColor = Color.White;
            labelTitle.Text = "🤖 Поддержка службы доставки";

            lblStatus.Font = new Font("Segoe UI", 10);
            lblStatus.ForeColor = Color.White;
            lblStatus.Text = "⏳ Проверка подключения...";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;

            panelChat.BackColor = Color.White;

            chatList.View = View.Details;
            chatList.Columns.Add("Чат", 780);
            chatList.HeaderStyle = ColumnHeaderStyle.None;
            chatList.BackColor = Color.FromArgb(248, 249, 250);
            chatList.Font = new Font("Segoe UI", 10);
            chatList.BorderStyle = BorderStyle.None;
            chatList.FullRowSelect = true;
            chatList.MultiSelect = false;

            panelInput.BackColor = Color.White;

            txtQuestion.Multiline = true;
            txtQuestion.Font = new Font("Segoe UI", 10);
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.BorderStyle = BorderStyle.FixedSingle;

            panelButtons.BackColor = Color.FromArgb(248, 249, 250);

            panelBottom.BackColor = Color.FromArgb(248, 249, 250);
            labelSupportInfo.Font = new Font("Segoe UI", 9);
            labelSupportInfo.ForeColor = Color.FromArgb(73, 80, 87);
            labelSupportInfo.Text = "📞 Телефон поддержки: 8-800-555-35-35 | ⏰ Работаем: ежедневно с 8:00 до 22:00 | 🌐 Сайт: https://fast-delivery.ru";
            labelSupportInfo.TextAlign = ContentAlignment.MiddleCenter;

            InitializeButtons();
        }

        private void InitializeButtons()
        {
            // 1. Кнопка "Отправить"
            btnSend.BackColor = Color.FromArgb(255, 153, 0);
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSend.Text = "📤 Отправить";
            btnSend.Cursor = Cursors.Hand;
            btnSend.Click += btnSend_Click;

            // 2. Кнопка "Очистить чат"
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Font = new Font("Segoe UI", 10);
            btnClear.Text = "🗑️ Очистить чат";
            btnClear.Cursor = Cursors.Hand;
            btnClear.Click += btnClear_Click;

            // 3. Кнопка "Проверить API"
            btnTestAPI.BackColor = Color.FromArgb(40, 167, 69);
            btnTestAPI.ForeColor = Color.White;
            btnTestAPI.FlatStyle = FlatStyle.Flat;
            btnTestAPI.FlatAppearance.BorderSize = 0;
            btnTestAPI.Font = new Font("Segoe UI", 10);
            btnTestAPI.Text = "🔍 Проверить API";
            btnTestAPI.Cursor = Cursors.Hand;
            btnTestAPI.Click += btnTestAPI_Click;

            // 4. Кнопка "Выйти"
            exitButton.BackColor = Color.FromArgb(220, 53, 69);
            exitButton.ForeColor = Color.White;
            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Font = new Font("Segoe UI", 10);
            exitButton.Text = "🚪 Выйти";
            exitButton.Cursor = Cursors.Hand;
            exitButton.Click += exitButton_Click;
        }

        private async Task<bool> GetAccessToken()
        {
            try
            {
                if (!string.IsNullOrEmpty(_accessToken) && DateTime.Now < _tokenExpiry)
                {
                    Console.WriteLine($"Используем существующий токен, истекает в {_tokenExpiry:HH:mm:ss}");
                    return true;
                }

                if (string.IsNullOrEmpty(_clientId) || string.IsNullOrEmpty(_clientSecret))
                {
                    Console.WriteLine("ERROR: Client ID или Client Secret не установлены");
                    return false;
                }

                string authString = $"{_clientId}:{_clientSecret}";
                string base64Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

                var requestData = new Dictionary<string, string>
                {
                    { "scope", "GIGACHAT_API_PERS" }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, GIGACHAT_AUTH_URL)
                {
                    Content = new FormUrlEncodedContent(requestData)
                };
                request.Headers.Add("Authorization", $"Basic {base64Auth}");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("RqUID", Guid.NewGuid().ToString());

                var response = await _httpClient.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = JsonConvert.DeserializeObject<TokenResponse>(responseText);

                    if (responseObject != null && responseObject.access_token != null)
                    {
                        _accessToken = (string)responseObject.access_token;

                        long expiresIn = 0;
                        if (responseObject.expires_in != null)
                        {
                            expiresIn = Convert.ToInt64(responseObject.expires_in);
                        }

                        _tokenExpiry = DateTime.Now.AddSeconds(expiresIn > 0 ? expiresIn - 300 : 1200);
                        Console.WriteLine($"Токен получен! Действителен до {_tokenExpiry:HH:mm:ss}");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка получения токена: {response.StatusCode}");
                    if (!string.IsNullOrEmpty(responseText))
                    {
                        Console.WriteLine($"Детали: {responseText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в GetAccessToken: {ex.Message}");
            }

            return false;
        }

        private void AddWelcomeMessage()
        {
            string aiStatus = _aiAvailable ?
                "Я помощник на базе GigaChat. " :
                "Я текстовый помощник службы доставки. ";

            string welcomeText = "Добро пожаловать в службу поддержки 'Быстрая Доставка'! 📦\n\n" +
                                aiStatus +
                                "Могу помочь с:\n" +
                                "• Отслеживанием заказов\n" +
                                "• Тарифами доставки\n" +
                                "• Изменением данных\n" +
                                "• Решением проблем\n" +
                                "• Ответами на вопросы о доставке\n\n" +
                                "Задавайте ваш вопрос!";

            AddMessageToChat("🤖 Поддержка", welcomeText, Color.FromArgb(255, 245, 225));
        }

        private void AddMessageToChat(string sender, string message, Color backgroundColor)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string displayText = $"[{timestamp}] {sender}:\n{message}";

            var item = new ListViewItem(displayText)
            {
                BackColor = backgroundColor,
                Font = new Font("Segoe UI", 10),
                UseItemStyleForSubItems = true
            };

            chatList.Items.Add(item);
            item.EnsureVisible();
        }

        private async Task SendMessage()
        {
            string question = txtQuestion.Text.Trim();
            if (string.IsNullOrWhiteSpace(question)) return;

            AddMessageToChat("👤 Вы", question, Color.FromArgb(229, 246, 253));
            txtQuestion.Clear();
            txtQuestion.Focus();

            btnSend.Enabled = false;
            btnSend.Text = "⏳ Отправка...";

            try
            {
                string response;

                if (_aiAvailable)
                {
                    if (DateTime.Now >= _tokenExpiry)
                    {
                        await GetAccessToken();
                    }
                    response = await AskGigaChat(question);
                }
                else
                {
                    response = GetTextResponse(question);
                }

                AddMessageToChat("🤖 Поддержка", response, Color.FromArgb(255, 245, 225));
            }
            catch (Exception ex)
            {
                AddMessageToChat("❌ Ошибка",
                    $"Не удалось получить ответ от ИИ: {ex.Message}\n" +
                    "Переключен в текстовый режим.\n" +
                    "Позвоните нам: 8-800-555-35-35",
                    Color.FromArgb(255, 228, 225));

                _aiAvailable = false;
                UpdateStatus("⚠️ API недоступен (текстовый режим)", Color.Orange);
            }
            finally
            {
                btnSend.Enabled = true;
                btnSend.Text = "📤 Отправить";
            }
        }

        private async Task<string> AskGigaChat(string question)
        {
            await Task.Delay(1000);
            try
            {
                if (string.IsNullOrEmpty(_accessToken) || DateTime.Now >= _tokenExpiry)
                {
                    if (!await GetAccessToken())
                    {
                        throw new Exception("Не удалось получить токен доступа");
                    }
                }

                var request = new GigaChatRequest
                {
                    model = "GigaChat",
                    messages = new Message[]
                    {
                        new Message
                        {
                            role = "system",
                            content = @"Ты помощник службы доставки 'Быстрая Доставка'. 
                            Телефон поддержки: 8-800-555-35-35. 
                            Тарифы: стандартная доставка (1-3 дня) - 300₽, экспресс (24 часа) - 600₽, срочная (2-4 часа) - 1200₽.
                            Бесплатная доставка при заказе от 3000₽.
                            График работы: ежедневно с 8:00 до 22:00.
                            Отвечай кратко, вежливо и по делу, только на вопросы связанные с доставкой.
                            Если вопрос не по теме, вежливо скажи, что можешь помочь только с вопросами доставки.
                            Также не упоминай, что ты генеративная языковая модель, нейросеть или GigaChat ни в коем случае!"
                        },
                        new Message
                        {
                            role = "user",
                            content = question
                        }
                    },
                    temperature = 0.7,
                    max_tokens = 350,
                    stream = false
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, GIGACHAT_CHAT_URL)
                {
                    Content = content
                };
                requestMessage.Headers.Add("Authorization", $"Bearer {_accessToken}");
                requestMessage.Headers.Add("Accept", "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var responseText = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<GigaChatResponse>(responseText);
                    if (result?.choices?.Length > 0 && result.choices[0].message?.content != null)
                    {
                        return ((string)result.choices[0].message.content).Trim();
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _accessToken = "";
                    _tokenExpiry = DateTime.MinValue;
                    if (await GetAccessToken())
                    {
                        return await AskGigaChat(question);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в AskGigaChat: {ex.Message}");
                _aiAvailable = false;
                UpdateStatus("⚠️ Ошибка подключения (текстовый режим)", Color.Orange);
            }

            return GetTextResponse(question);
        }

        private string GetTextResponse(string question)
        {
            question = question.ToLower();

            var responses = new Dictionary<Func<string, bool>, string>
            {
                { q => q.Contains("где") && q.Contains("заказ"),
                  "Для проверки статуса заказа нужен его номер. Пожалуйста, укажите номер заказа (например, A123456) 📦" },

                { q => q.Contains("сколько") && (q.Contains("стоит") || q.Contains("цена") || q.Contains("тариф")),
                  "💰 Наши тарифы:\n• Стандарт (1-3 дня): 300₽\n• Экспресс (24 часа): 600₽\n• Срочная (2-4 часа): 1200₽\n• Бесплатно при заказе от 3000₽! 🎁" },

                { q => q.Contains("телефон") || q.Contains("звон") || q.Contains("позвонить"),
                  "📞 Телефон поддержки: 8-800-555-35-35\n⏰ Работаем: ежедневно с 8:00 до 22:00" },

                { q => q.Contains("часы") || q.Contains("работа") || q.Contains("график"),
                  "⏰ График работы:\nКурьеры: Пн-Пт 9:00-21:00, Сб-Вс 10:00-20:00\nПоддержка: ежедневно 8:00-22:00" },

                { q => q.Contains("отследить") || q.Contains("трек") || q.Contains("статус"),
                  "🔍 Отследить заказ можно:\n• На сайте: https://track.fast-delivery.ru/\n• По телефону: 8-800-555-35-35\n• В мобильном приложении" },

                { q => q.Contains("привет") || q.Contains("здравств") || q.Contains("добрый"),
                  "👋 Здравствуйте! Я помощник службы доставки. Чем могу помочь?" },

                { q => q.Contains("спасибо") || q.Contains("благодар"),
                  "😊 Всегда рады помочь! Хорошего дня! 📦" }
            };

            foreach (var kvp in responses)
            {
                if (kvp.Key(question))
                    return kvp.Value;
            }

            return "🤔 Уточните, пожалуйста, ваш вопрос.\n" +
                   "Я могу помочь с:\n" +
                   "• Отслеживанием заказов\n" +
                   "• Тарифами доставки\n" +
                   "• Графиком работы\n" +
                   "• Изменением данных\n" +
                   "• Возвратами и обменами\n" +
                   "• Пунктами выдачи\n\n" +
                   "Или позвоните в поддержку: 8-800-555-35-35\n" +
                   "Мы работаем: 8:00-22:00 📞";
        }

        private void UpdateStatus(string text, Color color)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
        }

        private async void btnTestAPI_Click(object sender, EventArgs e)
        {
            UpdateStatus("⏳ Проверка подключения...", Color.Blue);
            btnTestAPI.Enabled = false;
            btnTestAPI.Text = "⏳ Проверка...";

            try
            {
                bool tokenSuccess = await GetAccessToken();

                if (!tokenSuccess || string.IsNullOrEmpty(_accessToken))
                {
                    _aiAvailable = false;
                    UpdateStatus("⚠️ API недоступен", Color.Orange);

                    MessageBox.Show("❌ Не удалось подключиться к GigaChat API.\n\n" +
                                   "Проверьте:\n" +
                                   "1. Правильность учетных данных\n" +
                                   "2. Подключение к интернету\n" +
                                   "3. Доступность сервиса GigaChat",
                                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _aiAvailable = true;
                UpdateStatus($"✅ GigaChat готов к работе (токен до {_tokenExpiry:HH:mm:ss})", Color.Green);

                MessageBox.Show("✅ GigaChat API успешно подключен!\n\n" +
                               $"Токен действителен до: {_tokenExpiry:HH:mm:ss}\n\n" +
                               "Сервис доступен и готов к работе.",
                               "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _aiAvailable = false;
                UpdateStatus("⚠️ Ошибка подключения", Color.Orange);

                MessageBox.Show($"❌ Ошибка подключения:\n\n{ex.Message}\n\n" +
                               "Переключен в текстовый режим.",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnTestAPI.Enabled = true;
                btnTestAPI.Text = "🔍 Проверить API";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (chatList.Items.Count > 0 &&
                MessageBox.Show("Очистить историю чата?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                chatList.Items.Clear();
                AddWelcomeMessage();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _ = SendMessage();
        }

        private void txtQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control)
            {
                e.SuppressKeyPress = true;
                _ = SendMessage();
            }
        }

        private async void CheckAPIOnLoad()
        {
            try
            {
                UpdateStatus("⏳ Проверка подключения...", Color.Blue);

                bool tokenSuccess = await GetAccessToken();

                if (tokenSuccess && !string.IsNullOrEmpty(_accessToken))
                {
                    _aiAvailable = true;
                    UpdateStatus($"✅ GigaChat готов к работе (токен до {_tokenExpiry:HH:mm:ss})", Color.Green);
                }
                else
                {
                    _aiAvailable = false;
                    UpdateStatus("⚠️ API недоступен", Color.Orange);
                }
            }
            catch
            {
                _aiAvailable = false;
                UpdateStatus("⚠️ Ошибка подключения к GigaChat", Color.Orange);
            }
            finally
            {
                chatList.Items.Clear();
                AddWelcomeMessage();
            }
        }

        private void Support_Load(object sender, EventArgs e)
        {
            txtQuestion.Focus();
        }

        public class TokenResponse
        {
            public string access_token { get; set; }
            public long expires_in { get; set; }
            public string scope { get; set; }
            public string token_type { get; set; }
        }

        public class ErrorResponse
        {
            public string error { get; set; }
            public string error_description { get; set; }
        }

        public class GigaChatResponse
        {
            public Choice[] choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public class GigaChatRequest
        {
            public string model { get; set; } = "GigaChat";
            public Message[] messages { get; set; }
            public double temperature { get; set; } = 0.7;
            public int max_tokens { get; set; } = 350;
            public bool stream { get; set; } = false;
        }

        private void Support_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}