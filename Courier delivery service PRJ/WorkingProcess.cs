using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace Courier_delivery_service_PRJ
{
    public partial class WorkingProcess : Form
    {
        private int progressValue = 0;
        private int totalSteps = 10;
        private Timer timer;
        public ClientForm clientForm;
        public double client_salary { get; set; }

        public WorkingProcess(ClientForm form)
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;

            clientForm = form;

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalSteps;
        }
        public WorkingProcess()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += Timer_Tick;

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalSteps;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progressValue++;
            progressBar1.Value = progressValue;
            labelStatus.Text = $"Выполняется... {progressValue * 10}%";

            UpdateStatusText();

            if (progressValue >= totalSteps)
            {
                timer.Stop();
                labelStatus.Text = "Работа завершена!";
                MessageBox.Show($"Ваша зарплата составила: {client_salary} рублей!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Task.Delay(2000).ContinueWith(_ =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.DialogResult = DialogResult.OK;
                        clientForm.Visible = true;
                        this.Close();
                    });
                });
            }
        }

        private void UpdateStatusText()
        {
            switch (progressValue)
            {
                case 1: labelStep.Text = "🚗 Отправляемся на работу..."; break;
                case 2: labelStep.Text = "🏢 Пришли на работу..."; break;
                case 3: labelStep.Text = "🛠️ Подготовка к работе..."; break;
                case 4: labelStep.Text = "💼 Начинаем работать..."; break;
                case 5: labelStep.Text = "⚡ Работаем..."; break;
                case 6: labelStep.Text = "☕ Перерыв..."; break;
                case 7: labelStep.Text = "🚀 Продолжаем работать..."; break;
                case 8: labelStep.Text = "🤔 Понимаем, что работать больше не надо..."; break;
                case 9: labelStep.Text = "✅ Закончили работать..."; break;
                case 10: labelStep.Text = "💰 Начисляем заработную плату..."; break;
            }
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void WorkingProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientForm.Show();
        }

        private void WorkingProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }

            timer = null;
        }
    }
}